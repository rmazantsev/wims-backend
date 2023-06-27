using AutoMapper;
using DAL.Base;
using Domain.App;

namespace BLL.App.Mappers;

public class UnitMapper: BaseMapper<BLL.DTO.Unit, DAL.DTO.Unit>
{
    public UnitMapper(IMapper mapper) : base(mapper)
    {
    }
}