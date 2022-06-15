using Answers.Api.Models.Request.Answers;
using Answers.Api.Models.Request.Surveys;
using Answers.Api.Models.Response.Results;
using Answers.Models.Answers;
using Answers.Services.Models.Questions;
using Answers.Services.Models.Surveys;
using AutoMapper;

namespace Answers.Api.MapperConfiguration.Profiles;

public class AnswerProfile : Profile
{
    public AnswerProfile()
    {
        CreateMap<SurveyAnswerResult, SurveyAnswerResultResponseModel>();

        CreateMap<BaseAnswerResult, BaseAnswerResultResponseModel>()
            .Include<TextAnswerResult, TextAnswerResultResponseModel>()
            .Include<RateAnswerResult, RateAnswerResultResponseModel>()
            .Include<ScaleAnswerResult, ScaleAnswerResultResponseModel>()
            .Include<RadioAnswerResult, RadioAnswerResultResponseModel>()
            .Include<CheckboxAnswerResult, CheckboxAnswerResultResponseModel>();

        CreateMap<TextAnswerResult, TextAnswerResultResponseModel>();
        CreateMap<RateAnswerResult, RateAnswerResultResponseModel>();
        CreateMap<ScaleAnswerResult, ScaleAnswerResultResponseModel>();
        CreateMap<RadioAnswerResult, RadioAnswerResultResponseModel>();
        CreateMap<CheckboxAnswerResult, CheckboxAnswerResultResponseModel>();
        
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