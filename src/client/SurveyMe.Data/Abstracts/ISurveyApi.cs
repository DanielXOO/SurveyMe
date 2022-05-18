using Refit;
using SurveyMe.DomainModels.Request;
using SurveyMe.DomainModels.Response;

namespace SurveyMe.Data.Abstracts;

[Headers("Authorization: Bearer")]
public interface ISurveyApi
{
    [Get("/surveys")]
    Task<PageResponseModel<SurveyResponseModel>> GetSurveysAsync([Query]GetPageRequest query, int page = 1);
    
    [Post("/surveys")]
    Task AddSurveyAsync([Body]SurveyRequestModel surveyModel);

    [Get("/surveys/{id}")]
    public Task<SurveyResponseModel> GetSurveyAsync(Guid id);

    [Patch("/surveys/{id}")]
    public Task EditSurveyAsync([Body]SurveyRequestModel surveyModel, Guid id);

    [Delete("/surveys/{id}")]
    Task DeleteSurveyAsync(Guid id);

    [Post("/surveys/{surveyId}/answers")]
    Task AnswerAsync([Body]SurveyAnswerRequestModel surveyAnswerRequestModel, Guid surveyId);

    [Get("/surveys/{surveyId}/statistics")]
    Task<SurveyAnswersStatisticResponseModel> GetSurveyStatisticAsync(Guid surveyId);
}