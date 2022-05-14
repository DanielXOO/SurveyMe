namespace SurveyMe.WebApplication.Models.ViewModels;

public sealed class PagedResultViewModel<TModel>
{
    public IReadOnlyCollection<TModel> Items { get; set; }

    public int PageSize { get; set; } = 5;

    public int CurrentPage { get; set; } = 1;
    
    public bool HasPrevious => CurrentPage > 1;

    public bool HasNext => CurrentPage < TotalPages;
    
    public int TotalItems { get; set; }
    
    public int TotalPages => (int) Math.Ceiling(TotalItems / (double) PageSize);
}