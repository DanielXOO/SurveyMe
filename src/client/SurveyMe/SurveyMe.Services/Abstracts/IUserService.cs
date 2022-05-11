using SurveyMe.DomainModels.Queries;
using SurveyMe.DomainModels.Request;
using SurveyMe.DomainModels.Response;

namespace SurveyMe.Services.Abstracts;

public interface IUserService
{
    Task<PageResponseModel<UserWithSurveysCountResponseModel>> GetUsersPageAsync(GetPageQuery query);

    Task DeleteUserAsync(Guid id);

    Task EditUserAsync(UserDeleteOrEditRequestModel userDeleteOrEditModel, Guid id);

    Task<UserEditResponseModel> GetUserAsync(Guid id);
}