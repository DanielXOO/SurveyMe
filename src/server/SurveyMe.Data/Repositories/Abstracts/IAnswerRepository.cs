using SurveyMe.DomainModels;
using SurveyMe.Repositories;

namespace SurveyMe.Data.Repositories.Abstracts
{
    public interface IAnswerRepository : IRepository<SurveyAnswer>
    {
        Task<SurveyAnswer> GetByIdAsync(Guid id);
    }
}