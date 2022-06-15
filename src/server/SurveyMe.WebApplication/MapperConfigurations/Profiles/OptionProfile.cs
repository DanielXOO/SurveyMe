using AutoMapper;
using SurveyMe.Foundation.Models.Statistics;
using SurveyMe.WebApplication.Models.Responses.Statistics;

namespace SurveyMe.WebApplication.MapperConfigurations.Profiles;

public sealed class OptionProfile : Profile
{
    public OptionProfile()
    {
        CreateMap<OptionAnswersStatistic, OptionAnswersStatisticResponseModel>();
    }
}