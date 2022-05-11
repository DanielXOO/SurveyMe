using SurveyMe.Common.Pagination;
using SurveyMe.DomainModels.Common;

namespace SurveyMe.DomainModels.Queries;

public sealed class GetPageQuery
{
    public string NameSearchTerm { get; set; } = "";
    
    public SortOrder SortOrder { get; set; } = SortOrder.Ascending;
    
    public int PageSize { get; set; } = 5;

    public int Page { get; set; } = 1;
}