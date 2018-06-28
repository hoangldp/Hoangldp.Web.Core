using System;
using System.Linq;

namespace Hoangldp.Web.Core.Pagination
{
    public static class QueryableExtension
    {
        public static IPaginator<T> Paginator<T>(this IQueryable<T> queryable, int perPage, int currentPage)
        {
            if (perPage < 1)
                throw new ArgumentOutOfRangeException(nameof(perPage), perPage, "Item per page cannot be below 1.");
            if (currentPage < 1)
                throw new ArgumentOutOfRangeException(nameof(currentPage), currentPage, "Current page cannot be less than 1.");

            int total = queryable.Count();
            if (total == 0) return new Paginator<T>();

            int lastPage = total / perPage;
            if (total % perPage > 0) lastPage = lastPage + 1;

            int take;
            int skip;

            if (currentPage >= lastPage)
            {
                take = total;
                skip = (lastPage - 1) * perPage;
            }
            else
            {
                take = currentPage * perPage;
                skip = (currentPage - 1) * perPage;
            }

            Paginator<T> result = new Paginator<T>();
            result.AddRange(queryable.Take(take).Skip(skip));
            result.Total = total;
            result.PerPage = perPage;
            result.CurrentPage = currentPage;
            result.LastPage = lastPage;

            return result;
        }
    }
}