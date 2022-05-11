namespace SurveyMe.Data.Abstracts;

public interface IClient
{
    Task<TResponse> SendGetRequestAsync<TResponse>(Uri url);
    
    Task<TResponse> SendGetRequestAsync<TResponse>(Uri url, object query);

    Task SendPatchRequestAsync<TRequest>(Uri url, TRequest data);

    Task SendDeleteRequestAsync(Uri url);

    Task<TResponse> SendPostRequestAsync<TRequest, TResponse>(Uri url, TRequest data);
    
    Task SendPostRequestAsync<TRequest>(Uri url, TRequest data);
}