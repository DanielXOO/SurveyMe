using Answers.Models.Options;
using Answers.Models.Questions;
using Answers.Models.Surveys;
using Answers.Services.Models.Queue;
using AutoMapper;

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