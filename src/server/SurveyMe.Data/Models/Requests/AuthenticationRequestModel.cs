using Refit;

namespace SurveyMe.Data.Models.Requests;

public sealed class AuthenticationRequestModel
{
    [AliasAs("username")]
    public string UserName { get; set; }

    [AliasAs("password")]
    public string Password { get; set; }

    [AliasAs("client_id")] public string ClientId { get; set; } = "1234";

    [AliasAs("grant_type")] public string GrantType { get; set; } = "password";

    [AliasAs("scope")] public string Scope { get; set; } = "SurveyApi";
}