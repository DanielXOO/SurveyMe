using SurveyMe.DomainModels;
using SurveyMe.Repositories;

namespace SurveyMe.Data.Repositories
{
    public interface IAnswerRepository : IRepository<SurveyAnswer>
    {
        Task<SurveyAnswer> GetByIdAsync(Guid id);
    }
}