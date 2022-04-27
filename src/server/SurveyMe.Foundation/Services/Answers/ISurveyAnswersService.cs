using System;
using System.Threading.Tasks;
using SurveyMe.DomainModels;

namespace SurveyMe.Surveys.Foundation.Services.Answers
{
    public interface ISurveyAnswersService
    {
        Task<SurveyAnswer> GetAnswerByIdAsync(Guid id);

        Task AddAnswerAsync(SurveyAnswer answer, User author);
    }
}