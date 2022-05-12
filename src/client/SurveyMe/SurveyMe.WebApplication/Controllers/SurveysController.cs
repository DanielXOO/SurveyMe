using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SurveyMe.DomainModels.Request;
using SurveyMe.Services.Abstracts;
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


    public async Task<IActionResult> Index([FromQuery] GetPageRequest request, int page = 1)
    {
        var pageResult = await _surveyApi.GetSurveysPageAsync(request, page);

        var pageResultViewModel = _mapper.Map<PageResponseViewModel<SurveyWithLinksViewModel>>(pageResult);
        
        return View(pageResultViewModel);
    }
}