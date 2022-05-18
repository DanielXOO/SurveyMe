using System.ComponentModel.DataAnnotations;

namespace SurveyMe.DomainModels.Request.Users;

public sealed class UserLoginRequestModel
{
    [Required(ErrorMessage = "Login cannot be empty")]
    public string Login { get; set; }

    [Required(ErrorMessage = "Password cannot be empty")]
    public string Password { get; set; }
}