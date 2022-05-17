using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SurveyMe.DomainModels.Request;
using SurveyMe.Services.Abstracts;
using SurveyMe.WebApplication.Models.ViewModels;

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
        var pageResult = await _surveyService.GetSurveysAsync(request, page);

        var pageResultViewModel = _mapper.Map<PageResponseViewModel<SurveyWithLinksViewModel>>(pageResult);
        
        foreach (var surveyWithLinksResponseModel in pageResultViewModel.Page.Items)
        {
            surveyWithLinksResponseModel.SurveyLink = 
                Url.Action(nameof(Answer), "Surveys", 
                    new { id = surveyWithLinksResponseModel.Id});
            surveyWithLinksResponseModel.ResultLink = 
                Url.Action(nameof(SurveyStatistic), "Surveys",
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
        var surveyRequest = _mapper.Map<SurveyRequestModel>(surveyModel);
        await _surveyService.AddSurveyAsync(surveyRequest);

        return Ok();
    }

    [HttpGet("[action]/{id:guid}")]
    public async Task<IActionResult> EditSurvey(Guid id)
    {
        var survey =  await _surveyService.GetSurveyAsync(id);
        var surveyView = _mapper.Map<SurveyAddOrEditViewModel>(survey);
        
        return View(surveyView);
    }
    
    [HttpPost]
    public async Task<IActionResult> EditSurvey([FromBody] SurveyAddOrEditViewModel surveyModel)
    {
        var surveyRequest = _mapper.Map<SurveyRequestModel>(surveyModel);
        await _surveyService.EditSurveyAsync(surveyRequest, surveyRequest.Id);
        
        return Ok();
    }
    
    [HttpGet]
    public IActionResult DeleteSurvey(SurveyDeleteViewModel surveyModel)
    {
        return View(surveyModel);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirm(SurveyDeleteViewModel surveyModel)
    {
        await _surveyService.DeleteSurveyAsync(surveyModel.Id);

        return Redirect(surveyModel.ReturnUrl);
    }
    
    [HttpGet("[action]/{id:guid}")]
    public async Task<IActionResult> Answer(Guid id)
    {
        var survey = await _surveyService.GetSurveyAsync(id);
        var surveyView = _mapper.Map<SurveyViewModel>(survey);
            
        return View(surveyView);
    }
    
    [HttpPost]
    public async Task<IActionResult> Answer([FromBody]AnswerViewModel answerViewModel)
    {
        var answer = _mapper.Map<SurveyAnswerRequestModel>(answerViewModel);
        await _surveyService.AnswerAsync(answer, answer.SurveyId);
        
        return Ok();
    }
    
    [HttpGet]
    public async Task<IActionResult> SurveyStatistic(Guid id)
    {
        var statistic = await _surveyService.GetSurveyStatisticAsync(id);
        var statisticView = _mapper.Map<SurveyAnswersStatisticViewModel>(statistic);
        
        return View(statisticView);
    }
}