﻿using SurveyMe.Common.Pagination;

namespace SurveyMe.WebApplication.Models.Queries;

public sealed class GetPageQuery
{
    public string NameSearchTerm { get; set; } = "";
    
    public SortOrder SortOrder { get; set; } = SortOrder.Ascending;
    
    public int CurrentPage { get; set; } = 1;
    
    public int PageSize { get; set; } = 5;
}