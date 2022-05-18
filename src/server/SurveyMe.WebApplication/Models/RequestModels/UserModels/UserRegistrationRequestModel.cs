using System.ComponentModel.DataAnnotations;

namespace SurveyMe.WebApplication.Models.RequestModels.UserModels;

public sealed class UserRegistrationRequestModel
{
    [Required(ErrorMessage = "Name cannot be empty")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Login cannot be empty")]
    public string Login { get; set; }

    [Required(ErrorMessage = "Password cannot be empty")]
    [MinLength(8, ErrorMessage = "Password is too short")]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).*$", 
        ErrorMessage ="Add digits and uppercase letters")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Repeat password")]
    [Compare("Password", ErrorMessage = "Passwords must match")]
    public string ConfirmPassword { get; set; }
}