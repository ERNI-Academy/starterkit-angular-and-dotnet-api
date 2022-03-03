namespace App.API.Helpers.Pagination;

/// <summary>
/// The page query info.
/// </summary>
public class PageInfo
{
    /// <summary>
    /// The maxpagesize.
    /// </summary>
    public const int MaxPageSize = 100;

    /// <summary>
    /// The defaultpagesize.
    /// </summary>
    public const int DefaultPageSize = 20;

    /// <summary>
    /// Gets or sets the page.
    /// </summary>
    public int? Page { get; set; }

    /// <summary>
    /// Gets or sets the page size.
    /// </summary>
    public int PageSize { get; set; } = DefaultPageSize;
}
