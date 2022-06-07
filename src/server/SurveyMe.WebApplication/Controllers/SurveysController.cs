using AutoMapper;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyMe.DomainModels.Answers;
using SurveyMe.DomainModels.Roles;
using SurveyMe.DomainModels.Surveys;
using SurveyMe.Foundation.Exceptions;
using SurveyMe.Foundation.Services.Abstracts;
using SurveyMe.WebApplication.Models.Errors;
using SurveyMe.WebApplication.Models.Requests.Queries;
using SurveyMe.WebApplication.Models.Requests.Surveys;
using SurveyMe.WebApplication.Models.Responses.Pages;
using SurveyMe.WebApplication.Models.Responses.Statistics;
using SurveyMe.WebApplication.Models.Responses.Surveys;

namespace SurveyMe.WebApplication.Controllers;

/// <summary>
/// Controller for surveys and answers
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class SurveysController : Controller
{
    private readonly ISurveyService _surveyService;
    private readonly IUserService _userService;
    private readonly ISurveyAnswersService _surveyAnswersService;
    private readonly IMapper _mapper;


    public SurveysController(ISurveyService surveyService, IUserService userService, IMapper mapper, ISurveyAnswersService surveyAnswersService)
    {
        _surveyService = surveyService;
        _userService = userService;
        _mapper = mapper;
        _surveyAnswersService = surveyAnswersService;
    }
    
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SurveyResponseModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseErrorResponse))]
    [HttpGet("{surveyId:guid}/answers/{id:guid}")]
    public async Task<IActionResult> Answer(Guid id, Guid surveyId)
    {
        var survey = await _surveyService.GetSurveyByIdAsync(surveyId);
        
        survey.Id = Guid.NewGuid();
        
        var surveyResponseModel = _mapper.Map<SurveyResponseModel>(survey);

        return Ok(surveyResponseModel);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseErrorResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseErrorResponse))]
    [HttpPost("{surveyId:guid}/answers")]
    public async Task<IActionResult> Answer([FromBody]SurveyAnswerRequestModel surveyAnswerRequestModel,
        Guid surveyId)
    {
        if (surveyAnswerRequestModel.SurveyId != surveyId)
        {
            throw new BadRequestException("Route id and request id do not match");
        }
        
        if (!ModelState.IsValid)
        {
            throw new BadRequestException("Invalid data");
        }
        
        var authorId = Guid.Parse(HttpContext.User.GetSubjectId());
        var author = await _userService.GetUserByIdAsync(authorId);

        var answer = _mapper.Map<SurveyAnswer>(surveyAnswerRequestModel);
        
        await _surveyAnswersService.AddAnswerAsync(answer, author);
        
        return Ok();
    }
    
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SurveyResponseModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseErrorResponse))]
    [HttpGet("{surveyId:guid}/statistics/")]
    public async Task<IActionResult> GetSurveyStatistic(Guid surveyId)
    {
        var surveyStatistic = await _surveyAnswersService.GetStatisticByIdAsync(surveyId);
        var surveyStatisticResponseModel = _mapper.Map<SurveyAnswersStatisticResponseModel>(surveyStatistic);
        
        return Ok(surveyStatisticResponseModel);
    }
}