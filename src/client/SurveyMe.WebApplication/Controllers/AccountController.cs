using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using SurveyMe.Data.Abstracts;
using SurveyMe.DomainModels.Request;

namespace SurveyMe.WebApplication.Controllers;

public class AccountController : Controller
{
    private readonly IAccountApi _accountApi;

    public AccountController( IAccountApi accountApi, JwtBearerHandler handler)
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
        return Ok();
    }
}