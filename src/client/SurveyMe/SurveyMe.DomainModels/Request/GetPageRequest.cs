using Refit;
using SurveyMe.Common.Pagination;

namespace SurveyMe.DomainModels.Request;

public sealed class GetPageRequest
{
    [AliasAs("nameSearchTerm")]
    public string NameSearchTerm { get; set; } = "";
    
    [AliasAs("sortOrder")]
    public SortOrder SortOrder { get; set; } = SortOrder.Ascending;
    
    [AliasAs("pageSize")]
    public int PageSize { get; set; } = 5;

    [AliasAs("page")]
    public int Page { get; set; } = 1;
}