using AutoMapper;
using DAL.Base;
using Domain.App;

namespace DAL.EF.App.Mappers;

public class LocationMapper: BaseMapper<DAL.DTO.Location, Location>
{
    public LocationMapper(IMapper mapper) : base(mapper)
    {
    }
}