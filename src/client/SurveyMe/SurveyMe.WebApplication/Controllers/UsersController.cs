using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyMe.DomainModels;
using SurveyMe.DomainModels.Queries;
using SurveyMe.Services.Abstracts;
using SurveyMe.WebApplication.ViewModels;

namespace SurveyMe.WebApplication.Controllers;

[Authorize(Roles = RoleNames.Admin)]
public class UsersController : Controller
{
    private readonly IUserService _userService;


    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(GetPageQuery query)
    {
        var pageResponse = await _userService.GetUsersPageAsync(query);

        //TODO: Map to PageResponseViewModel<UserWithSurveysCountViewModel>
        
        return View(pageResponse);
    }

    [HttpGet]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var user = await _userService.GetUserAsync(id);
        
        //TODO: Map to UserDeleteOrEditViewModel
        
        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteUser(UserDeleteOrEditViewModel user)
    {
        await _userService.DeleteUserAsync(user.Id);

        return Redirect(user.ReturnUrl);
    }

    [HttpGet]
    public async Task<IActionResult> EditUser(Guid id)
    {
        var user = await _userService.GetUserAsync(id);
        
        return View(user);
    }
    
    [HttpPost]
    public async Task<IActionResult> EditUser(UserDeleteOrEditViewModel user)
    {
        //await _userService.EditUserAsync(user);
        //TODO: Add mapper to UserRequestModel
        throw new NotImplementedException();
    }

}