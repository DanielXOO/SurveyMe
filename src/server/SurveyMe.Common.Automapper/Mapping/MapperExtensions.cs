using Microsoft.Extensions.DependencyInjection;
using SurveyMe.Common.Mapping;

namespace SurveyMe.Common.Automapper.Mapping;

public static class MapperExtensions
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
       

        return services;
    }
}