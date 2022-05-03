using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyMe.DomainModels;
using SurveyMe.Foundation.Services.Abstracts;
using SurveyMe.WebApplication.Models.Queries;
using SurveyMe.WebApplication.Models.RequestModels;
using SurveyMe.WebApplication.Models.ResponseModels;

namespace SurveyMe.WebApplication.Controllers;

[ApiController]
[Route("[controller]/[action]")]
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

    
    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> EditUser(Guid userId)
    {
        var user = await _userService.GetUserByIdAsync(userId);

        if (user == null)
        {
            return NoContent();
        }

        var userResponseModel = _mapper.Map<UserEditResponseModel>(user);
        
        return Ok(userResponseModel);
    }

    [HttpGet]
    public async Task<IActionResult> GetUsersPage([FromQuery] GetPageQuery query)
    {
        var users = await _userService
            .GetUsersAsync(query.CurrentPage, query.PageSize, query.SortOrder, query.NameSearchTerm);

        var pageResponse = new PageResponseModel<UserWithSurveysCountResponseModel>
        {
            NameSearchTerm = query.NameSearchTerm,
            SortOrder = query.SortOrder,
            Page = _mapper.Map<PagedResultResponseModel<UserWithSurveysCountResponseModel>>(users)
        };

        if (users.TotalPages < users.CurrentPage && users.TotalPages > 0)
        {
            return RedirectToAction(nameof(GetUsersPage), query);
        }

        return Ok(pageResponse);
    }
    
    [HttpDelete("{userId:guid}")]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        var user = await _userService.GetUserByIdAsync(userId);

        if (user == null)
        {
            ModelState.AddModelError(string.Empty, $"User doesn't exist");

            return BadRequest(ModelState);
        }

        if (user.UserName == HttpContext.User.Identity?.Name)
        {
            ModelState.AddModelError(string.Empty, $"You can't delete yourself");

            return BadRequest(ModelState);
        }

        var result = await _userService.DeleteUsersAsync(user);

        if (!result.IsSuccessful)
        {
            foreach (var error in result.ErrorMessages)
            {
                ModelState.AddModelError(string.Empty, error);

                return BadRequest(ModelState);
            }
        }

        return Ok();
    }
    
    
    [HttpPatch]
    public async Task<IActionResult> EditUser(UserEditRequestModel userEdit)
    {
        var user = await _userService.GetUserByIdAsync(userEdit.Id);
            
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, $"Unable to edit user");

            return BadRequest(ModelState);
        }

        user.DisplayName = userEdit.DisplayName;
        var result = await _userService.UpdateAsync(user);

        if (!result.IsSuccessful)
        {
            foreach (var error in result.ErrorMessages)
            {
                ModelState.AddModelError(string.Empty, error);

                return BadRequest(ModelState);
            }
        }

        return Ok();
    }
}