using Refit;
using SurveyMe.DomainModels.Request.Answers;

namespace SurveyMe.Data.Abstracts;

public interface IAnswersApi
{
    [Post("/surveys/{surveyId}/answers")]
    Task AnswerAsync([Body]SurveyAnswerRequestModel surveyAnswerRequestModel, Guid surveyId);
}