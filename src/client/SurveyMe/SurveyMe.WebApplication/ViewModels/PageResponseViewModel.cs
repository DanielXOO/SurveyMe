using SurveyMe.Common.Pagination;

namespace SurveyMe.WebApplication.ViewModels
{
    public sealed class PageResponseViewModel<T>
    {
        public PagedResult<T> Items { get; set; }

        public string NameSearchTerm { get; set; }

        public SortOrder SortOrder { get; set; }
    }
}