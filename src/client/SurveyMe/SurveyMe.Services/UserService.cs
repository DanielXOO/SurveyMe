using SurveyMe.Data.Abstracts;
using SurveyMe.DomainModels.Queries;
using SurveyMe.DomainModels.Request;
using SurveyMe.DomainModels.Response;
using SurveyMe.Services.Abstracts;

namespace SurveyMe.Services;

public sealed class UserService : IUserService
{
    private readonly IClient _client;
    
    
    public UserService(IClient client)
    {
        _client = client;
    }
    
    
    public async Task<UserEditResponseModel> EditUserAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<PageResponseModel<UserWithSurveysCountResponseModel>> GetUsersPageAsync(GetPageQuery query)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteUserAsync(Guid id)
    {
        var url = $"users/{id}";
        await _client.SendDeleteRequestAsync(url);
    }

    public async Task EditUserAsync(UserEditRequestModel userEditRequestModel, Guid id)
    {
        throw new NotImplementedException();
    }
}