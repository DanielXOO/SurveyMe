using AutoMapper;
using Duende.IdentityServer.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Surveys.Api.Models.Request.Surveys;
using Surveys.Api.Models.Response.Errors;
using Surveys.Api.Models.Response.Pages;
using Surveys.Api.Models.Response.Queries;
using Surveys.Api.Models.Response.Surveys;
using Surveys.Common.Exceptions;
using Surveys.Models.Surveys;
using Surveys.Services.Abstracts;

namespace Surveys.Api.Controllers;

/// <summary>
/// Controller for surveys and answers
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class SurveysController : Controller
{
    private readonly ISurveysService _surveyService;
    private readonly IMapper _mapper;


    public SurveysController(ISurveysService surveyService, IMapper mapper)
    {
        _surveyService = surveyService;
        _mapper = mapper;
    }


    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PageResponseModel<SurveyResponseModel>))]
    [HttpGet]
    public async Task<IActionResult> GetSurveysPage([FromQuery] GetPageRequest request)
    {
        //TODO: check null request
        
        var surveys = await _surveyService
            .GetSurveysAsync(request.Page, request.PageSize, request.SortOrder, request.NameSearchTerm);
        
        //TODO: Check where i use NameSearchTerm
        var pageResponse = new PageResponseModel<SurveyResponseModel>
        {
            NameSearchTerm = request.NameSearchTerm,
            SortOrder = request.SortOrder,
            Page = _mapper.Map<PagedResultResponseModel<SurveyResponseModel>>(surveys)
        };

        if (surveys.TotalPages < surveys.CurrentPage && surveys.TotalPages > 0)
        {
            return RedirectToAction(nameof(GetSurveysPage), request);
        }

        return Ok(pageResponse);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseErrorResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseErrorResponse))]
    [HttpPost]
    public async Task<IActionResult> AddSurvey([FromBody] SurveyRequestModel surveyModel)
    {
        var authorId = Guid.Parse(HttpContext.User.GetSubjectId());

        if (!ModelState.IsValid)
        {
            //TODO:Add dictionary with messages
            throw new BadRequestException("Invalid data");
        }

        var survey = _mapper.Map<Survey>(surveyModel);

        await _surveyService.AddSurveyAsync(survey, authorId);

        return Ok();
    }
    
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SurveyResponseModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseErrorResponse))]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetSurvey(Guid id)
    {
        var survey = await _surveyService.GetSurveyByIdAsync(id);

        var surveyResponseModel = _mapper.Map<SurveyResponseModel>(survey);
        
        return Ok(surveyResponseModel);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(BaseErrorResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseErrorResponse))]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteSurvey(Guid id)
    {
        var survey = await _surveyService.GetSurveyByIdAsync(id);
        
        await _surveyService.DeleteSurveyAsync(survey);
        
        return Ok();
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseErrorResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseErrorResponse))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(BaseErrorResponse))]
    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> EditSurvey([FromBody] SurveyRequestModel surveyModel, Guid id)
    {
        //TODO: new model will be better
        if (surveyModel.Id != id)
        {
            throw new BadRequestException("Route id and request id do not match");
        }
        
        var survey = await _surveyService.GetSurveyByIdAsync(id);

        var userId = Guid.Parse(HttpContext.User.GetSubjectId());

        //TODO: Add condition for check admin
        if (userId != survey.AuthorId)
        {
            throw new ForbidException("Action denied");
        }
        
        _mapper.Map(surveyModel, survey);

        await _surveyService.UpdateSurveyAsync(survey);

        //TODO: 204 and 
        return Ok();
    }
}