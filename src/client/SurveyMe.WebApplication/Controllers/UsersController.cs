using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SurveyMe.Data.Abstracts;
using SurveyMe.DomainModels.Request;
using SurveyMe.WebApplication.Models.ViewModels;

namespace SurveyMe.WebApplication.Controllers;

public class UsersController : Controller
{
    private readonly IUserApi _userApi;
    private readonly IMapper _mapper;

    public UsersController(IMapper mapper, IUserApi userApi)
    {
        _mapper = mapper;
        _userApi = userApi;
    }
    
    public async Task<IActionResult> Index(GetPageRequest request, int page = 1)
    {
        var pageResponse = await _userApi.GetUsersAsync(request, page);
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
    public async Task<IActionResult> DeleteUser(UserDeleteOrEditViewModel user)
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