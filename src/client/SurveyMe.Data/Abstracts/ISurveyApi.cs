using Refit;
using SurveyMe.DomainModels.Request.Answers;
using SurveyMe.DomainModels.Request.Queries;
using SurveyMe.DomainModels.Request.Surveys;
using SurveyMe.DomainModels.Response.Answers;
using SurveyMe.DomainModels.Response.Paggination;
using SurveyMe.DomainModels.Response.Surveys;

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

    [Get("/surveys/{surveyId}/statistics")]
    Task<SurveyAnswersStatisticResponseModel> GetSurveyStatisticAsync(Guid surveyId);
}