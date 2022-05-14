using SurveyMe.Data.Abstracts;
using SurveyMe.DomainModels.Request;
using SurveyMe.DomainModels.Response;
using SurveyMe.Services.Abstracts;

namespace SurveyMe.Services;

public class SurveyService : ISurveyService
{
    private readonly ISurveyApi _surveyApi;
    

    public SurveyService(ISurveyApi surveyApi)
    {
        _surveyApi = surveyApi;
    }
    
    
    public async Task<PageResponseModel<SurveyWithLinksResponseModel>> GetSurveysAsync(GetPageRequest request, int page = 1)
    {
       var surveys = await _surveyApi.GetSurveysAsync(request, page);

       return surveys;
    }

    public async Task AddSurveyAsync(SurveyRequestModel surveyModel)
    {
        await _surveyApi.AddSurveyAsync(surveyModel);
    }

    public async Task<SurveyResponseModel> GetSurveyAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteSurveyAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task EditSurveyAsync(SurveyResponseModel surveyModel, Guid id)
    {
        throw new NotImplementedException();
    }
}