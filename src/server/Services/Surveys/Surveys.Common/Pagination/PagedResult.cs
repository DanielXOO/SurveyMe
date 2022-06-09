namespace Surveys.Common.Pagination;

public class PagedResult<TModel>
{
    public int PageSize { get; set; }

    public int CurrentPage { get; set; }

    public int TotalItems { get; set; }

    public int TotalPages => (int) Math.Ceiling(TotalItems / (double) PageSize);

    public bool HasPrevious => CurrentPage > 1;

    public bool HasNext => CurrentPage < TotalPages;

    public IReadOnlyCollection<TModel> Items { get; set; }


    public PagedResult(IReadOnlyCollection<TModel> items, int pageSize, int currentPage, int totalItems)
    {
        PageSize = pageSize;
        CurrentPage = currentPage;
        Items = items;
        TotalItems = totalItems;
    }
}