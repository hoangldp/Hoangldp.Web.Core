namespace Hoangldp.Web.Core.Data
{
    /// <summary>
    /// Base for repository.
    /// </summary>
    public interface IRepositoryBase
    {
        /// <summary>
        /// Has use transaction.
        /// </summary>
        bool HasTransaction { get; set; }

        /// <summary>
        /// Save change into database.
        /// </summary>
        /// <returns></returns>
        //int Commit();

        /// <summary>
        /// Get <see cref="IDataContext"/>.
        /// </summary>
        /// <returns></returns>
        IDataContext GetContext();

        /// <summary>
        /// Set context.
        /// </summary>
        /// <param name="context"></param>
        void SetContext(IDataContext context);
    }
}
