using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SurveyMe.DomainModels.Request;
using SurveyMe.Services.Abstracts;
using SurveyMe.WebApplication.Models.ViewModels;

namespace SurveyMe.WebApplication.Controllers;

public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;
    
    public AccountController(IAccountService accountService, IMapper mapper)
    {
        _accountService = accountService;
        _mapper = mapper;
    }

    public IActionResult Login()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(UserLoginViewModel userLoginViewModel)
    {
        var userRequestModel = _mapper.Map<UserLoginRequestModel>(userLoginViewModel);

        await _accountService.LoginAsync(userRequestModel);

        return RedirectToAction("Index", "Users");
    }
}