namespace SurveyMe.DomainModels.Request.Users;

public sealed class UserLoginRequestModel
{
    public string Login { get; set; }

    public string Password { get; set; }
}