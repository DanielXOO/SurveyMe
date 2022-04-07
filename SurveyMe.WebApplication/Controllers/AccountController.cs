using Microsoft.AspNetCore.Mvc;

namespace SurveyMe.WebApplication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : Controller
{
    [HttpPost]
    public IActionResult SignIn()
    {
        return Json("Sign in");
    }
}