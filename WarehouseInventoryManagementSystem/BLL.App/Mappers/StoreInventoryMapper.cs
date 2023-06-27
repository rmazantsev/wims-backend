using AutoMapper;
using DAL.Base;

namespace BLL.App.Mappers;

public class StoreInventoryMapper: BaseMapper<BLL.DTO.StoreInventory, DAL.DTO.StoreInventory>
{
    public StoreInventoryMapper(IMapper mapper) : base(mapper)
    {
    }
}