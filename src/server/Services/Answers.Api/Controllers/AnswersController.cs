using Answers.Api.Exceptions;
using Answers.Api.Models.Answers;
using Answers.Api.Models.Response.Errors;
using Answers.Api.Models.Response.Surveys;
using Microsoft.AspNetCore.Mvc;

namespace Answers.Api.Controllers;


[ApiController]
public sealed class AnswersController : Controller
{
    public AnswersController()
    {
        
    }
    
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SurveyResponseModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseErrorResponse))]
    [HttpGet("{surveyId:guid}/answers/{id:guid}")]
    public async Task<IActionResult> Answer(Guid id, Guid surveyId)
    {
        //TODO: Load survey with refit
        
        return Ok();
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
}