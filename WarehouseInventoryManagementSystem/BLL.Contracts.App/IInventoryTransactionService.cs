using DAL.Contracts.App;
using DAL.Contracts.Base;
using Domain.App;

namespace BLL.Contracts.App;

public interface IInventoryTransactionService: IBaseRepository<BLL.DTO.InventoryTransaction>, IInventoryTransactionRepositoryCustom<BLL.DTO.InventoryTransaction>
{
    // add your custom service method here
    
}