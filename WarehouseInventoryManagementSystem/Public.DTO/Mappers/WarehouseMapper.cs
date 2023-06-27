using AutoMapper;
using DAL.Base;
using Domain.App;

namespace Public.DTO.Mappers;

public class WarehouseMapper: BaseMapper<BLL.DTO.Warehouse, v1.Warehouse>
{
    public WarehouseMapper(IMapper mapper) : base(mapper)
    {
    }
}