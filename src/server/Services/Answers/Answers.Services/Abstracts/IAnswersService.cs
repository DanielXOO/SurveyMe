using Answers.Models.Answers;
using Answers.Services.Models.Surveys;
using SurveyMe.Common.Pagination;

namespace Answers.Services.Abstracts;

public interface IAnswersService
{
    Task<SurveyAnswer> GetAnswerByIdAsync(Guid id);

    Task AddAnswerAsync(SurveyAnswer answer, Guid authorId);

    Task<PagedResult<SurveyAnswerResult>> GetSurveyAnswersAsync(int currentPage, int pageSize, Guid surveyId);
}