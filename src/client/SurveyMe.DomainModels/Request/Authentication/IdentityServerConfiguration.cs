namespace SurveyMe.DomainModels.Request.Authentication;

public sealed class IdentityServerConfiguration
{
    public string ClientId { get; set; }

    public string Scope { get; set; }
    
    public string ClientSecret { get; set; }
}