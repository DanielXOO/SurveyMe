using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace SurveyMe.WebApplication.Controllers;

public class ErrorsController : Controller
{
    public IActionResult Index(int code)
    {
        ViewBag.Data = new
        {
            ErrorCode = code, 
            ErrorDescription = ReasonPhrases.GetReasonPhrase(code)
        };

        return View();
    }
}