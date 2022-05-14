using SurveyMe.DomainModels.Request;
using SurveyMe.DomainModels.Response;

namespace SurveyMe.Services.Abstracts;

public interface ISurveyService
{
    Task<PageResponseModel<SurveyWithLinksResponseModel>> GetSurveysAsync(GetPageRequest request, int page = 1);

    Task AddSurveyAsync(SurveyRequestModel surveyModel);

    Task<SurveyResponseModel> GetSurveyAsync(Guid id);

    Task DeleteSurveyAsync(Guid id);

    Task EditSurveyAsync(SurveyResponseModel surveyModel, Guid id);
}