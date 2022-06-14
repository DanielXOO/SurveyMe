using Answers.Models.Options;
using Answers.Models.Questions;
using Answers.Models.Surveys;
using AutoMapper;
using SurveyMe.QueueModels;

namespace Answers.Services.AutoMapper.Profiles;

public class QueueModelsProfile : Profile
{
    public QueueModelsProfile()
    {
        CreateMap<SurveyQueueModel, Survey>();
        CreateMap<QuestionQueueModel, Question>();
        CreateMap<OptionQueueModel, Option>();
    }
}