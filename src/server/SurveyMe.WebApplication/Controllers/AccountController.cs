using Microsoft.AspNetCore.Mvc;
using SurveyMe.DomainModels;
using SurveyMe.Foundation.Services.Abstracts;
using SurveyMe.WebApplication.Models.RequestModels;

namespace SurveyMe.WebApplication.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly IAccountService _accountService;
    
    
    public AccountController(ILogger<AccountController> logger, IAccountService accountService)
    {
        _accountService = accountService;
        _logger = logger;
    }
    
    
    [HttpPost(nameof(Login))]
    public async Task<IActionResult> Login(UserLoginRequest user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _accountService.SignInAsync(user.Login, user.Password);

        if (!result.IsSuccessful)
        {
            foreach (var error in result.ErrorMessages)
            {
                ModelState.AddModelError(string.Empty, error);
            }

            return BadRequest(ModelState);
        }

        return Ok();
    }
    
    [HttpPost(nameof(Registration))]
    public async Task<IActionResult> Registration(UserRegistrationRequest userModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = new User(){UserName = userModel.Login, DisplayName = userModel.Name};

        var result = await _accountService.RegisterAsync(user, userModel.Password);

        if (!result.IsSuccessful)
        {
            foreach (var error in result.ErrorMessages)
            {
                ModelState.AddModelError(string.Empty, error);
            }

            return BadRequest(ModelState);
        }

        return Ok();
    }
    
    [HttpPost(nameof(LogOut))]
    public async Task<IActionResult> LogOut()
    {
        await _accountService.SignOutAsync();
            
        return Ok(); 
    }
}