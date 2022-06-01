using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Refit;
using SurveyMe.DomainModels.Request.Users;
using SurveyMe.Services.Abstracts;
using SurveyMe.WebApplication.Models.Errors;
using SurveyMe.WebApplication.Models.ViewModels.Users;

namespace SurveyMe.WebApplication.Controllers;

public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;
    
    public AccountController(IAccountService accountService, IMapper mapper)
    {
        _accountService = accountService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserLoginViewModel userView)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        var user = _mapper.Map<AuthenticationRequestModel>(userView);
        
        try
        {
            var token = await _accountService.LoginAsync(user);
            Response.Cookies.Append("X-Access-Token", token.AccessToken,
                new CookieOptions 
                { 
                    HttpOnly = true, 
                    SameSite = SameSiteMode.Strict 
                });
        }
        catch (ApiException ex)
        {
            var errors = JsonSerializer.Deserialize<BaseErrorResponse>(ex.Content);
            ModelState.AddModelError(string.Empty, errors.Details);
            
            return View();
        }
        
        return RedirectToAction("Index", "Surveys");
    }

    public IActionResult Registration()
    {
        return View();
    }

    /*[HttpPost]
    public async Task<IActionResult> Registration(UserRegistrationViewModel user)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        
        var userRequest = _mapper.Map<UserRegistrationRequestModel>(user);

        try
        {
            await _accountService.RegistrationAsync(userRequest);
        }
        catch (ApiException ex)
        {
            var errors = JsonSerializer.Deserialize<BaseErrorResponse>(ex.Content);
            ModelState.AddModelError(string.Empty, errors.Details);
            
            return View();
        }
        
        return RedirectToAction("Login", "Account");
    }*/

    public IActionResult SignOut()
    {
        if (User.Identity.IsAuthenticated)
        {
            Response.Cookies.Delete("X-Access-Token");
        }
        
        return RedirectToAction("Login", "Account");
    }
}