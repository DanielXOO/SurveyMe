using Microsoft.AspNetCore.Mvc;

namespace SurveyMe.WebApplication.Controllers;

[ApiController]
public class AccountController : Controller
{
    [HttpPost("SignIn")]
    public IActionResult SignIn()
    {
        return Json("Sign in");
    }
}