using Answers.Api.Data.Abstracts;
using Answers.Api.Models.Answers;
using Answers.Api.Services.Abstracts;

namespace Answers.Api.Services;

public class SurveySurveyAnswersService : ISurveyAnswersService
{
    private readonly IAnswersUnitOfWork _unitOfWork;

    public SurveySurveyAnswersService(IAnswersUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<SurveyAnswer> GetAnswerByIdAsync(Guid id)
    {
        var answer = await _unitOfWork.Answers.GetByIdAsync(id);

        return answer;
    }

    public async Task AddAnswerAsync(SurveyAnswer answer, Guid authorId)
    {
        answer.UserId = authorId;
        
        await _unitOfWork.Answers.CreateAsync(answer);
    }
}