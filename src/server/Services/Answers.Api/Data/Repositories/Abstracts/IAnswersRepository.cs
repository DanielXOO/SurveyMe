using Answers.Api.Data.Core.Abstracts;
using Answers.Api.Models.Answers;

namespace Answers.Api.Data.Repositories.Abstracts;

public interface IAnswersRepository : IRepository<SurveyAnswer>
{
    Task<SurveyAnswer> GetByIdAsync(Guid id);
}