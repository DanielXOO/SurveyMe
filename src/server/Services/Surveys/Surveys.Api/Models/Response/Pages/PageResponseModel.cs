using SurveyMe.Common.Pagination;

namespace Surveys.Api.Models.Response.Pages;

public sealed class PageResponseModel<T>
{
    public string NameSearchTerm { get; set; }

    public SortOrder SortOrder { get; set; }
    
    public PagedResultResponseModel<T> Page { get; set; }
}