using System.Text.Json;
using SurveyMe.Data.Abstracts;

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
        var fullUrl = _client.BaseAddress + url;
        
        var responseMessage = await _client.GetAsync(fullUrl);
        
        if (!responseMessage.IsSuccessStatusCode)
        {
            //TODO: throw exception
        }
        
        // Or use Microsoft.AspNet.WebApi.Client ReadAsAsync<T>
        var response = await DeserializeResponseAsync<TResponse>(responseMessage);

        return response;
    }
    
    public async Task SendPatchRequestAsync<TRequest>(string url, TRequest data)
    {
        var fullUrl = _client.BaseAddress + url;

        var serializedData = JsonSerializer.Serialize(data);
        var content = new StringContent(serializedData);
        
        var responseMessage = await _client.PatchAsync(fullUrl, content);
        
        if (!responseMessage.IsSuccessStatusCode)
        {
            //TODO: throw exception
        }
    }

    public async Task SendDeleteRequestAsync(string url)
    {
        var fullUrl = _client.BaseAddress + url;

        var responseMessage = await _client.DeleteAsync(fullUrl);
        
        if (!responseMessage.IsSuccessStatusCode)
        {
            //TODO: throw exception
        }
    }

    public async Task<TResponse> SendPostRequestAsync<TRequest, TResponse>(string url, TRequest data)
    {
        var fullUrl = _client.BaseAddress + url;

        var serializedData = JsonSerializer.Serialize(data);
        var content = new StringContent(serializedData);
        
        var responseMessage = await _client.PostAsync(fullUrl, content);
        
        if (!responseMessage.IsSuccessStatusCode)
        {
            //TODO: throw exception
        }
        
        // Or use Microsoft.AspNet.WebApi.Client ReadAsAsync<T>
        var response = await DeserializeResponseAsync<TResponse>(responseMessage);

        return response;
    }

    public async Task SendPostRequestAsync<TRequest>(string url, TRequest data)
    {
        var fullUrl = _client.BaseAddress + url;

        var serializedData = JsonSerializer.Serialize(data);
        var content = new StringContent(serializedData);
        
        var responseMessage = await _client.PostAsync(fullUrl, content);
        
        if (!responseMessage.IsSuccessStatusCode)
        {
            //TODO: throw exception
        }
    }


    protected static async Task<TResponse> DeserializeResponseAsync<TResponse>(HttpResponseMessage response)
    {
        var body = await response.Content.ReadAsStringAsync();

        var data = JsonSerializer.Deserialize<TResponse>(body);

        if (data == null)
        {
            //TODO: Throw exception
        }
        
        return data;
    }
}