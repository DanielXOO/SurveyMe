namespace SurveyMe.DomainModels.Request;

public sealed class UserDeleteOrEditRequestModel
{
    public Guid Id { get; set; } 
    
    public string DisplayName { get; set; }
}