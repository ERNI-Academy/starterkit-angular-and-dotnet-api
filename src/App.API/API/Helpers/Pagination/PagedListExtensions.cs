using System.Collections.Generic;
using System.Linq;

namespace App.API.Helpers.Pagination;

/// <summary>
/// The paged list extensions.
/// </summary>
public static class PagedListExtensions
{
    /// <summary>
    /// The to paged the results.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="pageNumber">The page number.</param>
    /// <param name="pageSize">The page size.</param>
    /// <typeparam name="T">The element.</typeparam>
    /// <returns>The <see cref="IEnumerable{T}"/>.</returns>
    public static PagedResultResponse<T> PagedResult<T>(this IQueryable<T> source, int pageNumber, int pageSize)
    {
        var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        var pagedResult = new PagedResultResponse<T> { Elements = items.ToList(), TotalElements = source.Count() };
        return pagedResult;
    }
}
