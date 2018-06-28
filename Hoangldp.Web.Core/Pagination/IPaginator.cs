using System.Collections.Generic;

namespace Hoangldp.Web.Core.Pagination
{
    public interface IPaginator<T> : IList<T>
    {
        int Total { get; }

        int PerPage { get; }

        /// <summary>
        /// Get the current page.
        /// </summary>
        int CurrentPage { get; }

        /// <summary>
        /// Get the last page.
        /// </summary>
        int LastPage { get; }
    }
}