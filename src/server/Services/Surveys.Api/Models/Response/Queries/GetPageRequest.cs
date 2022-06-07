using Surveys.Api.Common.Pagination;

namespace Surveys.Api.Models.Response.Queries;

public sealed class GetPageRequest
{
    public string? NameSearchTerm { get; set; } = "";
    
    public SortOrder SortOrder { get; set; } = SortOrder.Ascending;
    
    public int PageSize { get; set; } = 5;

    public int Page { get; set; } = 1;
}