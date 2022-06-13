using Answers.Data.Abstracts;
using Answers.Models.Surveys;
using Answers.Services.Abstracts;

namespace Answers.Services;

public class SurveysService : ISurveysService
{
    private readonly IAnswersUnitOfWork _unitOfWork;


    public SurveysService(IAnswersUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    
    public async Task DeleteSurveyAsync(Survey survey)
    {
        await _unitOfWork.Surveys.DeleteAsync(survey);
    }

    public async Task<Survey> GetSurveyByIdAsync(Guid id)
    {
        var survey = await _unitOfWork.Surveys.GetByIdAsync(id);

        return survey;
    }

    public async Task AddSurveyAsync(Survey survey)
    {
        await _unitOfWork.Surveys.CreateAsync(survey);
    }

    public async Task UpdateSurveyAsync(Survey survey)
    {
        await _unitOfWork.Surveys.UpdateAsync(survey);
    }
}