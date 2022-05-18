namespace SurveyMe.DomainModels.Request.Users;

public sealed class UserDeleteOrEditRequestModel
{
    public Guid Id { get; set; } 
    
    public string DisplayName { get; set; }
}