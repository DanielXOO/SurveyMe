using SurveyPerson.Models.Options;

namespace SurveyPerson.Data.Dapper.Abstracts;

public interface IOptionsRepository
{
    Task CreateAsync(SurveyPersonOptions options);

    Task<SurveyPersonOptions> GetByIdAsync(Guid id);

    Task<SurveyPersonOptions> GetBySurveyIdAsync(Guid surveyId);
    
    Task UpdateAsync(SurveyPersonOptions options);

    Task DeleteAsync(Guid id);
}