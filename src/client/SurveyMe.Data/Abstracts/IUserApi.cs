using SurveyMe.DomainModels.Request;
using SurveyMe.DomainModels.Response;

namespace SurveyMe.Data.Abstracts;

public interface IUserApi
{
    Task<PageResponseModel<UserWithSurveysCountResponseModel>> GetUsersAsync(string url, GetPageRequest query,
        int page = 1);
    
    Task DeleteUserAsync(string url);

    Task EditUserAsync(string url, UserDeleteOrEditRequestModel userDeleteOrEditModel);

    Task<UserDeleteOrEditResponseModel> GetUserAsync(string url);
}