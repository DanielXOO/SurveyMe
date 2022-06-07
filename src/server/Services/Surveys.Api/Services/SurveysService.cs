using Surveys.Api.Common.Pagination;
using Surveys.Api.Common.Time;
using Surveys.Api.Data.Abstracts;
using Surveys.Api.Exceptions;
using Surveys.Api.Models.Surveys;
using Surveys.Api.Services.Abstracts;

namespace Surveys.Api.Services;

public class SurveysService : ISurveysService
{
    private readonly ISurveysUnitOfWork _unitOfWork;
    private readonly ISystemClock _systemClock;


    public SurveysService(ISurveysUnitOfWork unitOfWork, ISystemClock systemClock)
    {
        _unitOfWork = unitOfWork;
        _systemClock = systemClock;
    }


    public async Task<PagedResult<Survey>> GetSurveysAsync(int currentPage, int pageSize,
        SortOrder order, string searchRequest)
    {
        var data = await _unitOfWork.Surveys
            .GetSurveysAsync(pageSize, currentPage, searchRequest, order);

        return data;
    }

    public async Task DeleteSurveyAsync(Survey survey)
    {
        await _unitOfWork.Surveys.DeleteAsync(survey);
    }

    public async Task<Survey> GetSurveyByIdAsync(Guid id)
    {
        var survey = await _unitOfWork.Surveys.GetByIdAsync(id);

        if (survey == null)
        {
            throw new NotFoundException("Survey do not exist");
        }
            
        return survey;
    }

    public async Task AddSurveyAsync(Survey survey, Guid authorId)
    {
        survey.AuthorId = authorId;
        survey.LastChangeDate = _systemClock.UtcNow;
        await _unitOfWork.Surveys.CreateAsync(survey);
    }

    public async Task UpdateSurveyAsync(Survey survey)
    {
        survey.LastChangeDate = _systemClock.UtcNow;
        await _unitOfWork.Surveys.UpdateAsync(survey);
    }
}