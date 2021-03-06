using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyMe.DomainModels.Roles;
using SurveyMe.Foundation.Exceptions;
using SurveyMe.Foundation.Services.Abstracts;
using SurveyMe.WebApplication.Models.Errors;
using SurveyMe.WebApplication.Models.Requests.Queries;
using SurveyMe.WebApplication.Models.Requests.Users;
using SurveyMe.WebApplication.Models.Responses.Pages;
using SurveyMe.WebApplication.Models.Responses.Users;

namespace SurveyMe.WebApplication.Controllers;

/// <summary>
/// Controller for admin panel for control users
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = RoleNames.Admin)]
public sealed class UsersController : Controller
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;


    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserEditResponseModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseErrorResponse))]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> EditUser(Guid id)
    {
        var user = await _userService.GetUserByIdAsync(id);

        if (user == null)
        {
            throw new NotFoundException("No such user");
        }
        
        var userResponseModel = _mapper.Map<UserEditResponseModel>(user);
        
        return Ok(userResponseModel);
    }

    [ProducesResponseType(StatusCodes.Status200OK, 
        Type = typeof(PageResponseModel<UserWithSurveysCountResponseModel>))]
    [HttpGet]
    public async Task<IActionResult> GetUsersPage([FromQuery] GetPageRequest request)
    {
        var users = await _userService
            .GetUsersAsync(request.Page, request.PageSize, request.SortOrder, request.NameSearchTerm);

        var pageResponse = new PageResponseModel<UserWithSurveysCountResponseModel>
        {
            NameSearchTerm = request.NameSearchTerm,
            SortOrder = request.SortOrder,
            Page = _mapper.Map<PagedResultResponseModel<UserWithSurveysCountResponseModel>>(users)
        };

        if (users.TotalPages < users.CurrentPage && users.TotalPages > 0)
        {
            return RedirectToAction(nameof(GetUsersPage), request);
        }

        return Ok(pageResponse);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseErrorResponse))]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var user = await _userService.GetUserByIdAsync(id);

        if (user.UserName == HttpContext.User.Identity?.Name)
        {
            throw new ForbidException("You can't delete yourself");
        }

        await _userService.DeleteUsersAsync(user);

        return Ok();
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseErrorResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseErrorResponse))]
    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> EditUser(UserEditRequestModel userEditRequestModel, Guid id)
    {
        if (userEditRequestModel.Id != id)
        {
            throw new BadRequestException("Id do not match");
        }
        
        var user = await _userService.GetUserByIdAsync(userEditRequestModel.Id);

        user.DisplayName = userEditRequestModel.DisplayName;
        await _userService.UpdateAsync(user);

        return Ok();
    }
}