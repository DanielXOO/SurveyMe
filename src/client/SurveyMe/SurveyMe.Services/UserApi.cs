using SurveyMe.Data.Abstracts;
using SurveyMe.DomainModels.Request;
using SurveyMe.DomainModels.Response;
using SurveyMe.Services.Abstracts;

namespace SurveyMe.Services;

public sealed class UserApi : IUserApi
{
    private readonly IClient _client;
    private const string ServiceBasePath = "/users";
    
    public UserApi(IClient client)
    {
        _client = client;
    }
    
    
    public async Task EditUserAsync(UserDeleteOrEditRequestModel userDeleteOrEditModel, Guid id)
    {
        var url = $"{ServiceBasePath}/{id}";

        await _client.SendPatchRequestAsync(url, userDeleteOrEditModel);
    }

    public async Task<UserDeleteOrEditResponseModel> GetUserAsync(Guid id)
    {
        var url = $"{ServiceBasePath}/{id}";

        var user = await _client.SendGetRequestAsync<UserDeleteOrEditResponseModel>(url);

        return user;
    }

    public async Task<PageResponseModel<UserWithSurveysCountResponseModel>> GetUsersAsync(GetPageRequest request)
    {
        //TODO: generic
        var pagedResult = await _client
            .SendGetRequestAsync<PageResponseModel<UserWithSurveysCountResponseModel>, GetPageRequest>(ServiceBasePath, request);

        return pagedResult;
    }

    public async Task DeleteUserAsync(Guid id)
    {
        var url = $"{ServiceBasePath}/{id}";
        
        await _client.SendDeleteRequestAsync(url);
    }
}