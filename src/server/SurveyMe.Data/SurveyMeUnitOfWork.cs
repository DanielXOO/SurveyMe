using SurveyMe.Data.Core;
using SurveyMe.Data.Repositories;
using SurveyMe.Data.Repositories.Abstracts;
using SurveyMe.DomainModels.Answers;
using SurveyMe.DomainModels.Roles;
using SurveyMe.DomainModels.Surveys;
using SurveyMe.DomainModels.Users;
using FileInfo = SurveyMe.DomainModels.Files.FileInfo;

namespace SurveyMe.Data;

public class SurveyMeUnitOfWork : UnitOfWork, ISurveyMeUnitOfWork
{
    public IUserRepository Users
        => (IUserRepository) GetRepository<User>();

    public IRoleRepository Roles
        => (IRoleRepository) GetRepository<Role>();

    public IAnswerRepository Answers
        => (IAnswerRepository) GetRepository<SurveyAnswer>();

    public ISurveyRepository Surveys
        => (ISurveyRepository) GetRepository<Survey>();

    public IFileRepository Files
        => (IFileRepository) GetRepository<FileInfo>();


    public SurveyMeUnitOfWork(SurveyMeDbContext dbContext)
        : base(dbContext)
    {
        AddSpecificRepository<User, UserRepository>();
        AddSpecificRepository<Role, RoleRepository>();
        AddSpecificRepository<Survey, SurveyRepository>();
        AddSpecificRepository<SurveyAnswer, AnswerRepository>();
        AddSpecificRepository<FileInfo, FileRepository>();
    }
}