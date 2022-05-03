using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SurveyMe.Common.Extensions;
using SurveyMe.Foundation.Services.Abstracts;
using SurveyMe.DomainModels;
using SurveyMe.WebApplication.Models.RequestModels;
using SurveyMe.WebApplication.Models.ResponseModels;

namespace SurveyMe.WebApplication.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class SurveyAnswerController : Controller
{
    private readonly ISurveyService _surveyService;
    private readonly ISurveyAnswersService _surveyAnswersService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    
    
    public SurveyAnswerController(ISurveyService surveyService, ISurveyAnswersService surveyAnswersService,
    IUserService userService, IMapper mapper)
    {
        _surveyService = surveyService;
        _surveyAnswersService = surveyAnswersService;
        _userService = userService;
        _mapper = mapper;
    }
    
    [HttpGet("{surveyId:guid}")]
    public async Task<IActionResult> Answer(Guid surveyId)
    {
        var survey = await _surveyService.GetSurveyByIdAsync(surveyId);

        if (survey == null)
        {
            return NotFound();
        }
        
        var surveyResponseModel = _mapper.Map<SurveyResponseModel>(survey);

        return Ok(surveyResponseModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> Answer([FromBody]SurveyAnswerRequestModel surveyAnswerRequestModel)
    {
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

        var answer = await CreateFromAnswerRequestModel(surveyAnswerRequestModel);

        await _surveyAnswersService.AddAnswerAsync(answer, author);
        
        return Ok();
    }
    

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