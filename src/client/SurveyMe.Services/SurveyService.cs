using SurveyMe.Data.Abstracts;
using SurveyMe.DomainModels.Request.Answers;
using SurveyMe.DomainModels.Request.Queries;
using SurveyMe.DomainModels.Request.Surveys;
using SurveyMe.DomainModels.Response.Answers;
using SurveyMe.DomainModels.Response.Paggination;
using SurveyMe.DomainModels.Response.Surveys;
using SurveyMe.Services.Abstracts;

namespace SurveyMe.Services;

public class SurveyService : ISurveyService
{
    private readonly ISurveyApi _surveyApi;
    

    public SurveyService(ISurveyApi surveyApi)
    {
        _surveyApi = surveyApi;
    }
    
    
    public async Task<PageResponseModel<SurveyResponseModel>> GetSurveysAsync(GetPageRequest request, int page = 1)
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
        var user = await _surveyApi.GetSurveyAsync(id);

        return user;
    }

    public async Task DeleteSurveyAsync(Guid id)
    {
        await _surveyApi.DeleteSurveyAsync(id);
    }

    public async Task EditSurveyAsync(SurveyRequestModel surveyModel, Guid id)
    {
        await _surveyApi.EditSurveyAsync(surveyModel, id);
    }

    public async Task AnswerAsync(SurveyAnswerRequestModel surveyAnswerRequestModel, Guid surveyId)
    {
        await _surveyApi.AnswerAsync(surveyAnswerRequestModel, surveyId);
    }

    public async Task<SurveyAnswersStatisticResponseModel> GetSurveyStatisticAsync(Guid surveyId)
    {
        var statistic = await _surveyApi.GetSurveyStatisticAsync(surveyId);

        return statistic;
    }
}