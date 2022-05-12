using Refit;
using SurveyMe.DomainModels.Request;
using SurveyMe.DomainModels.Response;

namespace SurveyMe.Services.Abstracts;

public interface ISurveyApi
{
    [Get("/surveys/{page}")]
    Task<PageResponseModel<SurveyResponseModel>> GetSurveysPageAsync([Query] GetPageRequest request, int page = 1);
}