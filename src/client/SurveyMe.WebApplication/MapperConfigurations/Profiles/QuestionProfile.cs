using AutoMapper;
using SurveyMe.DomainModels.Request;
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
    }
}