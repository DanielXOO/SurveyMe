using System;
using System.Threading.Tasks;
using AutoMapper;
using SurveyMe.Data;
using SurveyMe.DomainModels;
using SurveyMe.Foundation.Models;
using SurveyMe.Foundation.Services.Abstracts;

namespace SurveyMe.Foundation.Services;

public class SurveySurveyAnswersService : ISurveyAnswersService
{
    private readonly ISurveyMeUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SurveySurveyAnswersService(ISurveyMeUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public async Task<SurveyAnswer> GetAnswerByIdAsync(Guid id)
    {
        var answer = await _unitOfWork.Answers.GetByIdAsync(id);

        return answer;
    }

    public async Task AddAnswerAsync(SurveyAnswer answer, User author)
    {
        answer.User = author;
        answer.UserId = author.Id;

        await _unitOfWork.Answers.CreateAsync(answer);
    }

    public async Task<SurveyAnswersStatistic> GetStatisticByIdAsync(Guid surveyId)
    {
        var survey = await _unitOfWork.Surveys.GetByIdAsync(surveyId);
        
        //TODO: Get all answers for questions ids
        var surveyStatisticDb = await _unitOfWork.Answers.GetSurveyStatistic(survey);
        
        //TODO: match them
        var surveyStatistic = _mapper.Map<SurveyAnswersStatistic>(surveyStatisticDb);

        return surveyStatistic;
    }
}