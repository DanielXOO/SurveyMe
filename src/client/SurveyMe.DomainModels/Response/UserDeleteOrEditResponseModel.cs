namespace SurveyMe.DomainModels.Response;

public sealed class UserDeleteOrEditResponseModel
{
    public Guid Id { get; set; } 
    
    public string DisplayName { get; set; }
}