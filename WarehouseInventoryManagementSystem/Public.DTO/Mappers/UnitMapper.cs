using AutoMapper;
using DAL.Base;
using Domain.App;

namespace Public.DTO.Mappers;

public class UnitMapper: BaseMapper<BLL.DTO.Unit, v1.Unit>
{
    public UnitMapper(IMapper mapper) : base(mapper)
    {
    }
}