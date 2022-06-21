using SurveyPerson.Models.Options;

namespace SurveyPerson.Services.Abstracts;

public interface IOptionsService
{
    Task CreateAsync(SurveyPersonOptions options);
    
    Task<SurveyPersonOptions> GetByIdAsync(Guid id);

    Task<SurveyPersonOptions> GetBySurveyIdAsync(Guid surveyId);
    
    Task UpdateAsync(SurveyPersonOptions options);

    Task DeleteAsync(Guid id);
}