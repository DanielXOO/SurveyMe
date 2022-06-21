using SurveyMe.Common.Exceptions;
using SurveyPerson.Data.Dapper.Abstracts;
using SurveyPerson.Models.Options;
using SurveyPerson.Services.Abstracts;

namespace SurveyPerson.Services;

public sealed class OptionsService : IOptionsService
{
    private readonly IUnitOfWork _unitOfWork;
    
    
    public OptionsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    
    public async Task CreateAsync(SurveyPersonOptions options)
    {
        await _unitOfWork.Options.CreateAsync(options);
        
        _unitOfWork.Commit();
    }

    public async Task<SurveyPersonOptions> GetByIdAsync(Guid id)
    {
        var options = await _unitOfWork.Options.GetByIdAsync(id);

        if (options == null)
        {
            throw new NotFoundException("Options for survey do not found");
        }
        
        return options;
    }

    public async Task<SurveyPersonOptions> GetBySurveyIdAsync(Guid surveyId)
    {
        var options = await _unitOfWork.Options.GetBySurveyIdAsync(surveyId);

        if (options == null)
        {
            throw new NotFoundException("Options for survey do not found");
        }
        
        return options;
    }

    public async Task UpdateAsync(SurveyPersonOptions options)
    {
        await _unitOfWork.Options.UpdateAsync(options);
        
        _unitOfWork.Commit();
    }

    public async Task DeleteAsync(Guid id)
    {
        await _unitOfWork.Options.DeleteAsync(id);
        
        _unitOfWork.Commit();
    }
}