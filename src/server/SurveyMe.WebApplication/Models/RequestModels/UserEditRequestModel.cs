namespace SurveyMe.WebApplication.Models.RequestModels;

public sealed class UserEditRequestModel
{
    public Guid Id { get; set; } 
    
    public string DisplayName { get; set; }
}