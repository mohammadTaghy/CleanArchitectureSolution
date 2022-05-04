using Application;
using Domain;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public abstract class RepositoryReadBase<T> : IRepositoryReadBase<T> where T : class, IEntity
    {
        #region Property
        private string connectionName = "MongoDBConnectionString";
        private string connectionString;
        private readonly IMongoDatabase _dB;
        private IMongoCollection<T> collection;
        private MongoClient mongoClient;
        public IQueryable<T> Queryable => collection.AsQueryable();
        public CancellationToken CancellationToken { get; set; }
        #endregion

        public RepositoryReadBase(IConfiguration config)
        {
            connectionString = config.GetConnectionString(connectionName);
            mongoClient = new MongoClient(connectionString);
            this._dB = mongoClient.GetDatabase(connectionString.Split('/').Last());
            collection = _dB.GetCollection<T>(typeof(T).FullName);
        }
        #region Get

        public IList<T> FindAll() => Queryable.ToList();

        public IList<T> FindAll(Expression<Func<T, bool>> predicate) => Queryable.Where(predicate).ToList();

        public T FindOne(int id) => Queryable.Where(p => p.Id == id).First();
        public bool TryFindOne(int id, ref T? entity)
        {
            entity = Queryable.Where(p => p.Id == id).FirstOrDefault();
            return entity != null;
        }
        public bool Exists(Expression<Func<T, bool>> predicate) => Queryable.Any(predicate);
        public T FindOne(Expression<Func<T, bool>> predicate) => Queryable.Where(predicate).First();
        public bool TryFindOne(Expression<Func<T, bool>> predicate, ref T? entity)
        {
            entity = Queryable.Where(predicate).FirstOrDefault();
            return entity != null;
        }
        #endregion
        #region Manipulate

        public async void Add(T entity)
        {
            await AddMeny(Enumerable.Repeat(entity, 1));
        }

        public async Task AddMeny(IEnumerable<T> entities)
        {
            using (var session = await mongoClient.StartSessionAsync())
            {
                session.StartTransaction();
                try
                {
                    InsertManyOptions insertManyOptions = new InsertManyOptions();
                    insertManyOptions.IsOrdered = true;
                    Task task= this.collection.InsertManyAsync(session, entities,insertManyOptions,CancellationToken);
                    await task.ContinueWith(p=> session.CommitTransactionAsync());
                }
                catch (Exception ex)
                {
                    await session.AbortTransactionAsync();
                    throw;
                }
            }
        }

        public void Delete(T entity)
        {
            Delete(entity.Id);
        }

        public async void Delete(int id)
        {

            try
            {

                await this.collection.DeleteOneAsync(p => p.Id == id, CancellationToken);

            }
            catch (Exception ex)
            {
                // must be save in log history
                throw;
            }

        }

        public async Task Delete(IList<T> entities)
        {
            using (var session = await mongoClient.StartSessionAsync())
            {
                session.StartTransaction();
                try
                {

                    Task task= Delete(entities.Select(p => p.Id).ToList());
                    await task.ContinueWith(p=> session.CommitTransactionAsync());
                }
                catch (Exception)
                {
                    await session.AbortTransactionAsync();
                    throw;
                }
            }
        }


        public async Task Delete(IList<int> ids)
        {
            using (var session = await mongoClient.StartSessionAsync())
            {
                session.StartTransaction();
                try
                {
                    Task task = this.collection.DeleteManyAsync<T>(p => ids.Contains(p.Id), CancellationToken);
                    await task.ContinueWith(p => session.CommitTransactionAsync());
                }
                catch (Exception)
                {
                    await session.AbortTransactionAsync();
                    throw;
                }
            }
        }
        public async Task Update(T entity)
        {
            try
            {
                ReplaceOptions replaceOptions = new ReplaceOptions();
                replaceOptions.IsUpsert = true;
                await this.collection.ReplaceOneAsync<T>(p => p.Id == entity.Id, entity,replaceOptions,CancellationToken);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task Update(IList<T> entities)
        {
            using (var session = await mongoClient.StartSessionAsync())
            {
                session.StartTransaction();
                try
                {
                    Task task= Task.Run(() =>
                    {
                        Parallel.ForEach(entities, p =>
                        {
                            Update(p);
                        });
                    });
                    
                    await task.ContinueWith(p=> session.CommitTransactionAsync());
                }
                catch (Exception)
                {
                    await session.AbortTransactionAsync();
                    throw;
                }
            }
        }
        #endregion




    }
}
