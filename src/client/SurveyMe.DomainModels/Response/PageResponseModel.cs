using SurveyMe.Common.Pagination;

namespace SurveyMe.DomainModels.Response;

public class PageResponseModel<T>
{
    public string NameSearchTerm { get; set; }

    public SortOrder SortOrder { get; set; }
    
    
    public PagedResultResponseModel<T> Page { get; set; }
}