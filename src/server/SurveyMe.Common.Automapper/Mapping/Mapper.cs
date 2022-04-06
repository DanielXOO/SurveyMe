using AutoMapper;
using IMapperCommon = SurveyMe.Common.Mapping.IMapper;

namespace SurveyMe.Common.Automapper.Mapping;

public sealed class Mapper : IMapperCommon
{
    private readonly IMapper _mapper;


    public Mapper(IMapper mapper)
    {
        _mapper = mapper;
    }

    
    public TDestination Map<TDestination>(object source)
    {
        return _mapper.Map<TDestination>(source);
    }

    public TDestination Map<TSource, TDestination>(TSource source)
    {
        return _mapper.Map<TSource, TDestination>(source);
    }

    public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
    {
        return _mapper.Map(source,destination);
    }

    public object Map(object source, Type sourceType, Type destinationType)
    {
        return _mapper.Map(source, sourceType, destinationType);
    }

    public object Map(object source, object destination, Type sourceType, Type destinationType)
    {
        return _mapper.Map(source, destination, sourceType, destinationType);
    }
}