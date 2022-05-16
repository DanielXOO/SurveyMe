using AutoMapper;
using SurveyMe.DomainModels.Request;
using SurveyMe.DomainModels.Response;
using SurveyMe.WebApplication.Models.ViewModels;

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
        
        CreateMap<QuestionAnswerViewModel, QuestionAnswerRequestModel>();

        CreateMap<FileAnswerViewModel, FileAnswerRequestModel>();

        CreateMap<QuestionAnswersStatisticResponseModel, QuestionAnswersStatisticViewModel>();

        CreateMap<OptionAnswersStatisticResponseModel, OptionAnswersStatisticViewModel>();
    }
}