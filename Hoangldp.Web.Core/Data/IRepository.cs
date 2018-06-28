using System;
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
    public interface IRepository<TEntity> : IRepositoryBase where TEntity : IEntity, new()
    {
        /// <summary>
        /// Get data by sql normal.
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IQueryable<TEntity> SqlQuery(string sql, params object[] parameters);

        /// <summary>
        /// Execute sql normal.
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        int ExcecuteSqlCommand(string sql, params object[] parameters);

        /// <summary>
        /// Get all entity.
        /// </summary>
        /// <param name="includeExpressions"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeExpressions);

        /// <summary>
        /// Get list entity by condition.
        /// </summary>
        /// <param name="where"></param>
        /// <param name="includeExpressions"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeExpressions);

        /// <summary>
        /// Get all entity but is readonly.
        /// </summary>
        /// <param name="includeExpressions"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetAllReadOnly(params Expression<Func<TEntity, object>>[] includeExpressions);

        /// <summary>
        /// Get list entity by condition.
        /// </summary>
        /// <param name="where"></param>
        /// <param name="includeExpressions"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetListReadOnly(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeExpressions);

        /// <summary>
        /// Get entity by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetById(params object[] id);

        /// <summary>
        /// Get entity by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(params object[] id);

        /// <summary>
        /// Get entity by id.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] id);

        /// <summary>
        /// Get entity by condition.
        /// </summary>
        /// <param name="where"></param>
        /// <param name="includeExpressions"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Source or predicate is null.</exception>
        /// <exception cref="InvalidOperationException">More than one element satisfies the condition in predicate.</exception>
        TEntity Get(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeExpressions);

        /// <summary>
        /// Get entity by condition.
        /// </summary>
        /// <param name="where"></param>
        /// <param name="includeExpressions"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> @where, params Expression<Func<TEntity, object>>[] includeExpressions);

        /// <summary>
        /// Get entity by condition.
        /// </summary>
        /// <param name="where"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="includeExpressions"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> @where, CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includeExpressions);

        /// <summary>
        /// Add entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Update entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Delete entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Delete(TEntity entity);
    }
}
