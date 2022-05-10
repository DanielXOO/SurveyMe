using SurveyMe.Data.Contracts;
using SurveyMe.Data.Repositories.Abstracts;

namespace SurveyMe.Data;

public interface 
    ISurveyMeUnitOfWork : IUnitOfWork
{
    IUserRepository Users { get; }

    IRoleRepository Roles { get; }

    IAnswerRepository Answers { get; }

    ISurveyRepository Surveys { get; }

    IFileRepository Files { get; }
}