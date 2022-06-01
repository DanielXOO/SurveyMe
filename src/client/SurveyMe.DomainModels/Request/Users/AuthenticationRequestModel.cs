using System.Text.Json.Serialization;

namespace SurveyMe.DomainModels.Request.Users;

public sealed class AuthenticationRequestModel
{
    [JsonPropertyName("username")]
    public string UserName { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }

    [JsonPropertyName("client_id")]
    public string ClientId { get; set; } = "client";

    [JsonPropertyName("grant_type")]
    public string GrantType { get; set; } = "password";

    [JsonPropertyName("scope")]
    public string Scope { get; set; } = "SurveyMeApi";
}