using Answers.Models.Surveys;

namespace Answers.Services.Abstracts;

public interface ISurveysService
{
    Task DeleteSurveyAsync(Survey survey);

    Task<Survey> GetSurveyByIdAsync(Guid id);

    Task AddSurveyAsync(Survey survey);

    Task UpdateSurveyAsync(Survey survey);
}