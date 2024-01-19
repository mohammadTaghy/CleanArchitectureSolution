using Application;
using Application.Common.Exceptions;
using Application.Common.Model;
using Application.UseCases;
using Common;
using Common.Extention;
using Domain;
using Domain.Common;
using Domain.Entities;
using Infrastructure.RabbitMQ;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Persistence.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T>
        where T : class, IEntity
    {
        protected readonly ICurrentUserSession _currentUserSession;


        public RepositoryBase(PersistanceDBContext context, ICurrentUserSession currentUserSession)
        {
            Context = context;
            _currentUserSession = currentUserSession;
            ReciveMessage();
        }

        #region Properties
        public CancellationToken CancellationToken { get; set; }
        public PersistanceDBContext Context;
        bool isNoTracking = true;
        int maxId = 0;
        int MaxId
        {
            get
            {
                if (maxId == 0)
                    maxId = (GetAllAsQueryable().Max(x => (int?)x.Id) ?? 0);
                return maxId;
            }
            set { maxId = value; }
        }
        #endregion
        #region CustomMethod
        private bool checkHasSpecificWord(string text)
        {
            return text.ToLower().ContainsAny(" drop ", " delete ", " insert ", " alter ", " update ", " exec");
        }
        private void SetModify(EntityEntry entity)
        {
            if (entity.Entity is IChangeProperty)
            {
                IChangeProperty changeProperty = (IChangeProperty)entity.Entity;
                changeProperty.ModifyDate = DateTimeHelper.CurrentMDateTime;
                changeProperty.ModifyBy = _currentUserSession.UserId;

            }
        }

        private void SetCreate(EntityEntry entity)
        {
            if (entity.Entity is ICreateProperty)
            {
                ICreateProperty changeProperty = (ICreateProperty)entity.Entity;
                changeProperty.CreateDate = DateTimeHelper.CurrentMDateTime;
                changeProperty.CreateBy = _currentUserSession.UserId ?? 0;

            }
        }
        private void SetID(EntityEntry entity)
        {
            if (entity.Entity is IEntity)
            {
                IEntity item = (IEntity)entity.Entity;
                item.Id= ++MaxId;
            }
        }
        protected void SetNoTracking(bool noTraking) => isNoTracking = noTraking;
        protected string GetTableName()
        {
            Type entity = typeof(T);
            var loadTableAttribute = entity.GetCustomAttributes().OfType<LoadTableAttribute>().FirstOrDefault();
            if (loadTableAttribute!=null)
                return loadTableAttribute.TableName;
            return entity.Name;
        }
        #endregion
        #region Get
        public IQueryable<T> GetAllAsQueryable()
        {
            IQueryable<T> query = from p in Context.Set<T>()
                                  select p;
            if (isNoTracking)
                query = query.AsNoTracking();
            return query;
        }

        public IQueryable<T> GetAllAsQueryable(string[] includeList)
        {
            var Query = GetAllAsQueryable();
            if (includeList != null)
                foreach (string include in includeList)
                    Query = Query.Include(include);
            return Query;
        }
        public T Find(int id)
        {
            return GetAllAsQueryable().First(x => x.Id == id);
        }
        public async Task<T> FindAsync(int id)
        {
            return await GetAllAsQueryable().FirstOrDefaultAsync(x => x.Id == id, CancellationToken);
        }
        public T Find(Expression<Func<T, bool>> predicate)
        {
            return GetAllAsQueryable().FirstOrDefault(predicate);
        }
        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await GetAllAsQueryable().FirstOrDefaultAsync(predicate, cancellationToken);
        }
        public async Task<IList<T>> ItemList(Expression<Func<T, bool>> predicate)
        {
            return await GetAllAsQueryable().Where(predicate).ToListAsync(CancellationToken);
        }
        public async Task<bool> AnyEntity(Expression<Func<T, bool>> predicate)
        {
            return await GetAllAsQueryable().AnyAsync(predicate);
        }
        public async Task<Tuple<List<T>, int>> ItemListAdo(ItemListParameter baseGetApiParameter)
        {
            if (checkHasSpecificWord($"{baseGetApiParameter.Filter} {baseGetApiParameter.Orderby} {baseGetApiParameter.Columns}"))
                throw new ValidationException();
           
            try
            {
                string sql = "EXEC dbo.sp_LoadEntityList @tableName, @filter, @orderby, @selectColumn, @top, @skip, @count output";
                int count = 0;
                var outParameter = new SqlParameter { ParameterName = "count", Direction = ParameterDirection.Output, SqlDbType = SqlDbType.Int };
                List<T> result = await Context.Set<T>().FromSqlRaw<T>(sql,
                    new SqlParameter { ParameterName = "tableName", Value = GetTableName(), SqlDbType = SqlDbType.NVarChar },
                    new SqlParameter { ParameterName = "filter", Value = baseGetApiParameter.Filter ?? "", SqlDbType = SqlDbType.NVarChar },
                    new SqlParameter { ParameterName = "orderby", Value = baseGetApiParameter.Orderby ?? "Id", SqlDbType = SqlDbType.NVarChar },
                    new SqlParameter { ParameterName = "selectColumn", Value = baseGetApiParameter.Columns ?? "", SqlDbType = SqlDbType.NVarChar },
                    new SqlParameter { ParameterName = "top", Value = baseGetApiParameter.Top, SqlDbType = SqlDbType.SmallInt },
                    new SqlParameter { ParameterName = "skip", Value = baseGetApiParameter.Skip, SqlDbType = SqlDbType.SmallInt },
                    outParameter).ToListAsync();
                count = (int)outParameter.Value;
                return new Tuple<List<T>, int>(result, count);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(CommonMessage.ValidationMessage);
            }
            
        }
        public async Task<List<T>> ItemListAdo(string procudureName, SqlParameter[] sqlParameters)
        {
            if (sqlParameters.Any(p =>p.Value!=null && checkHasSpecificWord(p.Value.ToString())))
                throw new ValidationException();
            string sqlQuery = $"EXEC {procudureName} ";
            string sign = "";
            foreach (SqlParameter param in sqlParameters)
            {
                sqlQuery += $" {sign}@{param.ParameterName}";
                sign += ", ";
            }

            return await Context.Set<T>().FromSqlRaw<T>(sqlQuery, sqlParameters).ToListAsync();
        }
        public async Task<Tuple<DataTable, int>> ExecuteProcedure(string procudureName, Dictionary<string, string> dic = null)
        {
            DataTable dataTable= new DataTable();
            using (SqlConnection connection = new SqlConnection(IOCManager.GetService<IConfiguration>().GetConnectionString("MainConnectionString")))
            {
                try
                {
                    SqlCommand command = new SqlCommand(procudureName, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    if (dic != null)
                        foreach (var injectParam in dic)
                        {
                            command.Parameters.AddWithValue(injectParam.Key, injectParam.Value);
                        }
                    command.Parameters.Add("@count", SqlDbType.Int).Direction = ParameterDirection.Output;
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    await connection.OpenAsync();
                    da.Fill(dataTable);
                    return  new Tuple<DataTable, int>(dataTable, Convert.ToInt32(command.Parameters["@count"].Value));
                }
                catch (Exception ex)
                {
                    return new Tuple<DataTable, int>(dataTable, 0);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        #endregion
        #region Manipulate

        public virtual async Task<bool> DeleteItem(T entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            await Save();
            return true;
        }
        public virtual async Task<bool> DeleteItem(int id)
        {
            T entity = await FindAsync(id);
            Context.Entry(entity).State = EntityState.Deleted;
            await Save();
            return true;
        }

        public virtual async Task<bool> DeleteItems(IList<T> items)
        {
            foreach (T item in items)
            {
                Context.Entry(item).State = EntityState.Deleted;
            }
            await Save();
            return true;
        }

        public virtual bool DeleteItems(IList<int> ids)
        {
            IList<T> items = ItemList(p => ids.Contains(p.Id)).Result;
            return DeleteItems(items).Result;
        }
         public void Delete(T entity)
        {
            Detach();
            Context.Entry(entity).State = EntityState.Deleted;
        }
        public virtual async Task Insert(T entity)
        {
            Context.Entry(entity).State = EntityState.Added;
            await Save();
        }
        public virtual async Task Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await Save();
        }
        public void Add(T entity)
        {
            Detach();
            Context.Entry(entity).State = EntityState.Added;
        }
       
        public void Attach(T entity)
        {
            Detach();
            Context.Entry(entity).State = EntityState.Modified;
        }
        public void Detach()
        {
            var changedEntriesCopy = Context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Unchanged).ToList();
            Task.Run(() =>
            {
                Parallel.ForEach<EntityEntry>(changedEntriesCopy, entity =>
                 {
                     entity.State = EntityState.Detached;
                 });
            });
        }
        public async Task Save()
        {
            var entities = Context.ChangeTracker.Entries().Where(p => p.State != EntityState.Unchanged);
            
            Parallel.ForEach(entities, entity =>
             {
                 FillEntityProperty(entity);
             });

            try
            {
                await Context.SaveChangesAsync(CancellationToken);
            }

            catch (Exception ex)
            {
                //must be log
                var error = ex.GetBaseException();
                throw;
            }

        }
        private void FillEntityProperty(EntityEntry entity)
        {
            try
            {
                if (entity.State == EntityState.Added)
                {
                    SetID(entity);
                    SetCreate(entity);

                }
                else if (entity.State == EntityState.Modified)
                {
                    SetModify(entity);
                }

            }

            catch (Exception ex)
            {
                throw;
            }
        }

        public void ReciveMessage()
        {
            new RabbitMQUtility().RecieveMessage("CQRS", (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($" [x] Received {message}");
            });
        }
        #endregion

    }
}
