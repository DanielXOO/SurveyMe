namespace SurveyMe.DomainModels.Request;

public sealed class UserEditRequestModel
{
    public Guid Id { get; set; } 
    
    public string DisplayName { get; set; }
}