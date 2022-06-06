using System.Text.Json.Serialization;

namespace SurveyMe.DomainModels.Request.Authentication;

public sealed class RefreshTokenRequestModel
{
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; }
    
    [JsonPropertyName("client_id")]
    public string ClientId { get; set; }

    [JsonPropertyName("grant_type")]
    public string GrantType { get; set; }

    [JsonPropertyName("scope")]
    public string Scope { get; set; }
    
    [JsonPropertyName("client_secret")]
    public string ClientSecret { get; set; }
}