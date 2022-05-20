using Microsoft.AspNetCore.Mvc;
using SurveyMe.Data.Abstracts;
using SurveyMe.DomainModels.Request.Users;

namespace SurveyMe.WebApplication.Controllers;

public class AccountController : Controller
{
    private readonly IAccountApi _accountApi;
    
    public AccountController(IAccountApi accountApi)
    {
        _accountApi = accountApi;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserLoginRequestModel user)
    {
        var token = await _accountApi.Login(user);
        
        Response.Cookies.Append("X-Access-Token", token,
            new CookieOptions 
            { 
                HttpOnly = true, 
                SameSite = SameSiteMode.Strict 
            });
        
        return RedirectToAction("Index", "Surveys");
    }
}