using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SurveyMe.Data;
using SurveyMe.DomainModels.Answers;
using SurveyMe.DomainModels.Users;
using SurveyMe.Foundation.Models.Statistics;
using SurveyMe.Foundation.Services.Abstracts;

namespace SurveyMe.Foundation.Services;

public class SurveySurveyAnswersService : ISurveyAnswersService
{
    private readonly ISurveyMeUnitOfWork _unitOfWork;

    public SurveySurveyAnswersService(ISurveyMeUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
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
        var surveyAnswers = _unitOfWork.Answers.GetBySurveyId(surveyId);

        var questionAnswers = surveyAnswers
            .SelectMany(surveyAnswer => surveyAnswer.QuestionsAnswers);
        
        var questionsStatistics =  survey.Questions.GroupJoin(questionAnswers,
            question => question.Id,
            answer => answer.QuestionId,
            (question, _) => new QuestionAnswersStatistic
            {
                QuestionTitle = question.Title,
                QuestionType = question.Type,
                AnswersCount = questionAnswers
                    .Count(answer => answer.QuestionId == question.Id)
            });
        
        var surveyStatistics = new SurveyAnswersStatistic
        {
            SurveyTitle = survey.Name,
            AnswersCount = surveyAnswers.Count(),
            QuestionAnswersStatistic = questionsStatistics.ToArray()
        };
        
        return surveyStatistics;
    }
}