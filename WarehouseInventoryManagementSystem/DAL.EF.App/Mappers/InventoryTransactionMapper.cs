using AutoMapper;
using DAL.Base;
using Domain.App;

namespace DAL.EF.App.Mappers;

public class InventoryTransactionMapper: BaseMapper<DAL.DTO.InventoryTransaction, InventoryTransaction>
{
    public InventoryTransactionMapper(IMapper mapper) : base(mapper)
    {
    }
}