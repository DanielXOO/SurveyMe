using Answers.Data.Abstracts;
using Answers.Models.Answers;
using Answers.Services.Abstracts;
using SurveyMe.Common.Pagination;

namespace Answers.Services;

public class AnswersService : IAnswersService
{
    private readonly IAnswersUnitOfWork _unitOfWork;

    public AnswersService(IAnswersUnitOfWork unitOfWork)
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

    public async Task<PagedResult<SurveyAnswer>> GetSurveyAnswersAsync(int currentPage, int pageSize, Guid surveyId)
    {
        var result = await _unitOfWork.Answers
            .GetSurveyAnswersAsync(currentPage, pageSize, surveyId);

        return result;
    }
}