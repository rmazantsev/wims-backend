using AutoMapper;
using DAL.Base;
using Domain.App;

namespace Public.DTO.Mappers;

public class WarehouseInventoryMapper: BaseMapper<BLL.DTO.WarehouseInventory, v1.WarehouseInventory>
{
    public WarehouseInventoryMapper(IMapper mapper) : base(mapper)
    {
    }
}