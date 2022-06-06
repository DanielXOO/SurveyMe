using Authentication.Api.Models.Errors;
using Authentication.Api.Models.Exceptions;
using Authentication.Api.Models.Users;
using Authentication.Api.Services.Abstracts;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Api.Controllers;

[ApiController]
public class AuthenticationController : Controller
{
    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;

    public AuthenticationController(IAccountService accountService, IMapper mapper)
    {
        _accountService = accountService;
        _mapper = mapper;
    }

    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseErrorResponse))]
    [HttpPost("/authentication/registration")]
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
    }
}