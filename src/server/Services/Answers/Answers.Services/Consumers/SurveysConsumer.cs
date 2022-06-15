using Answers.Models.Surveys;
using Answers.Services.Abstracts;
using AutoMapper;
using MassTransit;
using SurveyMe.Common.Logging.Abstracts;
using SurveyMe.QueueModels;

namespace Answers.Services.Consumers;

public sealed class SurveysConsumer : IConsumer<SurveyQueueModel>
{
    private readonly ILogger _logger;

    private readonly IMapper _mapper;

    private readonly ISurveysService _surveysService;

    
    public SurveysConsumer(ILogger logger, IMapper mapper, ISurveysService surveysService)
    {
        _logger = logger;
        _mapper = mapper;
        _surveysService = surveysService;
    }

    
    public async Task Consume(ConsumeContext<SurveyQueueModel> context)
    {
        var survey = context.Message;
        
        _logger.LogInformation($"SurveyApi send action {survey.EventType}");
        
        await HandleEventAsync(survey);
    }


    private async Task HandleEventAsync(SurveyQueueModel surveyQueue)
    {
        var survey = _mapper.Map<Survey>(surveyQueue);
        
        switch (surveyQueue.EventType)
        {
            case EventType.Create:
                await _surveysService.AddSurveyAsync(survey);
                break;
            case EventType.Update:
                await _surveysService.UpdateSurveyAsync(survey);
                break;
            case EventType.Delete:
                await _surveysService.DeleteSurveyAsync(survey);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(surveyQueue), surveyQueue.EventType, "No such event");
        }
    }
}