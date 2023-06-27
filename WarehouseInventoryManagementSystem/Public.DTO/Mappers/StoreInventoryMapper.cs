using AutoMapper;
using DAL.Base;

namespace Public.DTO.Mappers;

public class StoreInventoryMapper: BaseMapper<BLL.DTO.StoreInventory, v1.StoreInventory>
{
    public StoreInventoryMapper(IMapper mapper) : base(mapper)
    {
    }
}