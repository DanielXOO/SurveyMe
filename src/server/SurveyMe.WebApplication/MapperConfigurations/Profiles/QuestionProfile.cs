using AutoMapper;
using SurveyMe.DomainModels.Answers;
using SurveyMe.DomainModels.Questions;
using SurveyMe.Foundation.Models.Statistics;
using SurveyMe.WebApplication.Models.Requests.Answers;
using SurveyMe.WebApplication.Models.Requests.Questions;
using SurveyMe.WebApplication.Models.Responses.Questions;
using SurveyMe.WebApplication.Models.Responses.Statistics;

namespace SurveyMe.WebApplication.MapperConfigurations.Profiles;

public sealed class QuestionProfile : Profile
{
    public QuestionProfile()
    {
        CreateMap<QuestionOptionRequestModel, QuestionOption>()
            .ReverseMap();

        CreateMap<QuestionRequestModel, Question>()
            .ReverseMap();

        CreateMap<BaseAnswerRequestModel, BaseAnswer>()
            .Include<TextAnswerRequestModel, TextAnswer>()
            .Include<FileAnswerRequestModel, FileAnswer>()
            .Include<RateAnswerRequestModel, RateAnswer>()
            .Include<ScaleAnswerRequestModel, ScaleAnswer>()
            .Include<RadioAnswerRequestModel, RadioAnswer>()
            .Include<CheckboxAnswerRequestModel, CheckboxAnswer>();

        CreateMap<TextAnswerRequestModel, TextAnswer>();

        CreateMap<TextAnswerRequestModel, TextAnswer>()
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.TextAnswer));
        CreateMap<FileAnswerRequestModel, FileAnswer>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.File.Name))
            .ForMember(dest => dest.ContentType, opt => opt.MapFrom(src => src.File.ContentType));
        CreateMap<RateAnswerRequestModel, RateAnswer>()
            .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.RateAnswer));
        CreateMap<ScaleAnswerRequestModel, ScaleAnswer>()
            .ForMember(dest => dest.Scale, opt => opt.MapFrom(src => src.ScaleAnswer));
        CreateMap<RadioAnswerRequestModel, RadioAnswer>();
        CreateMap<CheckboxAnswerRequestModel, CheckboxAnswer>()
            .ForMember(dest => dest.Options, 
                opt => opt.MapFrom(src => src.OptionIds.Select(option => new OptionAnswer
                {
                    OptionId = option
                })));
        
        CreateMap<QuestionOptionResponseModel, QuestionOption>()
            .ReverseMap();

        CreateMap<QuestionResponseModel, Question>()
            .ReverseMap();
        
        CreateMap<QuestionAnswersStatistic, QuestionAnswersStatisticResponseModel>();
    }
}