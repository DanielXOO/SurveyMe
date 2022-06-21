using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Person.Api.Models.Request.Personality;
using Person.Api.Models.Response.Errors;
using Person.Api.Models.Response.Personality;
using Persons.Models.Persons;
using Persons.Services.Abstracts;
using SurveyMe.Common.Exceptions;

namespace Person.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public sealed class PersonController : Controller
{
    private readonly IPersonalityService _personalityService;

    private readonly IMapper _mapper;
    
    
    public PersonController(IPersonalityService personalityService, IMapper mapper)
    {
        _personalityService = personalityService;
        _mapper = mapper;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestErrorResponse))]
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
        
        var personality = _mapper.Map<Personality>(personalityRequest);

        await _personalityService.AddPersonalityAsync(personality);
        
        return NoContent();
    }

    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(PersonalityResponseModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseErrorResponse))]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPersonality(string id)
    {
        var personality = await _personalityService.GetPersonalityAsync(id);
        var personalityResponse = _mapper.Map<PersonalityResponseModel>(personality);

        return Ok(personalityResponse);
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestErrorResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseErrorResponse))]
    [HttpPatch("{id}")]
    public async Task<IActionResult> EditPersonality(PersonalityEditRequestModels personalityEditRequestModels,
        string id)
    {
        if (personalityEditRequestModels == null)
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

        if (!string.Equals(personalityEditRequestModels.Id, id))
        {
            throw new BadRequestException("Model id and request id do not match");
        }
        
        var personality = _mapper.Map<Personality>(personalityEditRequestModels);
        await _personalityService.EditPersonalityAsync(personality);

        return NoContent();
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseErrorResponse))]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePersonality(string id)
    {
        await _personalityService.DeletePersonality(id);
        
        return NoContent();
    }
}