using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SurveyMe.Common.Exceptions;
using SurveyMe.DomainModels.Request.Queries;
using SurveyMe.DomainModels.Request.Users;
using SurveyMe.Services.Abstracts;
using SurveyMe.WebApplication.Models.ViewModels.Pages;
using SurveyMe.WebApplication.Models.ViewModels.Users;

namespace SurveyMe.WebApplication.Controllers;

[Microsoft.AspNetCore.Authorization.Authorize]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UsersController(IMapper mapper, IUserService userService)
    {
        _mapper = mapper;
        _userService = userService;
    }
    
    public async Task<IActionResult> Index(GetPageRequest request, int page = 1)
    {
        var pageResponse = await _userService.GetUsersAsync(request, page);
        
        if (pageResponse == null)
        {
            throw new NotFoundException("Page do not exists");
        }
        
        var pageResponseViewModel = _mapper
            .Map<PageResponseViewModel<UserWithSurveysCountViewModel>>(pageResponse);
        
        return View(pageResponseViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> DeleteUser(UserDeleteOrEditViewModel user)
    {
        var userDeleteOrEditViewModel = _mapper.Map<UserDeleteOrEditViewModel>(user);
        
        return View(userDeleteOrEditViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteUserConfirm(UserDeleteOrEditViewModel user)
    {
        await _userService.DeleteUserAsync(user.Id);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> EditUser(Guid id)
    {
        var user = await _userService.GetUserAsync(id);
        var userDeleteOrEditViewModel = _mapper.Map<UserDeleteOrEditViewModel>(user);
        
        return View(userDeleteOrEditViewModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> EditUser(UserDeleteOrEditViewModel userDeleteOrEditViewModel)
    {
        var user = _mapper.Map<UserDeleteOrEditRequestModel>(userDeleteOrEditViewModel);
        
        await _userService.EditUserAsync(user, user.Id);

        return RedirectToAction("Index", "Users");
    }
}