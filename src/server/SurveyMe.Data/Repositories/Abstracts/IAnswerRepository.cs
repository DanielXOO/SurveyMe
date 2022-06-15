using SurveyMe.Data.Contracts;
using SurveyMe.DomainModels.Answers;

namespace SurveyMe.Data.Repositories.Abstracts;

public interface IAnswerRepository : IRepository<SurveyAnswer>
{
    Task<SurveyAnswer> GetByIdAsync(Guid id);

    IEnumerable<SurveyAnswer> GetBySurveyId(Guid surveyId);
}