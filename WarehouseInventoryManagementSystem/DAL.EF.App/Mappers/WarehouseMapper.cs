using AutoMapper;
using DAL.Base;
using Domain.App;

namespace DAL.EF.App.Mappers;

public class WarehouseMapper: BaseMapper<DAL.DTO.Warehouse, Warehouse>
{
    public WarehouseMapper(IMapper mapper) : base(mapper)
    {
    }
}