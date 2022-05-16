using Refit;
using SurveyMe.DomainModels.Request;
using SurveyMe.DomainModels.Response;

namespace SurveyMe.Data.Abstracts;

public interface IUserApi
{
    [Get("/users")]
    Task<PageResponseModel<UserWithSurveysCountResponseModel>> GetUsersAsync([Query]GetPageRequest query, int page = 1);
    
    [Delete("/users/{id}")]
    Task DeleteUserAsync(Guid id);

    [Patch("/users/{id}")]
    Task EditUserAsync([Body]UserDeleteOrEditRequestModel userDeleteOrEditModel, Guid id);

    [Get("/users/{id}")]
    Task<UserDeleteOrEditResponseModel> GetUserAsync(Guid id);
}