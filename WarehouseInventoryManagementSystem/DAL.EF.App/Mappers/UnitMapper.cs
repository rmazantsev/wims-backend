using AutoMapper;
using DAL.Base;
using Domain.App;

namespace DAL.EF.App.Mappers;

public class UnitMapper: BaseMapper<DAL.DTO.Unit, Unit>
{
    public UnitMapper(IMapper mapper) : base(mapper)
    {
    }
}