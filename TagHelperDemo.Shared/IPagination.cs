namespace TagHelperDemo.Shared;

public interface IPagination
{
    int CurrentPage { get; }
    int TotalPages { get; }
    int PageSize { get; }
    int TotalCount { get; }
    bool HasPrevious { get; }
    bool HasNext { get; }
}