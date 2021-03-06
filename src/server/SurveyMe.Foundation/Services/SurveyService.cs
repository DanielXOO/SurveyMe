using System;
using System.Threading.Tasks;
using SurveyMe.Common.Pagination;
using SurveyMe.Common.Time;
using SurveyMe.Data;
using SurveyMe.DomainModels.Surveys;
using SurveyMe.DomainModels.Users;
using SurveyMe.Foundation.Exceptions;
using SurveyMe.Foundation.Services.Abstracts;

namespace SurveyMe.Foundation.Services;

public class SurveyService : ISurveyService
{
    private readonly ISurveyMeUnitOfWork _unitOfWork;
    private readonly ISystemClock _systemClock;


    public SurveyService(ISurveyMeUnitOfWork unitOfWork, ISystemClock systemClock)
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

    public async Task AddSurveyAsync(Survey survey, User author)
    {
        survey.Author = author;
        survey.AuthorId = author.Id;
        survey.LastChangeDate = _systemClock.UtcNow;
        await _unitOfWork.Surveys.CreateAsync(survey);
    }

    public async Task UpdateSurveyAsync(Survey survey)
    {
        survey.LastChangeDate = _systemClock.UtcNow;
        await _unitOfWork.Surveys.UpdateAsync(survey);
    }
}