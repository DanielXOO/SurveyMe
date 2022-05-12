using SurveyMe.DomainModels.Request;
using SurveyMe.DomainModels.Response;

namespace SurveyMe.Services.Abstracts;

public interface IUserApi
{
    Task<PageResponseModel<UserWithSurveysCountResponseModel>> GetUsersAsync(GetPageRequest request);

    Task DeleteUserAsync(Guid id);

    Task EditUserAsync(UserDeleteOrEditRequestModel userDeleteOrEditModel, Guid id);

    Task<UserDeleteOrEditResponseModel> GetUserAsync(Guid id);
}