using AutoMapper;
using DAL.Base;
using Domain.App;

namespace Public.DTO.Mappers;

public class LocationMapper: BaseMapper<BLL.DTO.Location, v1.Location>
{
    public LocationMapper(IMapper mapper) : base(mapper)
    {
    }
}