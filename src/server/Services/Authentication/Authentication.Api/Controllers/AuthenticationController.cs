using Authentication.Api.Models.Request.Users;
using Authentication.Api.Models.Response.Errors;
using Authentication.Services.Abstracts;
using Authentication.Users;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SurveyMe.Common.Exceptions;

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
            var errors = ModelState.ToDictionary(
                error => error.Key,
                error => error.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
            );
            
            throw new BadRequestException("Invalid data", errors);
        }

        var user = _mapper.Map<User>(userModel);

        var result = await _accountService.RegisterAsync(user, userModel.Password);

        if (!result.IsSuccessful)
        {
            var error = new Dictionary<string, string[]>
            {
                { "Error", result.ErrorMessages.ToArray() }
            };
            
            throw new BadRequestException("Registration error", error);
        }

        return Ok();
    }
}