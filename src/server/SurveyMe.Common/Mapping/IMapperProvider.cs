namespace SurveyMe.Common.Mapping
{
    public interface IMapperProvider
    {
        IMapper CreateMapper();
    }
}