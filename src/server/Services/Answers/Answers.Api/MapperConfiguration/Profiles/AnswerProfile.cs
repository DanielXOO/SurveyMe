using Answers.Api.Models.Request.Answers;
using Answers.Api.Models.Request.Surveys;
using Answers.Api.Models.Response.Answers;
using Answers.Models.Answers;
using AutoMapper;

namespace Answers.Api.MapperConfiguration.Profiles;

public class AnswerProfile : Profile
{
    public AnswerProfile()
    {
        CreateMap<SurveyAnswer, SurveyAnswerResponseModel>();

        CreateMap<BaseQuestionAnswer, BaseAnswerResponseModel>()
            .Include<TextQuestionAnswer, TextAnswerResponseModel>()
            .Include<RateQuestionAnswer, RateAnswerResponseModel>()
            .Include<ScaleQuestionAnswer, ScaleAnswerResponseModel>()
            .Include<RadioQuestionAnswer, RadioAnswerResponseModel>()
            .Include<CheckboxQuestionAnswer, CheckboxAnswerResponseModel>();

        CreateMap<TextQuestionAnswer, TextAnswerResponseModel>()
            .ForMember(dest => dest.TextAnswer, opt => opt.MapFrom(src => src.Text));
        CreateMap<RateQuestionAnswer, RateAnswerResponseModel>()
            .ForMember(dest => dest.RateAnswer, opt => opt.MapFrom(src => src.Rate));
        CreateMap<ScaleQuestionAnswer, ScaleAnswerResponseModel>()
            .ForMember(dest => dest.ScaleAnswer, opt => opt.MapFrom(src => src.Scale));
        CreateMap<RadioQuestionAnswer, RadioAnswerResponseModel>();
        CreateMap<CheckboxQuestionAnswer, CheckboxAnswerResponseModel>()
            .ForMember(dest => dest.OptionIds, 
                opt => opt.MapFrom(src => src.Options.Select(option => option.OptionId)));
        
        CreateMap<SurveyAnswerRequestModel, SurveyAnswer>();
        
        CreateMap<BaseAnswerRequestModel, BaseQuestionAnswer>()
            .Include<TextAnswerRequestModel, TextQuestionAnswer>()
            .Include<FileAnswerRequestModel, FileQuestionAnswer>()
            .Include<RateAnswerRequestModel, RateQuestionAnswer>()
            .Include<ScaleAnswerRequestModel, ScaleQuestionAnswer>()
            .Include<RadioAnswerRequestModel, RadioQuestionAnswer>()
            .Include<CheckboxAnswerRequestModel, CheckboxQuestionAnswer>();

        CreateMap<TextAnswerRequestModel, TextQuestionAnswer>();

        CreateMap<TextAnswerRequestModel, TextQuestionAnswer>()
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.TextAnswer));
        CreateMap<FileAnswerRequestModel, FileQuestionAnswer>();
        CreateMap<RateAnswerRequestModel, RateQuestionAnswer>()
            .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.RateAnswer));
        CreateMap<ScaleAnswerRequestModel, ScaleQuestionAnswer>()
            .ForMember(dest => dest.Scale, opt => opt.MapFrom(src => src.ScaleAnswer));
        CreateMap<RadioAnswerRequestModel, RadioQuestionAnswer>();
        CreateMap<CheckboxAnswerRequestModel, CheckboxQuestionAnswer>()
            .ForMember(dest => dest.Options, 
                opt => opt.MapFrom(src => src.OptionIds.Select(option => new OptionQuestionAnswer
                {
                    OptionId = option
                })));
    }
}