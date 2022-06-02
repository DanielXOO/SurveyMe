using Refit;
using SurveyMe.DomainModels.Request.Queries;
using SurveyMe.DomainModels.Request.Users;
using SurveyMe.DomainModels.Response;
using SurveyMe.DomainModels.Response.Paggination;
using SurveyMe.DomainModels.Response.Users;

namespace SurveyMe.Data.Abstracts;

[Headers("Authorization: Bearer")]
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