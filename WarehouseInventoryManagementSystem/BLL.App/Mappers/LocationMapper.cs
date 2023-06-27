using AutoMapper;
using DAL.Base;
using Domain.App;

namespace BLL.App.Mappers;

public class LocationMapper: BaseMapper<BLL.DTO.Location, DAL.DTO.Location>
{
    public LocationMapper(IMapper mapper) : base(mapper)
    {
    }
}