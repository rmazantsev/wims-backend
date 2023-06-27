using AutoMapper;
using DAL.Base;


namespace Public.DTO.Mappers;

public class InventoryTransactionMapper: BaseMapper<BLL.DTO.InventoryTransaction, v1.InventoryTransaction>
{
    public InventoryTransactionMapper(IMapper mapper) : base(mapper)
    {
    }
}