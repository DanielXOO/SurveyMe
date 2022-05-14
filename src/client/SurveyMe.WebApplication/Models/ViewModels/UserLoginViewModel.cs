using System.ComponentModel.DataAnnotations;

namespace SurveyMe.WebApplication.Models.ViewModels
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "Login cannot be empty")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password cannot be empty")]
        public string Password { get; set; }
    }
}