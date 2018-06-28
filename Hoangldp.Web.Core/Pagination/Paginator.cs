using System.Collections.Generic;

namespace Hoangldp.Web.Core.Pagination
{
    public class Paginator<T> : List<T>, IPaginator<T>
    {
        public int Total { get; set; }

        public int PerPage { get; set; }

        /// <summary>
        /// Get the current page.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Get the last page.
        /// </summary>
        /// <returns></returns>
        public int LastPage { get; set; }

        public Paginator() : base() { }

        public Paginator(IEnumerable<T> collection) : base(collection) { }

        public Paginator(int capacity) : base(capacity) { }
    }
}