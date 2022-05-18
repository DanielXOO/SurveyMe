using AutoMapper;
using SurveyMe.Foundation.Models;
using SurveyMe.WebApplication.Models.ResponseModels;
using SurveyMe.WebApplication.Models.ResponseModels.StatisticModels;

namespace SurveyMe.WebApplication.MapperConfigurations.Profiles;

public sealed class OptionProfile : Profile
{
    public OptionProfile()
    {
        CreateMap<OptionAnswersStatistic, OptionAnswersStatisticResponseModel>();
    }
}