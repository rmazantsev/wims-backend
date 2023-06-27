using AutoMapper;
using DAL.Base;
using Domain.App;

namespace DAL.EF.App.Mappers;

public class StoreInventoryMapper: BaseMapper<DAL.DTO.StoreInventory, StoreInventory>
{
    public StoreInventoryMapper(IMapper mapper) : base(mapper)
    {
    }
}