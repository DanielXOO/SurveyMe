namespace SurveyMe.WebApplication.Models.Requests.Users;

public sealed class UserEditRequestModel
{
    public Guid Id { get; set; } 
    
    public string DisplayName { get; set; }
}