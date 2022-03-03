using System.Collections.Generic;

namespace App.API.Helpers.Pagination;

/// <summary>
/// The paged result response.
/// </summary>
/// <typeparam name="T">The elements paged</typeparam>
public class PagedResultResponse<T>
{
    /// <summary>
    /// Gets or sets  the total count.
    /// </summary>
    public int TotalElements { get; set; }

    /// <summary>
    /// Gets or sets the elements.
    /// </summary>
    public IEnumerable<T> Elements { get; set; }
}
