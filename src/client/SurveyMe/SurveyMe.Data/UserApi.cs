using System.Text;
using Newtonsoft.Json;
using SurveyMe.Common.Exceptions;
using SurveyMe.Data.Abstracts;
using SurveyMe.Data.Helpers;
using SurveyMe.DomainModels.Request;
using SurveyMe.DomainModels.Response;

namespace SurveyMe.Data;

public class UserApi : IUserApi
{
    private readonly HttpClient _client;


    public UserApi(HttpClient client)
    {
        _client = client;
    }


    public async Task<PageResponseModel<UserWithSurveysCountResponseModel>> GetUsersAsync(string url,
        GetPageRequest query, int page = 1)
    {
        var queryString = query?.ToQuery();
        var fullUrl = new Uri($"{_client.BaseAddress}{url}?{queryString}&page={page}");
        
        var responseMessage = await _client.GetAsync(fullUrl);
        
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new BadRequestException();
        }

        var response = await 
            DeserializeResponseAsync<PageResponseModel<UserWithSurveysCountResponseModel>>(responseMessage);
        
        return response;
    }

    public async Task DeleteUserAsync(string url)
    {
        var fullUrl = new Uri($"{_client.BaseAddress}{url}");

        var responseMessage = await _client.DeleteAsync(fullUrl);
        
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new BadRequestException();
        }
    }

    public async Task EditUserAsync(string url, UserDeleteOrEditRequestModel userDeleteOrEditModel)
    {
        var fullUrl = new Uri($"{_client.BaseAddress}{url}");

        var serializedData = JsonConvert.SerializeObject(userDeleteOrEditModel);
        var content = new StringContent(serializedData, Encoding.Default, "application/json");
        
        var responseMessage = await _client.PatchAsync(fullUrl, content);
        
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new BadRequestException();
        }
    }

    public async Task<UserDeleteOrEditResponseModel> GetUserAsync(string url)
    {
        var fullUrl = new Uri($"{_client.BaseAddress}{url}");
        
        var responseMessage = await _client.GetAsync(fullUrl);
        
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new BadRequestException();
        }
        
        var response = await DeserializeResponseAsync<UserDeleteOrEditResponseModel>(responseMessage);

        return response;
    }
    

    private static async Task<TResponse> DeserializeResponseAsync<TResponse>(HttpResponseMessage response)
    {
        var body = await response.Content.ReadAsStringAsync();

        var data = JsonConvert.DeserializeObject<TResponse>(body);

        if (data == null)
        {
            throw new BadRequestException();
        }
        
        return data;
    }
}