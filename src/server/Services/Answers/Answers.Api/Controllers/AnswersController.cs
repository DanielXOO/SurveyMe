using Answers.Api.Models.Request;
using Answers.Api.Models.Request.Surveys;
using Answers.Api.Models.Response.Errors;
using Answers.Api.Models.Response.Pages;
using Answers.Api.Models.Response.Results;
using Answers.Models.Answers;
using Answers.Services.Abstracts;
using AutoMapper;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyMe.Common.Exceptions;

namespace Answers.Api.Controllers;


[ApiController]
[Route("api/surveys/{surveyId:guid}/answers")]
[Authorize]
public sealed class AnswersController : Controller
{
    private readonly IMapper _mapper;
    private readonly IAnswersService _answersService;
    
    public AnswersController(IMapper mapper, IAnswersService answersService)
    {
        _mapper = mapper;
        _answersService = answersService;
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseErrorResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseErrorResponse))]
    [HttpPost]
    public async Task<IActionResult> Answer([FromBody]SurveyAnswerRequestModel surveyAnswerRequestModel,
        Guid surveyId)
    {
        if (surveyAnswerRequestModel.SurveyId != surveyId)
        {
            throw new BadRequestException("Route id and request id do not match");
        }
        
        if (!ModelState.IsValid)
        {
            var errors = ModelState.ToDictionary(
                error => error.Key,
                error => error.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
            );
            
            throw new BadRequestException("Invalid data", errors);
        }
        
        var authorId = Guid.Parse(HttpContext.User.GetSubjectId());

        var answer = _mapper.Map<SurveyAnswer>(surveyAnswerRequestModel);
        
        await _answersService.AddAnswerAsync(answer, authorId);
        
        return Ok();
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseErrorResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseErrorResponse))]
    [HttpGet]
    public async Task<IActionResult> GetAnswers([FromQuery] GetPageRequest request, Guid surveyId)
    {
        if (request == null)
        {
            throw new BadRequestException("Request is empty");
        }
        
        var answers = await _answersService.GetSurveyAnswersAsync(request.Page, request.PageSize, surveyId);
        var response = _mapper.Map<PagedResultResponseModel<SurveyAnswerResultResponseModel>>(answers);

        return Ok(response);
    }
}