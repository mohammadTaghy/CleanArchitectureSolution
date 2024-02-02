using Application;
using Domain;
using Microsoft.AspNetCore.OData.Query;
using MongoDB.Driver;
using System.Data.Entity;
using System.Linq.Expressions;
using Microsoft.Extensions.Options;
using Common;
using Application.Common;
using System.Text;
using Newtonsoft.Json;
using Application.Common.Model;

namespace Persistence
{
    public abstract class RepositoryReadBase<T> : IRepositoryReadBase<T> where T : class, IEntity
    {
        #region Property
        private readonly IMongoDatabase _dB;
        private readonly IDirectExchangeRabbitMQ directExchangeRabbitMQ;
        private IMongoCollection<T> collection;
        private MongoClient mongoClient;
        public IQueryable<T> Queryable => collection.AsQueryable();
        public CancellationToken CancellationToken { get; set; }
        #endregion

        public RepositoryReadBase(IOptions<MongoDatabaseOption> mongoDatabaseOption, IDirectExchangeRabbitMQ directExchangeRabbitMQ)
        {
            string connectionString = mongoDatabaseOption.Value.ConnectionString;
            mongoClient = new MongoClient(connectionString);
            this._dB = mongoClient.GetDatabase(mongoDatabaseOption.Value.DatabaseName);
            collection = _dB.GetCollection<T>(typeof(T).FullName);
            SetReciveMessageEvent();
            this.directExchangeRabbitMQ = directExchangeRabbitMQ;
        }
        #region Get

        public IList<T> FindAll() => Queryable.ToList();

        public IList<T> FindAll(Expression<Func<T, bool>> predicate) => Queryable.Where(predicate).ToList();

        public async Task<Tuple<List<T>, int>> ItemList(ODataQueryOptions<T> options)
        {
            ODataQuerySettings settings = new ODataQuerySettings()
            {
                PageSize = 10,
            };

            IQueryable results = options.ApplyTo(Queryable, settings);
            return new Tuple<List<T>, int>(
                await results.ToListAsync() as List<T> ?? new List<T>(),
                int.Parse(options.Count.RawValue ?? "0")
                );
        }
        public async Task<T> FindOne(int id) => await Queryable.Where(p => p.Id == id).FirstOrDefaultAsync();
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

        public async Task Add(T entity)
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
                    Task task = this.collection.InsertManyAsync(session, entities, insertManyOptions, CancellationToken);
                    await task.ContinueWith(p => session.CommitTransactionAsync());
                }
                catch (Exception ex)
                {
                    await session.AbortTransactionAsync();
                    throw;
                }
            }
        }

        public async Task Delete(T entity)
        {
            await Delete(entity.Id);
        }

        public async Task Delete(int id)
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

                    Task task = Delete(entities.Select(p => p.Id).ToList());
                    await task.ContinueWith(p => session.CommitTransactionAsync());
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
                await this.collection.ReplaceOneAsync<T>(p => p.Id == entity.Id, entity, replaceOptions, CancellationToken);
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
                    Task task = Task.Run(() =>
                    {
                        Parallel.ForEach(entities, p =>
                        {
                            Update(p);
                        });
                    });

                    await task.ContinueWith(p => session.CommitTransactionAsync());
                }
                catch (Exception)
                {
                    await session.AbortTransactionAsync();
                    throw;
                }
            }
        }

        #endregion
        #region abstract
        protected virtual void SetReciveMessageEvent()
        {
            directExchangeRabbitMQ.RecieveMessage(new Application.Common.Model.RabbitMQRecieveRequest
            {
                RoutingKey = nameof(T),
                QueueName = nameof(T),
                EventHandler = async (model, ea) =>
               {
                   var body = ea.Body.ToArray();
                   var message = Encoding.UTF8.GetString(body);
                   RabbitMQMessageModel rabbitMQMessageModel = JsonConvert.DeserializeObject<RabbitMQMessageModel>(message);
                   if (rabbitMQMessageModel != null)
                   {
                       T entity = JsonConvert.DeserializeObject<T>(rabbitMQMessageModel.Body);
                       switch (rabbitMQMessageModel.ChangedType)
                       {
                           case (byte)Constants.ChangedType.Create:
                               await this.Add(entity);
                               break;
                           case (byte)Constants.ChangedType.Update:
                               await this.Update(entity);
                               break;
                           case (byte)Constants.ChangedType.Delete:
                               await this.Delete(entity);
                               break;
                           default:
                               break;
                       }
                   }
                   Console.WriteLine($" [x] Received {message}");
               }
            });
        }
        #endregion



    }
}
