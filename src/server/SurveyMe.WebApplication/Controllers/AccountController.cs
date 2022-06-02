using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SurveyMe.DomainModels.Users;
using SurveyMe.Foundation.Exceptions;
using SurveyMe.Foundation.Services.Abstracts;
using SurveyMe.WebApplication.Models.Errors;
using SurveyMe.WebApplication.Models.Requests.Users;

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
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            
            throw new BadRequestException(string.Join('\n', errors));
        }

        var token = await _accountService.SignInAsync(user.Login, user.Password);
        
        Response.Cookies.Append("X-Access-Token", token.AccessToken,
            new CookieOptions 
            { 
                HttpOnly = true, 
                SameSite = SameSiteMode.Strict 
            });
        
        return Ok(token);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseErrorResponse))]
    [HttpPost]
    public async Task<IActionResult> Registration(UserRegistrationRequestModel userModel)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            
            throw new BadRequestException(string.Join('\n', errors));
        }

        var user = _mapper.Map<User>(userModel);

        var result = await _accountService.RegisterAsync(user, userModel.Password);

        if (!result.IsSuccessful)
        {
            throw new BadRequestException(string.Join('\n', result.ErrorMessages));
        }

        return Ok();
    }
}