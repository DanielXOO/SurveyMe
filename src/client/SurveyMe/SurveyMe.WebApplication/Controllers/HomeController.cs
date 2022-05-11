using Microsoft.AspNetCore.Mvc;

namespace SurveyMe.WebApplication.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}