using System.ComponentModel.DataAnnotations;

namespace SurveyMe.DomainModels.Request;

public sealed class UserLoginRequestModel
{
    [Required(ErrorMessage = "Login cannot be empty")]
    public string Login { get; set; }

    [Required(ErrorMessage = "Password cannot be empty")]
    public string Password { get; set; }
}