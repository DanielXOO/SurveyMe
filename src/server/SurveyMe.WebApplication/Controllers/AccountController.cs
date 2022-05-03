using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyMe.DomainModels;
using SurveyMe.Foundation.Services.Abstracts;
using SurveyMe.WebApplication.Models.RequestModels;

namespace SurveyMe.WebApplication.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public sealed class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;


    public AccountController(ILogger<AccountController> logger, IAccountService accountService, IMapper mapper)
    {
        _accountService = accountService;
        _logger = logger;
        _mapper = mapper;
    }
    
    
    [HttpPost]
    public async Task<IActionResult> Login(UserLoginRequestModel user)
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
    
    [HttpPost]
    public async Task<IActionResult> Registration(UserRegistrationRequestModel userModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = _mapper.Map<User>(userModel);

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
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> LogOut()
    {
        await _accountService.SignOutAsync();
            
        return Ok(); 
    }
}