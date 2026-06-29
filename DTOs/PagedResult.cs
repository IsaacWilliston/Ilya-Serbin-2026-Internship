namespace SeatsReservationDotNet.DTOs;

/// <summary>
/// Paginated response wrapper matching the Spring Boot <c>Page&lt;T&gt;</c> shape.
/// </summary>
/// <typeparam name="T">Type of items in the page.</typeparam>
public class PagedResult<T>
{
    /// <summary>Items on the current page.</summary>
    public IEnumerable<T> Content { get; init; }

    /// <summary>Total number of items across all pages.</summary>
    public long TotalElements { get; init; }

    /// <summary>Total number of pages.</summary>
    public int TotalPages { get; init; }

    /// <summary>Requested page size.</summary>
    public int Size { get; init; }

    /// <summary>Zero-based index of the current page.</summary>
    public int Number { get; init; }

    /// <summary>Whether this is the first page.</summary>
    public bool First { get; init; }

    /// <summary>Whether this is the last page.</summary>
    public bool Last { get; init; }

    /// <summary>Builds a paginated result from a slice of data.</summary>
    /// <param name="content">Items on the current page.</param>
    /// <param name="totalElements">Total item count.</param>
    /// <param name="page">Zero-based page index.</param>
    /// <param name="size">Page size.</param>
    public PagedResult(IEnumerable<T> content, long totalElements, int page, int size)
    {
        Content = content;
        TotalElements = totalElements;
        Size = size;
        Number = page;
        TotalPages = size > 0 ? (int)Math.Ceiling(totalElements / (double)size) : 0;
        First = page == 0;
        Last = page >= TotalPages - 1;
    }
}
