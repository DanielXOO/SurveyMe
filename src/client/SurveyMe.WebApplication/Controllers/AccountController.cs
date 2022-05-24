﻿using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SurveyMe.DomainModels.Request.Users;
using SurveyMe.Services.Abstracts;
using SurveyMe.WebApplication.Models.ViewModels.Users;

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

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserLoginRequestModel user)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        
        var token = await _accountService.LoginAsync(user);
        
        Response.Cookies.Append("X-Access-Token", token,
            new CookieOptions 
            { 
                HttpOnly = true, 
                SameSite = SameSiteMode.Strict 
            });
        
        return RedirectToAction("Index", "Surveys");
    }

    public IActionResult Registration()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Registration(UserRegistrationViewModel user)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        
        var userRequest = _mapper.Map<UserRegistrationRequestModel>(user);
        
        await _accountService.RegistrationAsync(userRequest);
        
        return RedirectToAction("Login", "Account");
    }

    public IActionResult SignOut()
    {
        Response.Cookies.Delete("X-Access-Token");
        
        return RedirectToAction("Login", "Account");
    }
}