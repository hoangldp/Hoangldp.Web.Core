using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Hoangldp.Web.Core.Data
{
    /// <summary>
    /// Repository.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : BaseRepository<TEntity>, IDisposable where TEntity : class, IEntity, new()
    {
        private DbContext _context;
        private IDataContext _contextBase;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="contextBase"></param>
        public Repository(IDataContext contextBase)
        {
            _contextBase = contextBase;
        }

        /// <summary>
        /// Lưu lại các thay đổi vào db.
        /// </summary>
        /// <returns></returns>
        //public override int Commit()
        //{
        //    return _context.SaveChanges();
        //}

        /// <summary>
        /// Get <see cref="IDataContext"/>.
        /// </summary>
        /// <returns></returns>
        public override IDataContext GetContext()
        {
            return _contextBase;
        }

        /// <summary>
        /// Set context.
        /// </summary>
        /// <param name="context"></param>
        public override void SetContext(IDataContext context)
        {
            _contextBase = context;
            _context = (DbContext)_contextBase;
        }

        /// <summary>
        /// Get data by sql normal.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public override IQueryable<TEntity> SqlQuery(string sql, params object[] parameters)
        {
            return _context.Set<TEntity>().FromSql<TEntity>(sql, parameters);
        }

        /// <summary>
        /// Execute sql normal.
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        public override int ExcecuteSqlCommand(string sql, params object[] parameters)
        {
            return _context.Database.ExecuteSqlCommand(sql, parameters);
        }

        /// <summary>
        /// Get all entity.
        /// </summary>
        /// <param name="includeExpressions"></param>
        /// <returns></returns>
        public override IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            IQueryable<TEntity> list = _context.Set<TEntity>();
            foreach (Expression<Func<TEntity, object>> includeExpression in includeExpressions)
            {
                list = list.Include(includeExpression);
            }
            return list;
        }

        /// <summary>
        /// Get list entity by condition.
        /// </summary>
        /// <param name="where"></param>
        /// <param name="includeExpressions"></param>
        /// <returns></returns>
        public override IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>> @where, params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            return GetAll(includeExpressions).Where(where);
        }

        /// <summary>
        /// Get all entity but is readonly.
        /// </summary>
        /// <param name="includeExpressions"></param>
        /// <returns></returns>
        public override IQueryable<TEntity> GetAllReadOnly(params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            IQueryable<TEntity> list = _context.Set<TEntity>().AsNoTracking();
            foreach (Expression<Func<TEntity, object>> includeExpression in includeExpressions)
            {
                list = list.Include(includeExpression);
            }
            return list;
        }

        /// <summary>
        /// Get list entity by condition.
        /// </summary>
        /// <param name="where"></param>
        /// <param name="includeExpressions"></param>
        /// <returns></returns>
        public override IQueryable<TEntity> GetListReadOnly(Expression<Func<TEntity, bool>> @where, params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            return GetAllReadOnly(includeExpressions).Where(where);
        }

        /// <summary>
        /// Get entity by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override TEntity GetById(params object[] id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        /// <summary>
        /// Get entity by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override Task<TEntity> GetByIdAsync(params object[] id)
        {
            return _context.Set<TEntity>().FindAsync(id);
        }

        /// <summary>
        /// Get entity by id.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public override Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] id)
        {
            return _context.Set<TEntity>().FindAsync(cancellationToken, id);
        }

        /// <summary>
        /// Get entity by condition.
        /// </summary>
        /// <param name="where"></param>
        /// <param name="includeExpressions"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Source or predicate is null.</exception>
        /// <exception cref="InvalidOperationException">More than one element satisfies the condition in predicate.</exception>
        public override TEntity Get(Expression<Func<TEntity, bool>> @where, params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            return GetAll(includeExpressions).SingleOrDefault(where);
        }

        /// <summary>
        /// Get entity by condition.
        /// </summary>
        /// <param name="where"></param>
        /// <param name="includeExpressions"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Source or predicate is null.</exception>
        /// <exception cref="InvalidOperationException">More than one element satisfies the condition in predicate.</exception>
        public override Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> @where, params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            return GetAll(includeExpressions).SingleOrDefaultAsync(where);
        }

        /// <summary>
        /// Get entity by condition.
        /// </summary>
        /// <param name="where"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="includeExpressions"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Source or predicate is null.</exception>
        /// <exception cref="InvalidOperationException">More than one element satisfies the condition in predicate.</exception>
        public override Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> @where, CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            return GetAll(includeExpressions).SingleOrDefaultAsync(where, cancellationToken);
        }

        /// <summary>
        /// Add entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override TEntity Add(TEntity entity)
        {
            EntityEntry<TEntity> result = _context.Set<TEntity>().Add(entity);

            if (!HasTransaction)
            {
                _context.SaveChanges();
            }
            return result.Entity;
        }

        /// <summary>
        /// Add entity.
        /// </summary>
        /// <param name="entites"></param>
        /// <returns></returns>
        public override IList<TEntity> Add(IEnumerable<TEntity> entites)
        {
            IList<TEntity> result = new List<TEntity>();
            foreach (TEntity entity in entites)
            {
                result.Add(_context.Set<TEntity>().Add(entity).Entity);
            }

            if (!HasTransaction)
            {
                _context.SaveChanges();
            }
            return result.ToList();
        }

        /// <summary>
        /// Update entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override TEntity Update(TEntity entity)
        {
            EntityEntry<TEntity> result = null;
            try
            {
                //TEntity tracked = Context.Set<TEntity>().Find(Context.KeyValuesFor(entity));
                //if (tracked != null)
                //{
                //    Context.Entry(tracked).CurrentValues.SetValues(entity);
                //}

                EntityEntry<TEntity> dbEntityEntry = _context.Entry(entity);
                if (dbEntityEntry.State == EntityState.Detached)
                {
                    result = _context.Set<TEntity>().Attach(entity);
                }
                dbEntityEntry.State = EntityState.Modified;
            }
            catch (System.Exception)
            {
                RefreshAll();
                throw;
            }

            if (!HasTransaction)
            {
                _context.SaveChanges();
            }
            return result.Entity;
        }

        /// <summary>
        /// Update entity.
        /// </summary>
        /// <param name="entites"></param>
        /// <returns></returns>
        public override IList<TEntity> Update(IEnumerable<TEntity> entites)
        {
            List<TEntity> result = new List<TEntity>();
            try
            {
                foreach (TEntity entity in entites)
                {
                    //TEntity tracked = Context.Set<TEntity>().Find(Context.KeyValuesFor(entity));
                    //if (tracked != null)
                    //{
                    //    Context.Entry(tracked).CurrentValues.SetValues(entity);
                    //}

                    EntityEntry<TEntity> dbEntityEntry = _context.Entry(entity);
                    if (dbEntityEntry.State == EntityState.Detached)
                    {
                        result.Add(_context.Set<TEntity>().Attach(entity).Entity);
                    }
                    dbEntityEntry.State = EntityState.Modified;
                }
            }
            catch (System.Exception)
            {
                RefreshAll();
                throw;
            }

            if (!HasTransaction)
            {
                _context.SaveChanges();
            }
            return result;
        }

        /// <summary>
        /// Delete entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override TEntity Delete(TEntity entity)
        {
            EntityEntry<TEntity> result = null;
            EntityEntry<TEntity> dbEntityEntry = _context.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                _context.Set<TEntity>().Attach(entity);
                result = _context.Set<TEntity>().Remove(entity);
            }

            if (!HasTransaction)
            {
                _context.SaveChanges();
            }
            return result.Entity;
        }

        /// <summary>
        /// Delete entity.
        /// </summary>
        /// <param name="entites"></param>
        /// <returns></returns>
        public override IList<TEntity> Delete(IList<TEntity> entites)
        {
            IList<TEntity> result = new List<TEntity>();
            foreach (TEntity entity in entites)
            {
                EntityEntry<TEntity> dbEntityEntry = _context.Entry(entity);
                if (dbEntityEntry.State != EntityState.Deleted)
                {
                    dbEntityEntry.State = EntityState.Deleted;
                }
                else
                {
                    _context.Set<TEntity>().Attach(entity);
                    result.Add(_context.Set<TEntity>().Remove(entity).Entity);
                }
            }

            if (!HasTransaction)
            {
                _context.SaveChanges();
            }
            return result;
        }

        /// <summary>
        /// Reload all entity when has exception
        /// </summary>
        /// <remarks></remarks>
        private void RefreshAll()
        {
            // Get all objects in statemanager with entityKey
            // (context.Refresh will throw an exception otherwise)
            if (_context.ChangeTracker.HasChanges())
            {
                foreach (EntityEntry<TEntity> entityTracker in _context.ChangeTracker.Entries())
                {
                    switch (entityTracker.State)
                    {
                        case EntityState.Modified:
                        case EntityState.Deleted:
                            entityTracker.Reload();
                            break;
                        case EntityState.Added:
                            entityTracker.State = EntityState.Detached;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
