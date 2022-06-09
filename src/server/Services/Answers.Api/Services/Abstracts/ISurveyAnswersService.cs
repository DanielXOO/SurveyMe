using Answers.Api.Models.Answers;

namespace Answers.Api.Services.Abstracts;

public interface ISurveyAnswersService
{
    Task<SurveyAnswer> GetAnswerByIdAsync(Guid id);

    Task AddAnswerAsync(SurveyAnswer answer, Guid authorId);
}