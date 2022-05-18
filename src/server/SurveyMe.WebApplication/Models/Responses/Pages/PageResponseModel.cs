using SurveyMe.Common.Pagination;

namespace SurveyMe.WebApplication.Models.Responses.Pages;

public sealed class PageResponseModel<T>
{
    public string NameSearchTerm { get; set; }

    public SortOrder SortOrder { get; set; }
    
    public PagedResultResponseModel<T> Page { get; set; }
}