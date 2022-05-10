using System;
using System.Threading.Tasks;
using SurveyMe.DomainModels;
using SurveyMe.Foundation.Models;

namespace SurveyMe.Foundation.Services.Abstracts;

public interface ISurveyAnswersService
{
    Task<SurveyAnswer> GetAnswerByIdAsync(Guid id);

    Task AddAnswerAsync(SurveyAnswer answer, User author);
        
    Task<SurveyAnswersStatistic> GetStatisticByIdAsync(Guid surveyId);
}