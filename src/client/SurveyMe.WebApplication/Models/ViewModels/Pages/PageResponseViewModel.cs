using SurveyMe.Common.Pagination;

namespace SurveyMe.WebApplication.Models.ViewModels.Pages
{
    public sealed class PageResponseViewModel<T>
    {
        public string NameSearchTerm { get; set; }

        public SortOrder SortOrder { get; set; }
        
        public PagedResultViewModel<T> Page { get; set; }
    }
}