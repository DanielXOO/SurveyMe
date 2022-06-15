using System.Text.Json.Serialization;

namespace SurveyMe.DomainModels.Request.Authentication;

public sealed class AuthenticationRequestModel
{
    [JsonPropertyName("username")]
    public string UserName { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }

    [JsonPropertyName("client_id")]
    public string ClientId { get; set; }

    [JsonPropertyName("grant_type")]
    public string GrantType { get; set; }

    [JsonPropertyName("scope")]
    public string Scope { get; set; }
    
    [JsonPropertyName("client_secret")]
    public string ClientSecret { get; set; }
}