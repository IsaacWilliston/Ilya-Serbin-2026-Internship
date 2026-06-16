namespace SeatsReservationDotNet.DTOs;

public class PagedResult<T>
{
    public IEnumerable<T> Content { get; init; }
    public long TotalElements { get; init; }
    public int TotalPages { get; init; }
    public int Size { get; init; }
    public int Number { get; init; }
    public bool First { get; init; }
    public bool Last { get; init; }

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
