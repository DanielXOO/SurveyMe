using AutoMapper;
using Surveys.Models.Options;
using Surveys.Models.Questions;
using Surveys.Models.Queue;
using Surveys.Models.Surveys;

namespace Surveys.Services.Automapper.Profiles;

public sealed class QueueModelsProfile : Profile
{
    public QueueModelsProfile()
    {
        CreateMap<Survey, SurveyQueueModel>();
        CreateMap<Question, QuestionQueueModel>();
        CreateMap<QuestionOption, OptionQueueModel>();
    }
}