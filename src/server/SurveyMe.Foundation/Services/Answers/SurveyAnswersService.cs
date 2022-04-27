using System;
using System.Threading.Tasks;
using SurveyMe.Data;
using SurveyMe.DomainModels;

namespace SurveyMe.Surveys.Foundation.Services.Answers
{
    public class SurveySurveyAnswersService : ISurveyAnswersService
    {
        private readonly ISurveyMeUnitOfWork _unitOfWork;
        
        
        public SurveySurveyAnswersService(ISurveyMeUnitOfWork unitOfWork)
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
    }
}