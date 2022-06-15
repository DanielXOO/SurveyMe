using Microsoft.AspNetCore.Mvc;

namespace SurveyMe.WebApplication.Controllers;

/// <summary>
/// Controller for account actions (signin, signup, etc.)
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
public sealed class AccountController : Controller
{
    /*private readonly IMapper _mapper;


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

        await _accountService.SignInAsync(user.Login, user.Password);

        var test =  await HttpContext.GetTokenAsync("access_token");
        
        
        return Ok();
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
            throw new BadRequestException(string.Join('\n', result.ErrorMessages));
        }

        return Ok();
    }*/
}