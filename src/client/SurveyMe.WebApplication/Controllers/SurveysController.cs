using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SurveyMe.Common.Exceptions;
using SurveyMe.DomainModels.Request.Answers;
using SurveyMe.DomainModels.Request.Queries;
using SurveyMe.DomainModels.Request.Surveys;
using SurveyMe.Services.Abstracts;
using SurveyMe.WebApplication.Models.ViewModels.Answers;
using SurveyMe.WebApplication.Models.ViewModels.Pages;
using SurveyMe.WebApplication.Models.ViewModels.Statistics;
using SurveyMe.WebApplication.Models.ViewModels.Surveys;

namespace SurveyMe.WebApplication.Controllers;

public class SurveysController : Controller
{
    private readonly ISurveyService _surveyService;
    private readonly IMapper _mapper;
    
    public SurveysController(ISurveyService surveyService, IMapper mapper)
    {
        _surveyService = surveyService;
        _mapper = mapper;
    }


    [HttpGet]
    public async Task<IActionResult> Index(GetPageRequest request, int page = 1)
    {
        var pageResponse = await _surveyService.GetSurveysAsync(request, page);

        if (pageResponse == null)
        {
            throw new NotFoundException("Page do not exists");
        }
        
        var pageResultViewModel = _mapper.Map<PageResponseViewModel<SurveyWithLinksViewModel>>(pageResponse);
        
        foreach (var surveyWithLinksResponseModel in pageResultViewModel.Page.Items)
        {
            surveyWithLinksResponseModel.SurveyLink = 
                Url.Action(nameof(Answer), "Surveys", 
                    new { id = surveyWithLinksResponseModel.Id});
            surveyWithLinksResponseModel.ResultLink = 
                Url.Action(nameof(SurveyStatistics), "Surveys",
                    new { id = surveyWithLinksResponseModel.Id});;
        }
        
        return View(pageResultViewModel);
    }
    
    [HttpGet]
    public IActionResult AddSurvey()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddSurvey([FromBody] SurveyAddOrEditViewModel surveyModel)
    {
        if (surveyModel == null)
        {
            throw new BadRequestException("Survey add error");
        }
        
        var surveyRequest = _mapper.Map<SurveyRequestModel>(surveyModel);
        await _surveyService.AddSurveyAsync(surveyRequest);

        return Ok();
    }

    [HttpGet("[action]/{id:guid}")]
    public async Task<IActionResult> EditSurvey(Guid id)
    {
        var survey =  await _surveyService.GetSurveyAsync(id);
        
        if (survey == null)
        {
            throw new NotFoundException("Survey do not exists");
        }
        
        var surveyView = _mapper.Map<SurveyAddOrEditViewModel>(survey);
        
        return View(surveyView);
    }
    
    [HttpPost]
    public async Task<IActionResult> EditSurvey([FromBody] SurveyAddOrEditViewModel surveyModel)
    {
        if (surveyModel == null)
        {
            throw new BadRequestException("Survey edit error");
        }
        
        var surveyRequest = _mapper.Map<SurveyRequestModel>(surveyModel);
        await _surveyService.EditSurveyAsync(surveyRequest, surveyRequest.Id);
        
        return Ok();
    }
    
    [HttpGet]
    public IActionResult DeleteSurvey(SurveyDeleteViewModel surveyModel)
    {
        if (surveyModel == null)
        {
            throw new BadRequestException("Survey delete error");
        }
        
        return View(surveyModel);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirm(SurveyDeleteViewModel surveyModel)
    {
        if (surveyModel == null)
        {
            throw new BadRequestException("Survey delete error");
        }
        
        await _surveyService.DeleteSurveyAsync(surveyModel.Id);

        return Redirect(surveyModel.ReturnUrl);
    }
    
    [HttpGet("[action]/{id:guid}")]
    public async Task<IActionResult> Answer(Guid id)
    {
        var survey = await _surveyService.GetSurveyAsync(id);
        
        if (survey == null)
        {
            throw new NotFoundException("Survey do not exists");
        }
        
        var surveyView = _mapper.Map<SurveyViewModel>(survey);
            
        return View(surveyView);
    }
    
    [HttpPost]
    public async Task<IActionResult> Answer([FromBody]AnswerViewModel answerViewModel)
    {
        if (answerViewModel == null)
        {
            throw new BadRequestException("Answering error");
        }

        var answer = _mapper.Map<SurveyAnswerRequestModel>(answerViewModel);
        await _surveyService.AnswerAsync(answer, answer.SurveyId);
        
        return Ok();
    }
    
    [HttpGet]
    public async Task<IActionResult> SurveyStatistics(Guid id)
    {
        var statistic = await _surveyService.GetSurveyStatisticAsync(id);
        
        if (statistic == null)
        {
            throw new NotFoundException("Statistics for survey do not exists");
        }
        
        var statisticView = _mapper.Map<SurveyAnswersStatisticViewModel>(statistic);
        
        return View(statisticView);
    }
}