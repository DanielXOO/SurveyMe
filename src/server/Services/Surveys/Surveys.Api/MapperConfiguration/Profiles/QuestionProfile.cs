using AutoMapper;
using Surveys.Api.Models.Request.Options;
using Surveys.Api.Models.Request.Questions;
using Surveys.Api.Models.Response.Questions;
using Surveys.Models.Options;
using Surveys.Models.Questions;

namespace Surveys.Api.MapperConfiguration.Profiles;

public sealed class QuestionProfile : Profile
{
    public QuestionProfile()
    {
        CreateMap<QuestionOptionAddRequestModel, QuestionOption>()
            .ReverseMap();
        
        CreateMap<QuestionOptionEditRequestModel, QuestionOption>()
            .ReverseMap();

        CreateMap<QuestionAddRequestModel, Question>()
            .ReverseMap();
        
        CreateMap<QuestionEditRequestModel, Question>()
            .ReverseMap();

        CreateMap<QuestionOptionResponseModel, QuestionOption>()
            .ReverseMap();

        CreateMap<QuestionResponseModel, Question>()
            .ReverseMap();
    }
}