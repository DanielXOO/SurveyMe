using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyMe.Common.Exceptions;
using SurveyMe.DomainModels;
using SurveyMe.Foundation.Services.Abstracts;
using SurveyMe.WebApplication.Models.Errors;
using SurveyMe.WebApplication.Models.RequestModels;

namespace SurveyMe.WebApplication.Controllers;

/// <summary>
/// Controller for account actions (signin, signup, etc.)
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
public sealed class AccountController : Controller
{
    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;


    public AccountController(IAccountService accountService, IMapper mapper)
    {
        _accountService = accountService;
        _mapper = mapper;
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseErrorResponse))]
    [HttpPost]
    public async Task<IActionResult> Login(UserLoginRequestModel user)
    {
        if (!ModelState.IsValid)
        {
            throw new BadRequestException("Invalid data");
        }

        var result = await _accountService.SignInAsync(user.Login, user.Password);

        if (!result.IsSuccessful)
        {
            throw new BadRequestException("Error SignIn");
        }

        var token = await _accountService.GenerateTokenAsync(user.Login);

        var response = new
        {
            access_token = token
        };
        
        return Ok(response);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseErrorResponse))]
    [HttpPost]
    public async Task<IActionResult> Registration(UserRegistrationRequestModel userModel)
    {
        if (!ModelState.IsValid)
        {
            throw new BadRequestException("Invalid data");
        }

        var user = _mapper.Map<User>(userModel);

        var result = await _accountService.RegisterAsync(user, userModel.Password);

        if (!result.IsSuccessful)
        {
            throw new BadRequestException("Registration error");
        }

        return Ok();
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> LogOut()
    {
        await _accountService.SignOutAsync();
            
        return Ok(); 
    }
}