using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyMe.Common.Extensions;
using SurveyMe.DomainModels;
using SurveyMe.Foundation.Services.Abstracts;
using SurveyMe.WebApplication.Models.Queries;
using SurveyMe.WebApplication.Models.ResponseModels;

namespace SurveyMe.WebApplication.Controllers;

[ApiController]
[Route("[controller]/[action]")]
[Authorize]
public class SurveysController : Controller
{
    private readonly ISurveyService _surveyService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    
    
    public SurveysController(ISurveyService surveyService, IUserService userService, IMapper mapper)
    {
        _surveyService = surveyService;
        _userService = userService;
        _mapper = mapper;
    }


    [HttpGet]
    public async Task<IActionResult> GetSurveysPage([FromQuery] GetPageQuery query)
    {
        var surveys = await _surveyService
            .GetSurveysAsync(query.CurrentPage, query.PageSize, query.SortOrder, query.NameSearchTerm);
        
        var pageResponse = new PageResponseModel<SurveyWithLinksResponseModel>
        {
            NameSearchTerm = query.NameSearchTerm,
            SortOrder = query.SortOrder,
            Page = _mapper.Map<PagedResultResponseModel<SurveyWithLinksResponseModel>>(surveys)
        };
  
        foreach (var surveyWithLinksResponseModel in pageResponse.Page.Items)
        {
            surveyWithLinksResponseModel.SurveyLink = 
                Url.Action("Answer", "SurveyAnswer", new { surveyId = surveyWithLinksResponseModel.Id });
            surveyWithLinksResponseModel.ResultLink = "#";
        }

        if (surveys.TotalPages < surveys.CurrentPage && surveys.TotalPages > 0)
        {
            return RedirectToAction(nameof(GetSurveysPage), query);
        }

        return Ok(pageResponse);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddSurvey([FromBody] SurveyResponseModel surveyModel)
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

    [HttpGet("{surveyId:guid}")]
    public async Task<IActionResult> EditSurvey(Guid surveyId)
    {
        var survey = await _surveyService.GetSurveyByIdAsync(surveyId);
        if (survey == null)
        {
            return NotFound();
        }
        var surveyResponseModel = _mapper.Map<SurveyResponseModel>(survey);
        
        return Ok(surveyResponseModel);
    }
    
    [HttpDelete("{surveyId:guid}")]
    public async Task<IActionResult> DeleteSurvey(Guid surveyId)
    {
        var survey = await _surveyService.GetSurveyByIdAsync(surveyId);
        
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

    [HttpPatch]
    public async Task<IActionResult> EditSurvey([FromBody] SurveyResponseModel surveyModel)
    {
        var survey = await _surveyService.GetSurveyByIdAsync(surveyModel.Id);

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
}