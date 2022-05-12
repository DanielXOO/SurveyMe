using System.Text;
using Newtonsoft.Json;
using SurveyMe.Data.Abstracts;
using SurveyMe.Data.Helpers;
using SurveyMe.Foundation.Exceptions;

namespace SurveyMe.Data;

public class Client : IClient
{
    private readonly HttpClient _client;


    public Client(HttpClient client)
    {
        _client = client;
    }


    public async Task<TResponse> SendGetRequestAsync<TResponse>(string url)
    {
        var fullUrl = new Uri($"{_client.BaseAddress}{url}");
        
        var responseMessage = await _client.GetAsync(fullUrl);
        
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new BadRequestException();
        }
        
        var response = await DeserializeResponseAsync<TResponse>(responseMessage);

        return response;
    }

    public async Task<TResponse> SendGetRequestAsync<TResponse, TQuery>(string url, TQuery query)
    {
        var queryString = query?.ToQuery();
        var fullUrl = new Uri($"{_client.BaseAddress}{url}?{queryString}");
        
        var responseMessage = await _client.GetAsync(fullUrl);
        
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new BadRequestException();
        }

        var response = await DeserializeResponseAsync<TResponse>(responseMessage);
        
        return response;
    }
    
    public async Task SendPatchRequestAsync<TRequest>(string url, TRequest data)
    {
        var fullUrl = new Uri($"{_client.BaseAddress}{url}");

        var serializedData = JsonConvert.SerializeObject(data);
        var content = new StringContent(serializedData, Encoding.Default, "application/json");
        
        var responseMessage = await _client.PatchAsync(fullUrl, content);
        
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new BadRequestException();
        }
    }

    public async Task SendDeleteRequestAsync(string url)
    {
        var fullUrl = new Uri($"{_client.BaseAddress}{url}");

        var responseMessage = await _client.DeleteAsync(fullUrl);
        
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new BadRequestException();
        }
    }

    public async Task<TResponse> SendPostRequestAsync<TRequest, TResponse>(string url, TRequest data)
    {
        var fullUrl = new Uri($"{_client.BaseAddress}{url}");

        var serializedData = JsonConvert.SerializeObject(data);
        var content = new StringContent(serializedData, Encoding.Default, "application/json");
        
        var responseMessage = await _client.PostAsync(fullUrl, content);
        
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new BadRequestException();
        }
        
        var response = await DeserializeResponseAsync<TResponse>(responseMessage);

        return response;
    }

    public async Task SendPostRequestAsync<TRequest>(string url, TRequest data)
    {
        var fullUrl = new Uri($"{_client.BaseAddress}{url}");

        var serializedData = JsonConvert.SerializeObject(data);
        var content = new StringContent(serializedData, Encoding.Default, "application/json");
        
        var responseMessage = await _client.PostAsync(fullUrl, content);

        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new BadRequestException();
        }
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