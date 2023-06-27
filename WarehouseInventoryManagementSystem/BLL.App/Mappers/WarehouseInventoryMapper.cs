using AutoMapper;
using DAL.Base;
using Domain.App;

namespace BLL.App.Mappers;

public class WarehouseInventoryMapper: BaseMapper<BLL.DTO.WarehouseInventory, DAL.DTO.WarehouseInventory>
{
    public WarehouseInventoryMapper(IMapper mapper) : base(mapper)
    {
    }
}