using SurveyMe.DomainModels.Queries;
using SurveyMe.DomainModels.Request;
using SurveyMe.DomainModels.Response;

namespace SurveyMe.Services.Abstracts;

public interface IUserService
{
    Task<UserEditResponseModel> EditUserAsync(Guid id);

    Task<PageResponseModel<UserWithSurveysCountResponseModel>> GetUsersPageAsync(GetPageQuery query);

    Task DeleteUserAsync(Guid id);

    Task EditUserAsync(UserEditRequestModel userEditRequestModel, Guid id);
}