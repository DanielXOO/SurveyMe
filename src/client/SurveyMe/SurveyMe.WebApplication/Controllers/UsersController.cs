using Microsoft.AspNetCore.Mvc;
using SurveyMe.Services.Abstracts;

namespace SurveyMe.WebApplication.Controllers;

public class UsersController : Controller
{
    private readonly IUserService _userService;
    
    
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    
    public async Task<IActionResult> Index()
    {
        await _userService.DeleteUserAsync(Guid.Parse("edbd62f4-1784-4d32-b24e-6a1b9c2aea3c"));
        
        return View();
    }
}