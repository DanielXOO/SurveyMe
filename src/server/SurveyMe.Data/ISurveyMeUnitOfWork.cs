using SurveyMe.Data.Repositories;
using SurveyMe.Data.Repositories.Abstracts;
using SurveyMe.Repositories;

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