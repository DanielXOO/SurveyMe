using Answers.Common.Pagination;
using Answers.Data.Core.Abstracts;
using Answers.Models.Answers;

namespace Answers.Data.Repositories.Abstracts;

public interface IAnswersRepository : IRepository<SurveyAnswer>
{
    Task<SurveyAnswer> GetByIdAsync(Guid id);

    Task<PagedResult<SurveyAnswer>> GetSurveyAnswersAsync(int currentPage, int pageSize, Guid surveyId);
}