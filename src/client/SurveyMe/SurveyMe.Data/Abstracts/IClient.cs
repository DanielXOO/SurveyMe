namespace SurveyMe.Data.Abstracts;

public interface IClient
{
    Task<TResponse> SendGetRequestAsync<TResponse>(string url);

    Task SendPatchRequestAsync<TRequest>(string url, TRequest data);

    Task SendDeleteRequestAsync(string url);

    Task<TResponse> SendPostRequestAsync<TRequest, TResponse>(string url, TRequest data);
    
    Task SendPostRequestAsync<TRequest>(string url, TRequest data);
}