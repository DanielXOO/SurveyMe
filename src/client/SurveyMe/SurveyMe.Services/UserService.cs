using SurveyMe.Data.Abstracts;
using SurveyMe.DomainModels.Queries;
using SurveyMe.DomainModels.Request;
using SurveyMe.DomainModels.Response;
using SurveyMe.Services.Abstracts;

namespace SurveyMe.Services;

public sealed class UserService : IUserService
{
    private readonly IClient _client;
    private const string ServiceBasePath = "users/";
    
    public UserService(IClient client)
    {
        _client = client;
    }
    
    
    public async Task EditUserAsync(UserDeleteOrEditRequestModel userDeleteOrEditModel, Guid id)
    {
        var url = new Uri($"{ServiceBasePath}{id}");

        await _client.SendPatchRequestAsync(url, userDeleteOrEditModel);
    }

    public async Task<UserEditResponseModel> GetUserAsync(Guid id)
    {
        var url = new Uri($"{ServiceBasePath}{id}");

        var user = await _client.SendGetRequestAsync<UserEditResponseModel>(url);

        return user;
    }

    public async Task<PageResponseModel<UserWithSurveysCountResponseModel>> GetUsersPageAsync(GetPageQuery query)
    {
        var url = new Uri($"{ServiceBasePath}");

        var pagedResult = await _client
            .SendGetRequestAsync<PageResponseModel<UserWithSurveysCountResponseModel>>(url, query);

        return pagedResult;
    }

    public async Task DeleteUserAsync(Guid id)
    {
        var url = new Uri($"{ServiceBasePath}{id}");
        
        await _client.SendDeleteRequestAsync(url);
    }
}