using AutoMapper;
using SurveyMe.Foundation.Models;
using SurveyMe.WebApplication.Models.ResponseModels;

namespace SurveyMe.WebApplication.MapperConfigurations.Profiles;

public sealed class OptionProfile : Profile
{
    public OptionProfile()
    {
        CreateMap<OptionAnswersStatistic, OptionAnswersStatisticResponseModel>();
    }
}