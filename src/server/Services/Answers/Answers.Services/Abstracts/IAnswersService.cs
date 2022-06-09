using Answers.Common.Pagination;
using Answers.Models.Answers;

namespace Answers.Services.Abstracts;

public interface IAnswersService
{
    Task<SurveyAnswer> GetAnswerByIdAsync(Guid id);

    Task AddAnswerAsync(SurveyAnswer answer, Guid authorId);

    Task<PagedResult<SurveyAnswer>> GetSurveyAnswersAsync(int currentPage, int pageSize, Guid surveyId);
}