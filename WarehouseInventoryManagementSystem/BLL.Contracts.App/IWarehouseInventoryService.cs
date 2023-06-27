using DAL.Contracts.App;
using DAL.Contracts.Base;
using Domain.App;

namespace BLL.Contracts.App;

public interface IWarehouseInventoryService : IBaseRepository<BLL.DTO.WarehouseInventory>,
    IWarehouseInventoryRepositoryCustom<BLL.DTO.WarehouseInventory>
{
    // add your custom service method here
    Task<BLL.DTO.WarehouseInventory?> AddWithTransaction(BLL.DTO.WarehouseInventory entity, Guid userId);
    Task<BLL.DTO.WarehouseInventory?> UpdateWithTransaction(BLL.DTO.WarehouseInventory entity, Guid userId);
    
}