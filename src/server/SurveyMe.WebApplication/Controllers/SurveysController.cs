using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyMe.Common.Extensions;
using SurveyMe.DomainModels;
using SurveyMe.Foundation.Services.Abstracts;
using SurveyMe.WebApplication.Models.Queries;
using SurveyMe.WebApplication.Models.RequestModels;
using SurveyMe.WebApplication.Models.ResponseModels;

namespace SurveyMe.WebApplication.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SurveysController : Controller
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


    [HttpGet("pages/{page:int}")]
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
                Url.Action(nameof(Answer), "Surveys", new { surveyId = surveyWithLinksResponseModel.Id });
            surveyWithLinksResponseModel.ResultLink = "#";
        }

        if (surveys.TotalPages < surveys.CurrentPage && surveys.TotalPages > 0)
        {
            return RedirectToAction(nameof(GetSurveysPage), query);
        }

        return Ok(pageResponse);
    }
    
    [HttpPost("{id:guid}")]
    public async Task<IActionResult> AddSurvey([FromBody] SurveyResponseModel surveyModel, Guid id)
    {
        var authorId = User.GetUserId();
        var author = await _userService.GetUserByIdAsync(authorId);

        if (author == null)
        {
            ModelState.AddModelError(string.Empty, "No author for survey");
            
            return BadRequest(ModelState);
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var survey = _mapper.Map<Survey>(surveyModel);

        await _surveyService.AddSurveyAsync(survey, author);

        return Ok();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetSurvey(Guid id)
    {
        var survey = await _surveyService.GetSurveyByIdAsync(id);
        if (survey == null)
        {
            return NotFound();
        }
        var surveyResponseModel = _mapper.Map<SurveyResponseModel>(survey);
        
        return Ok(surveyResponseModel);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteSurvey(Guid id)
    {
        var survey = await _surveyService.GetSurveyByIdAsync(id);
        
        if (survey == null)
        {
            return NotFound();
        }
        
        var userId = User.GetUserId();
        var user = await _userService.GetUserByIdAsync(userId);
        var isAdmin = user.Roles.Select(role => role.Name).Contains(RoleNames.Admin);
        
        if (survey.Author.Id != userId || !isAdmin)
        {
            return Forbid();
        }
        
        await _surveyService.DeleteSurveyAsync(survey);
        
        return Ok();
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> EditSurvey([FromBody] SurveyResponseModel surveyModel, Guid id)
    {
        if (surveyModel.Id != id)
        {
            ModelState.AddModelError(string.Empty, "Route id and request id do not match");
            
            return BadRequest(ModelState);
        }
        
        var survey = await _surveyService.GetSurveyByIdAsync(id);

        if (survey == null)
        {
            ModelState.AddModelError(string.Empty, "Survey do not exist");
            
            return BadRequest(ModelState);
        }

        var userId = User.GetUserId();

        if (userId != survey.AuthorId)
        {
            return StatusCode(StatusCodes.Status403Forbidden);
        }
        
        _mapper.Map(surveyModel, survey);

        await _surveyService.UpdateSurveyAsync(survey);

        return Ok();
    }
    
    [HttpGet("answers/{id:guid}")]
    public async Task<IActionResult> Answer(Guid id)
    {
        var survey = await _surveyService.GetSurveyByIdAsync(id);

        if (survey == null)
        {
            return NotFound();
        }
        
        var surveyResponseModel = _mapper.Map<SurveyResponseModel>(survey);

        return Ok(surveyResponseModel);
    }
    
    [HttpPost("{surveyId:guid}/answers/{id:guid}")]
    public async Task<IActionResult> Answer([FromBody]SurveyAnswerRequestModel surveyAnswerRequestModel, 
        Guid surveyId, Guid id)
    {
        if (surveyAnswerRequestModel.SurveyId != surveyId)
        {
            ModelState.AddModelError(string.Empty, "Route id and request id do not match");
            
            return BadRequest(ModelState);
        }
        
        var authorId = User.GetUserId();
        var author = await _userService.GetUserByIdAsync(authorId);

        if (author == null)
        {
            ModelState.AddModelError(string.Empty, "No such author");
            
            return BadRequest(ModelState);
        }

        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        surveyAnswerRequestModel.Id = id;
        
        var answer = await CreateFromAnswerRequestModel(surveyAnswerRequestModel);

        await _surveyAnswersService.AddAnswerAsync(answer, author);
        
        return Ok();
    }
    

    private async Task<SurveyAnswer> CreateFromAnswerRequestModel(SurveyAnswerRequestModel answerRequestModel)
    {
        var survey = await _surveyService.GetSurveyByIdAsync(answerRequestModel.SurveyId);
        
        var answer = new SurveyAnswer
        {
            Id = answerRequestModel.Id,
            SurveyId = answerRequestModel.SurveyId,
            QuestionAnswers = answerRequestModel.Questions.Select(question =>
            {
                var answer = new QuestionAnswer
                {
                    QuestionId = question.Id
                };
                
                var questionDb = survey.Questions
                    .FirstOrDefault(questionDb => questionDb.Id == question.Id);

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
                    default:
                        throw new ArgumentOutOfRangeException(nameof(questionDb.Type), "No such type");
                }

                return answer;
            }).ToList()
        };

        return answer;
    }
}