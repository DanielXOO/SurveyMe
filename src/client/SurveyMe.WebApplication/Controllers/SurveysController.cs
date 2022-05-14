using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SurveyMe.Data.Abstracts;
using SurveyMe.DomainModels.Request;
using SurveyMe.WebApplication.Models.ViewModels;

namespace SurveyMe.WebApplication.Controllers;

public class SurveysController : Controller
{
    private readonly ISurveyApi _surveyApi;
    private readonly IMapper _mapper;
    
    public SurveysController(ISurveyApi surveyApi, IMapper mapper)
    {
        _surveyApi = surveyApi;
        _mapper = mapper;
    }


    public async Task<IActionResult> Index(GetPageRequest request, int page = 1)
    {
        var pageResult = await _surveyApi.GetSurveysAsync(request, page);

        var pageResultViewModel = _mapper.Map<PageResponseViewModel<SurveyWithLinksViewModel>>(pageResult);
        
        return View(pageResultViewModel);
    }
    
    public IActionResult AddSurvey()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddSurvey([FromBody] SurveyAddOrEditViewModel surveyModel)
    {
        var surveyRequest = _mapper.Map<SurveyRequestModel>(surveyModel);
        await _surveyApi.AddSurveyAsync(surveyRequest);

        return Ok();
    }
}