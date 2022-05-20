using AutoMapper;
using SurveyMe.Foundation.Models.Statistics;
using SurveyAnswersStatisticDb = SurveyMe.Data.Models.SurveyAnswersStatistic;
using QuestionAnswersStatisticDb = SurveyMe.Data.Models.QuestionAnswersStatistic;
using OptionAnswersStatisticDb = SurveyMe.Data.Models.OptionAnswersStatistic;

namespace SurveyMe.Foundation.MapperConfigurations.Profiles;

public class SurveyStatisticProfile : Profile
{
    public SurveyStatisticProfile()
    {
        CreateMap<OptionAnswersStatisticDb, OptionAnswersStatistic>();
        CreateMap<QuestionAnswersStatisticDb, QuestionAnswersStatistic>();
        CreateMap<SurveyAnswersStatisticDb, SurveyAnswersStatistic>();
    }
}