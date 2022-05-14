using SurveyMe.Data.Abstracts;
using SurveyMe.DomainModels.Request;
using SurveyMe.DomainModels.Response;
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
        var url = $"{ServiceBasePath}/{id}";

        await _userApi.EditUserAsync(url, userDeleteOrEditModel);
    }

    public async Task<UserDeleteOrEditResponseModel> GetUserAsync(Guid id)
    {
        var url = $"{ServiceBasePath}/{id}";

        var user = await _userApi.GetUserAsync(url);

        return user;
    }

    public async Task<PageResponseModel<UserWithSurveysCountResponseModel>> GetUsersAsync(GetPageRequest request, int page = 1)
    {
        var pagedResult = await _userApi
            .GetUsersAsync(ServiceBasePath, request);

        return pagedResult;
    }

    public async Task DeleteUserAsync(Guid id)
    {
        var url = $"{ServiceBasePath}/{id}";
        
        await _userApi.DeleteUserAsync(url);
    }
}