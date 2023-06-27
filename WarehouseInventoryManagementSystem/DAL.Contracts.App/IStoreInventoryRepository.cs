using DAL.Contracts.Base;

namespace DAL.Contracts.App;

public interface IStoreInventoryRepository 
    : IBaseRepository<DAL.DTO.StoreInventory>, IStoreInventoryRepositoryCustom<DAL.DTO.StoreInventory>
{
    public Task<DAL.DTO.StoreInventory?> IsAlreadyExist(DAL.DTO.StoreInventory entity);
}

public interface IStoreInventoryRepositoryCustom<TEntity>
{
    public Task<TEntity?> FindAsyncWithData(Guid id);
    // add here shared methods between repo and service
    public Task<IEnumerable<TEntity>> AllAsync(Guid userId);
    Task<TEntity?> FindAsync(Guid id, Guid userId);
    Task<TEntity?> RemoveAsync(Guid id, Guid userId);
    Task<bool> IsOwnedByUserAsync(Guid id, Guid userId);
}