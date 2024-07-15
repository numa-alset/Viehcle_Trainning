using System.Linq.Expressions;
using VehicleApi.Core.Models;

namespace VehicleApi.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T>query,IQueryObjects queryObj,Dictionary<String, Expression<Func<T, object>>> columnsMap)
        {
            if (String.IsNullOrEmpty(queryObj.SortBy)||!columnsMap.ContainsKey(queryObj.SortBy))
                return query;

            if (queryObj.IsSortAscending)
                return query.OrderBy(columnsMap[queryObj.SortBy]);
            else
                return query.OrderByDescending(columnsMap[queryObj.SortBy]);
        }
        public static IQueryable<T> ApllyPaging<T>(this IQueryable<T> query, IQueryObjects queryObj)
        {
            if (queryObj.PageSize <= 0)
                queryObj.PageSize = 10;
            if (queryObj.Page <= 0 )
                queryObj.Page = 1;
            return query.Skip((queryObj.Page - 1) * queryObj.PageSize).Take(queryObj.PageSize);
        }
    }
}
