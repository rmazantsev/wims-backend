using DAL.Contracts.Base;

namespace DAL.Contracts.App;

public interface IWarehouseInventoryRepository 
    : IBaseRepository<DAL.DTO.WarehouseInventory>, IWarehouseInventoryRepositoryCustom<DAL.DTO.WarehouseInventory>
{
    public Task<DAL.DTO.WarehouseInventory?> IsAlreadyExist(DAL.DTO.WarehouseInventory entity);
}

public interface IWarehouseInventoryRepositoryCustom<TEntity>
{
    public Task<TEntity?> FindAsyncWithData(Guid id);
    // add here shared methods between repo and service
    public Task<IEnumerable<TEntity>> AllAsync(Guid userId);
    Task<TEntity?> FindAsync(Guid id, Guid userId);
    Task<TEntity?> RemoveAsync(Guid id, Guid userId);
    Task<bool> IsOwnedByUserAsync(Guid id, Guid userId);
}