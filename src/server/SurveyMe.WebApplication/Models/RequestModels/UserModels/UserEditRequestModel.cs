namespace SurveyMe.WebApplication.Models.RequestModels.UserModels;

public sealed class UserEditRequestModel
{
    public Guid Id { get; set; } 
    
    public string DisplayName { get; set; }
}