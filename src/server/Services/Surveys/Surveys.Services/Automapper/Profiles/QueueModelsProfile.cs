using AutoMapper;
using SurveyMe.QueueModels;
using Surveys.Models.Options;
using Surveys.Models.Questions;
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