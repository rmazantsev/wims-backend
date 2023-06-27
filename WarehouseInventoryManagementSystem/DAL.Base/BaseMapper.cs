
using AutoMapper;
using Contracts.Base;

namespace DAL.Base;

public class BaseMapper<TSource, TDestination>: IMapper<TSource, TDestination>
{
    protected readonly IMapper Mapper;

    public BaseMapper(IMapper mapper)
    {
        Mapper = mapper;
    }
    
    public TDestination? Map(TSource? entity)
    {
        return Mapper.Map<TDestination>(entity);
    }

    public TSource? Map(TDestination? entity)
    {
        return Mapper.Map<TSource>(entity);
    }
}