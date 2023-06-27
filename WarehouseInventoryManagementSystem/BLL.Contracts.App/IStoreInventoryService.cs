using DAL.Contracts.App;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IStoreInventoryService : IBaseRepository<BLL.DTO.StoreInventory>,
    IStoreInventoryRepositoryCustom<BLL.DTO.StoreInventory>
{
    // add your custom service method here
    Task<BLL.DTO.StoreInventory?> AddWithTransaction(BLL.DTO.StoreInventory entity, Guid userId);
    Task<BLL.DTO.StoreInventory?> UpdateWithTransaction(BLL.DTO.StoreInventory entity, Guid userId);
}