using Microsoft.AspNetCore.Mvc;
using SurveyMe.Common.Exceptions;
using SurveyPerson.Api.Models.Requests;
using SurveyPerson.Services.Abstracts;

namespace SurveyPerson.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class SurveyPersonController : Controller
{
    private readonly IOptionsService _optionsService;
    
    
    public SurveyPersonController(IOptionsService optionsService)
    {
        _optionsService = optionsService;
    }


    public async Task<IActionResult> GetOptions(Guid id)
    {
        var options = await _optionsService.GetByIdAsync(id);
        
        return Ok(options);
    }

    public async Task<IActionResult> EditOptions(OptionsEditRequestModel editRequestModel)
    {
        if (editRequestModel == null)
        {
            throw new BadRequestException("Model is empty");
        }

        if (!ModelState.IsValid)
        {
            var errors = ModelState.ToDictionary(
                error => error.Key,
                error => error.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
            );
            
            throw new BadRequestException("Invalid data", errors);
        }

        return NoContent();
    }

    public async Task<IActionResult> DeleteOptions()
    {
        return Ok();
    }

    public async Task<IActionResult> AddSurvey()
    {
        return Ok();
    }
}