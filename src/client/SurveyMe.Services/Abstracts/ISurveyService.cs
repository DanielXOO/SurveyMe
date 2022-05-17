using SurveyMe.DomainModels.Request;
using SurveyMe.DomainModels.Response;

namespace SurveyMe.Services.Abstracts;

public interface ISurveyService
{
    Task<PageResponseModel<SurveyResponseModel>> GetSurveysAsync(GetPageRequest request, int page = 1);

    Task AddSurveyAsync(SurveyRequestModel surveyModel);

    Task<SurveyResponseModel> GetSurveyAsync(Guid id);

    Task DeleteSurveyAsync(Guid id);

    Task EditSurveyAsync(SurveyRequestModel surveyModel, Guid id);
    
    Task AnswerAsync(SurveyAnswerRequestModel surveyAnswerRequestModel, Guid surveyId);

    Task<SurveyAnswersStatisticResponseModel> GetSurveyStatisticAsync(Guid surveyId);
}