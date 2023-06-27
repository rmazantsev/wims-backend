using AutoMapper;
using DAL.Base;
using DAL.DTO;

namespace BLL.App.Mappers;

public class InventoryTransactionMapper: BaseMapper<BLL.DTO.InventoryTransaction, DAL.DTO.InventoryTransaction>
{
    public InventoryTransactionMapper(IMapper mapper) : base(mapper)
    {
    }
}