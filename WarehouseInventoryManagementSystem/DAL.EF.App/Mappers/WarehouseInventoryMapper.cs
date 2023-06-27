using AutoMapper;
using DAL.Base;
using Domain.App;

namespace DAL.EF.App.Mappers;

public class WarehouseInventoryMapper: BaseMapper<DAL.DTO.WarehouseInventory, WarehouseInventory>
{
    public WarehouseInventoryMapper(IMapper mapper) : base(mapper)
    {
    }
}