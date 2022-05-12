using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SurveyMe.DomainModels.Request;
using SurveyMe.Services.Abstracts;
using SurveyMe.WebApplication.Models.ViewModels;

namespace SurveyMe.WebApplication.Controllers;

public class UsersController : Controller
{
    private readonly IUserApi _userApi;
    private readonly IMapper _mapper;

    public UsersController(IUserApi userApi, IMapper mapper)
    {
        _userApi = userApi;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Index(GetPageRequest request)
    {
        var pageResponse = await _userApi.GetUsersAsync(request);
        var pageResponseViewModel = _mapper
            .Map<PageResponseViewModel<UserWithSurveysCountViewModel>>(pageResponse);
        
        return View(pageResponseViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var user = await _userApi.GetUserAsync(id);
        var userDeleteOrEditViewModel = _mapper.Map<UserDeleteOrEditViewModel>(user);
        
        return View(userDeleteOrEditViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteUser(UserDeleteOrEditViewModel user, string returnUrl = "")
    {
        await _userApi.DeleteUserAsync(user.Id);

        return RedirectToAction("Index", "Users");
    }

    [HttpGet]
    public async Task<IActionResult> EditUser(Guid id)
    {
        var user = await _userApi.GetUserAsync(id);
        var userDeleteOrEditViewModel = _mapper.Map<UserDeleteOrEditViewModel>(user);
        
        return View(userDeleteOrEditViewModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> EditUser(UserDeleteOrEditViewModel userDeleteOrEditViewModel)
    {
        var user = _mapper.Map<UserDeleteOrEditRequestModel>(userDeleteOrEditViewModel);
        
        await _userApi.EditUserAsync(user, user.Id);

        return RedirectToAction("Index", "Users");
    }
}