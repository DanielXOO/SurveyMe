using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyMe.Common.Exceptions;
using SurveyMe.Common.Extensions;
using SurveyMe.DomainModels;
using SurveyMe.Foundation.Services.Abstracts;
using SurveyMe.WebApplication.Models.Errors;
using SurveyMe.WebApplication.Models.RequestModels;
using SurveyMe.WebApplication.Models.ResponseModels;

namespace SurveyMe.WebApplication.Controllers;

/// <summary>
/// Controller for surveys and answers
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
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
        
        //TODO: Add auth and uncomment
        //var authorId = User.GetUserId();
        //TODO: Change guid
        var author = await _userService.GetUserByIdAsync(Guid.NewGuid());

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
    public async Task<IActionResult> EditSurvey([FromBody] SurveyRequestModel surveyModel, Guid id)
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
        
        if (!ModelState.IsValid)
        {
            throw new BadRequestException("Invalid data");
        }
        
        var authorId = User.GetUserId();
        var author = await _userService.GetUserByIdAsync(authorId);
        
        var answer = await CreateFromAnswerRequestModel(surveyAnswerRequestModel);

        //pass authorId
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
    
    
    
    /// <summary>
    /// Map SurveyAnswerRequestModel into SurveyAnswer
    /// </summary>
    /// <param name="answerRequestModel">Request model</param>
    /// <returns>Answer domain model</returns>
    /// <exception cref="ArgumentOutOfRangeException">Throws if no such question type</exception>
    private async Task<SurveyAnswer> CreateFromAnswerRequestModel(SurveyAnswerRequestModel answerRequestModel)
    {
        var existingSurvey = await _surveyService.GetSurveyByIdAsync(answerRequestModel.SurveyId);
        
        var answer = new SurveyAnswer
        {
            SurveyId = answerRequestModel.SurveyId,
            QuestionAnswers = answerRequestModel.Questions.Select(questionFromRequest =>
            {
                var answer = new QuestionAnswer
                {
                    QuestionId = questionFromRequest.QuestionId
                };
                
                var questionDb = existingSurvey.Questions
                    .FirstOrDefault(q => q.Id == questionFromRequest.QuestionId);

                switch (questionDb?.Type)
                {
                    case QuestionType.Checkbox or QuestionType.Radio:
                        answer.Options = questionFromRequest.OptionIds.Select(optionId => new QuestionAnswerOption
                        {
                            QuestionOptionId = optionId
                        }).ToList();
                        break;
                    case QuestionType.Text:
                        answer.TextAnswer = questionFromRequest.TextAnswer;
                        break;
                    case QuestionType.File:
                        answer.FileAnswerId = questionFromRequest.FileAnswer.FileId;
                        break;
                    case QuestionType.Rate:
                        answer.RateAnswer = questionFromRequest.RateAnswer;
                        break;
                    case QuestionType.Scale:
                        answer.ScaleAnswer = questionFromRequest.ScaleAnswer;
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