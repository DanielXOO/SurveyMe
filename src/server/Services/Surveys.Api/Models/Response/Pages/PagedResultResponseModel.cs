namespace Surveys.Api.Models.Response.Pages;

public sealed class PagedResultResponseModel<TModel>
{
    public IReadOnlyCollection<TModel> Items { get; set; }

    public int PageSize { get; set; } = 5;

    public int CurrentPage { get; set; } = 1;
    
    public int TotalItems { get; set; }
}