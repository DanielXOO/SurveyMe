using SurveyMe.DomainModels.Request.Queries;
using SurveyMe.DomainModels.Request.Users;
using SurveyMe.DomainModels.Response.Paggination;
using SurveyMe.DomainModels.Response.Users;

namespace SurveyMe.Services.Abstracts;

public interface IUserService
{
    Task<PageResponseModel<UserWithSurveysCountResponseModel>> GetUsersAsync(GetPageRequest request, int page = 1);

    Task DeleteUserAsync(Guid id);

    Task EditUserAsync(UserDeleteOrEditRequestModel userDeleteOrEditModel, Guid id);

    Task<UserDeleteOrEditResponseModel> GetUserAsync(Guid id);
}