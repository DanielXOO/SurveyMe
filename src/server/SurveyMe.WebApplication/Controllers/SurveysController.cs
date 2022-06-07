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


    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PageResponseModel<SurveyResponseModel>))]
    [HttpGet]
    public async Task<IActionResult> GetSurveysPage([FromQuery] GetPageRequest request, int page = 1)
    {
        var surveys = await _surveyService
            .GetSurveysAsync(page, request.PageSize, request.SortOrder, request.NameSearchTerm);
        
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
        var author = await _userService.GetUserByIdAsync(authorId);

        if (!ModelState.IsValid)
        {
            throw new BadRequestException("Invalid data");
        }

        var survey = _mapper.Map<Survey>(surveyModel);

        await _surveyService.AddSurveyAsync(survey, author);

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
        
        var userId = Guid.Parse(HttpContext.User.GetSubjectId());
        var user = await _userService.GetUserByIdAsync(userId);
        var isAdmin = user.Roles.Select(role => role.Name).Contains(RoleNames.Admin);
        
        if (survey.Author.Id != userId || !isAdmin)
        {
            throw new ForbidException("Action denied");
        }
        
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
        if (surveyModel.Id != id)
        {
            throw new BadRequestException("Route id and request id do not match");
        }
        
        var survey = await _surveyService.GetSurveyByIdAsync(id);

        var userId = Guid.Parse(HttpContext.User.GetSubjectId());

        if (userId != survey.AuthorId)
        {
            throw new ForbidException("Action denied");
        }
        
        _mapper.Map(surveyModel, survey);

        await _surveyService.UpdateSurveyAsync(survey);

        return Ok();
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