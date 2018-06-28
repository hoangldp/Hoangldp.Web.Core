using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Hoangldp.Web.Core.Data
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : IEntity, new()
    {
        public bool HasTransaction { get; set; }

        /// <summary>
        /// Save change into database.
        /// </summary>
        /// <returns></returns>
        //public abstract int Commit();

        /// <summary>
        /// Get <see cref="IDataContext"/>.
        /// </summary>
        /// <returns></returns>
        public abstract IDataContext GetContext();

        /// <summary>
        /// Set context.
        /// </summary>
        /// <param name="context"></param>
        public abstract void SetContext(IDataContext context);

        /// <summary>
        /// Get data by sql normal.
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public abstract IQueryable<TEntity> SqlQuery(string sql, params object[] parameters);

        /// <summary>
        /// Execute sql normal.
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        public abstract int ExcecuteSqlCommand(string sql, params object[] parameters);

        /// <summary>
        /// Get all entity.
        /// </summary>
        /// <param name="includeExpressions"></param>
        /// <returns></returns>
        public abstract IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeExpressions);

        /// <summary>
        /// Get list entity by condition.
        /// </summary>
        /// <param name="where"></param>
        /// <param name="includeExpressions"></param>
        /// <returns></returns>
        public abstract IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>> @where, params Expression<Func<TEntity, object>>[] includeExpressions);

        /// <summary>
        /// Get all entity but is readonly.
        /// </summary>
        /// <param name="includeExpressions"></param>
        /// <returns></returns>
        public abstract IQueryable<TEntity> GetAllReadOnly(params Expression<Func<TEntity, object>>[] includeExpressions);

        /// <summary>
        /// Get list entity by condition.
        /// </summary>
        /// <param name="where"></param>
        /// <param name="includeExpressions"></param>
        /// <returns></returns>
        public abstract IQueryable<TEntity> GetListReadOnly(Expression<Func<TEntity, bool>> @where, params Expression<Func<TEntity, object>>[] includeExpressions);

        /// <summary>
        /// Get entity by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract TEntity GetById(params object[] id);

        /// <summary>
        /// Get entity by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract Task<TEntity> GetByIdAsync(params object[] id);

        /// <summary>
        /// Get entity by id.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] id);

        /// <summary>
        /// Get entity by condition.
        /// </summary>
        /// <param name="where"></param>
        /// <param name="includeExpressions"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Source or predicate is null.</exception>
        /// <exception cref="InvalidOperationException">More than one element satisfies the condition in predicate.</exception>
        public abstract TEntity Get(Expression<Func<TEntity, bool>> @where, params Expression<Func<TEntity, object>>[] includeExpressions);

        /// <summary>
        /// Get entity by condition.
        /// </summary>
        /// <param name="where"></param>
        /// <param name="includeExpressions"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Source or predicate is null.</exception>
        /// <exception cref="InvalidOperationException">More than one element satisfies the condition in predicate.</exception>
        public abstract Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> @where, params Expression<Func<TEntity, object>>[] includeExpressions);

        /// <summary>
        /// Get entity by condition.
        /// </summary>
        /// <param name="where"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="includeExpressions"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Source or predicate is null.</exception>
        /// <exception cref="InvalidOperationException">More than one element satisfies the condition in predicate.</exception>
        public abstract Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> @where, CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includeExpressions);

        /// <summary>
        /// Add entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public abstract TEntity Add(TEntity entity);

        /// <summary>
        /// Add entity.
        /// </summary>
        /// <param name="entites"></param>
        /// <returns></returns>
        public abstract IList<TEntity> Add(IEnumerable<TEntity> entites);

        /// <summary>
        /// Update entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public abstract TEntity Update(TEntity entity);

        /// <summary>
        /// Update entity.
        /// </summary>
        /// <param name="entites"></param>
        /// <returns></returns>
        public abstract IList<TEntity> Update(IEnumerable<TEntity> entites);

        /// <summary>
        /// Delete entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public abstract TEntity Delete(TEntity entity);

        /// <summary>
        /// Delete entity.
        /// </summary>
        /// <param name="entites"></param>
        /// <returns></returns>
        public abstract IList<TEntity> Delete(IList<TEntity> entites);
    }
}
