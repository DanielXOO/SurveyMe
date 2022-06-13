using Answers.Data.Core.Abstracts;
using Answers.Models.Surveys;

namespace Answers.Data.Repositories.Abstracts;

public interface ISurveyRepository : IRepository<Survey>
{
    Task<Survey> GetByIdAsync(Guid id);
}