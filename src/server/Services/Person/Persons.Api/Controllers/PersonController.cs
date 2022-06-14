using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Person.Api.Models.Request;
using Persons.Models.Persons;
using Persons.Services.Abstracts;
using SurveyMe.Common.Exceptions;

namespace Person.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public sealed class PersonController : Controller
{
    private readonly IPersonalityService _personalityService;

    private readonly IMapper _mapper;
    
    
    public PersonController(IPersonalityService personalityService, IMapper mapper)
    {
        _personalityService = personalityService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> AddPersonality(PersonalityCreateRequestModels personalityRequest)
    {
        if (personalityRequest == null)
        {
            throw new BadRequestException("Empty model");
        }

        if (!ModelState.IsValid)
        {
            var errors = ModelState.ToDictionary(
                error => error.Key,
                error => error.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
            );
            
            throw new BadRequestException("Invalid data", errors);
        }
        
        //TODO: Add profile
        var personality = _mapper.Map<Personality>(personalityRequest);

        await _personalityService.AddPersonalityAsync(personality);
        
        return Ok();
    }
}