using AutoMapper;
using SurveyMe.DomainModels.Request.Answers;
using SurveyMe.DomainModels.Request.Questions;
using SurveyMe.DomainModels.Response;
using SurveyMe.DomainModels.Response.Answers;
using SurveyMe.DomainModels.Response.Questions;
using SurveyMe.WebApplication.Models.ViewModels.Answers;
using SurveyMe.WebApplication.Models.ViewModels.Questions;
using SurveyMe.WebApplication.Models.ViewModels.Statistics;

namespace SurveyMe.WebApplication.MapperConfigurations.Profiles;

public class QuestionProfile : Profile
{
    public QuestionProfile()
    {
        CreateMap<QuestionAddOrEditViewModel, QuestionRequestModel>()
            .ForMember(dest => dest.Options, 
                opt => opt.MapFrom(src => src.Options
                    .Select(o => new QuestionOptionRequestModel{Text = o})));
        
        CreateMap<QuestionRequestModel, QuestionAddOrEditViewModel>()
            .ForMember(dest => dest.Options, 
                opt => opt.MapFrom(src => src.Options.SelectMany(o => o.Text)));
        
        CreateMap<QuestionResponseModel, QuestionAddOrEditViewModel>()
            .ForMember(dest => dest.Options, 
                opt => opt.MapFrom(src => src.Options.SelectMany(o => o.Text)));
        
        CreateMap<QuestionResponseModel, QuestionViewModel>();
        
        CreateMap<QuestionOptionResponseModel, QuestionOptionViewModel>();
        
        CreateMap<BaseAnswerViewModel, BaseAnswerRequestModel>()
            .Include<TextAnswerViewModel, TextAnswerRequestModel>()
            .Include<RadioAnswerViewModel, RadioAnswerRequestModel>()
            .Include<CheckboxAnswerViewModel, CheckboxAnswerRequestModel>()
            .Include<RateAnswerViewModel, RateAnswerRequestModel>()
            .Include<ScaleAnswerViewModel, ScaleAnswerRequestModel>()
            .Include<FileAnswerViewModel, FileAnswerRequestModel>();


        CreateMap<TextAnswerViewModel, TextAnswerRequestModel>();
        
        CreateMap<RadioAnswerViewModel, RadioAnswerRequestModel>();
        
        CreateMap<CheckboxAnswerViewModel, CheckboxAnswerRequestModel>();
        
        CreateMap<RateAnswerViewModel, RateAnswerRequestModel>();
        
        CreateMap<ScaleAnswerViewModel, ScaleAnswerRequestModel>();

        CreateMap<FileAnswerViewModel, FileAnswerRequestModel>();

        CreateMap<QuestionAnswersStatisticResponseModel, QuestionAnswersStatisticViewModel>();

        CreateMap<OptionAnswersStatisticResponseModel, OptionAnswersStatisticViewModel>();
    }
}