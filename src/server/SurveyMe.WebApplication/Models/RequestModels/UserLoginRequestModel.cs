using System.ComponentModel.DataAnnotations;

namespace SurveyMe.WebApplication.Models.RequestModels;

public sealed class UserLoginRequestModel
{
    [Required(ErrorMessage = "Login cannot be empty")]
    public string Login { get; set; }

    [Required(ErrorMessage = "Password cannot be empty")]
    public string Password { get; set; }
}