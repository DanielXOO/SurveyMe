using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyMe.Common.Extensions;
using SurveyMe.DomainModels;
using SurveyMe.Foundation.Services.Abstracts;
using SurveyMe.Surveys.Foundation.Exceptions;
using SurveyMe.WebApplication.Models.Errors;
using SurveyMe.WebApplication.Models.Queries;
using SurveyMe.WebApplication.Models.RequestModels;
using SurveyMe.WebApplication.Models.ResponseModels;

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
    private readonly IMapper _mapper;
    private readonly ISurveyAnswersService _surveyAnswersService;
    
    
    public SurveysController(ISurveyService surveyService, IUserService userService, IMapper mapper,
        ISurveyAnswersService surveyAnswersService)
    {
        _surveyService = surveyService;
        _userService = userService;
        _mapper = mapper;
        _surveyAnswersService = surveyAnswersService;
    }


    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PageResponseModel<SurveyWithLinksResponseModel>))]
    [HttpGet]
    public async Task<IActionResult> GetSurveysPage([FromQuery] GetPageQuery query, int page = 1)
    {
        var surveys = await _surveyService
            .GetSurveysAsync(page, query.PageSize, query.SortOrder, query.NameSearchTerm);
        
        var pageResponse = new PageResponseModel<SurveyWithLinksResponseModel>
        {
            NameSearchTerm = query.NameSearchTerm,
            SortOrder = query.SortOrder,
            Page = _mapper.Map<PagedResultResponseModel<SurveyWithLinksResponseModel>>(surveys)
        };
  
        foreach (var surveyWithLinksResponseModel in pageResponse.Page.Items)
        {
            surveyWithLinksResponseModel.SurveyLink = 
                Url.Action(nameof(Answer), "Surveys", 
                    new { surveyId = surveyWithLinksResponseModel.Id, id = Guid.NewGuid() });
            surveyWithLinksResponseModel.ResultLink = "#";
        }

        if (surveys.TotalPages < surveys.CurrentPage && surveys.TotalPages > 0)
        {
            return RedirectToAction(nameof(GetSurveysPage), query);
        }

        return Ok(pageResponse);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseErrorResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseErrorResponse))]
    [HttpPost]
    public async Task<IActionResult> AddSurvey([FromBody] SurveyResponseModel surveyModel)
    {
        var authorId = User.GetUserId();
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
        
        var userId = User.GetUserId();
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
    public async Task<IActionResult> EditSurvey([FromBody] SurveyResponseModel surveyModel, Guid id)
    {
        if (surveyModel.Id != id)
        {
            throw new BadRequestException("Route id and request id do not match");
        }
        
        var survey = await _surveyService.GetSurveyByIdAsync(id);

        var userId = User.GetUserId();

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
        
        var authorId = User.GetUserId();
        var author = await _userService.GetUserByIdAsync(authorId);

        if (!ModelState.IsValid)
        {
            throw new BadRequestException("Invalid data");
        }
        
        var answer = await CreateFromAnswerRequestModel(surveyAnswerRequestModel);

        await _surveyAnswersService.AddAnswerAsync(answer, author);
        
        return Ok();
    }
    
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SurveyResponseModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseErrorResponse))]
    [HttpGet("{surveyId:guid}/answers/statistic")]
    public async Task<IActionResult> GetSurveyStatistic(Guid surveyId)
    {
        await _surveyAnswersService.GetStatisticByIdAsync(surveyId);

        return Ok();
    }
    
    
    
    /// <summary>
    /// Map SurveyAnswerRequestModel into SurveyAnswer
    /// </summary>
    /// <param name="answerRequestModel">Request model</param>
    /// <returns>Answer domain model</returns>
    /// <exception cref="ArgumentOutOfRangeException">Throws if no such question type</exception>
    private async Task<SurveyAnswer> CreateFromAnswerRequestModel(SurveyAnswerRequestModel answerRequestModel)
    {
        var survey = await _surveyService.GetSurveyByIdAsync(answerRequestModel.SurveyId);
        
        var answer = new SurveyAnswer
        {
            SurveyId = answerRequestModel.SurveyId,
            QuestionAnswers = answerRequestModel.Questions.Select(question =>
            {
                var answer = new QuestionAnswer
                {
                    QuestionId = question.QuestionId
                };
                
                var questionDb = survey.Questions
                    .FirstOrDefault(questionDb => questionDb.Id == question.QuestionId);

                switch (questionDb?.Type)
                {
                    case QuestionType.Checkbox or QuestionType.Radio:
                        answer.Options = question.OptionIds.Select(optionId => new QuestionAnswerOption()
                        {
                            QuestionOptionId = optionId
                        }).ToList();
                        break;
                    case QuestionType.Text:
                        answer.TextAnswer = question.TextAnswer;
                        break;
                    case QuestionType.File:
                        answer.FileAnswerId = question.FileId;
                        break;
                    case QuestionType.Rate:
                        answer.RateAnswer = question.RateAnswer;
                        break;
                    case QuestionType.Scale:
                        answer.ScaleAnswer = question.ScaleAnswer;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(questionDb.Type), "No such type");
                }

                return answer;
            }).ToList()
        };

        return answer;
    }
}