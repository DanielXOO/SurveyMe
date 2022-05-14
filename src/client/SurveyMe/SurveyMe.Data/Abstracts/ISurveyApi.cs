using Refit;
using SurveyMe.DomainModels.Request;
using SurveyMe.DomainModels.Response;

namespace SurveyMe.Data.Abstracts;

public interface ISurveyApi
{
    [Get("/surveys")]
    Task<PageResponseModel<SurveyWithLinksResponseModel>> GetSurveysAsync([Query]GetPageRequest query, int page = 1);

    [Post("/surveys")]
    Task AddSurveyAsync([Body] SurveyRequestModel surveyModel);
}