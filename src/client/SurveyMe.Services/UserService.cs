using SurveyMe.Data.Abstracts;
using SurveyMe.DomainModels.Request.Queries;
using SurveyMe.DomainModels.Request.Users;
using SurveyMe.DomainModels.Response.Paggination;
using SurveyMe.DomainModels.Response.Users;
using SurveyMe.Services.Abstracts;

namespace SurveyMe.Services;

public sealed class UserService : IUserService
{
    private readonly IUserApi _userApi;
    
    private const string ServiceBasePath = "/users";
    
    
    public UserService(IUserApi userApi)
    {
        _userApi = userApi;
    }
    
    
    public async Task EditUserAsync(UserDeleteOrEditRequestModel userDeleteOrEditModel, Guid id)
    {
        await _userApi.EditUserAsync(userDeleteOrEditModel, id);
    }

    public async Task<UserDeleteOrEditResponseModel> GetUserAsync(Guid id)
    {
        var user = await _userApi.GetUserAsync(id);

        return user;
    }

    public async Task<PageResponseModel<UserWithSurveysCountResponseModel>> GetUsersAsync(GetPageRequest request, int page = 1)
    {
        var pagedResult = await _userApi.GetUsersAsync(request, page);

        return pagedResult;
    }

    public async Task DeleteUserAsync(Guid id)
    {
        await _userApi.DeleteUserAsync(id);
    }
}