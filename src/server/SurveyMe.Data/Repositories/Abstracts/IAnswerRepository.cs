using SurveyMe.Data.Contracts;
using SurveyMe.Data.Models;
using SurveyMe.DomainModels;

namespace SurveyMe.Data.Repositories.Abstracts;

public interface IAnswerRepository : IRepository<SurveyAnswer>
{
    Task<SurveyAnswer> GetByIdAsync(Guid id);

    Task<SurveyAnswersStatistic> GetSurveyStatistic(Survey survey);
}