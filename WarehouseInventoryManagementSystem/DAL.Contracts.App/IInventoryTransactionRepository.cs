using DAL.Contracts.Base;

namespace DAL.Contracts.App;

public interface IInventoryTransactionRepository: 
    IBaseRepository<DAL.DTO.InventoryTransaction>, IInventoryTransactionRepositoryCustom<DAL.DTO.InventoryTransaction>
{
}

public interface IInventoryTransactionRepositoryCustom<TEntity>
{
    // add here shared methods between repo and service
    public Task<IEnumerable<TEntity?>> AllAsyncById(Guid id);
    Task<TEntity?> FindAsync(Guid id, Guid userId);
    Task<TEntity?> RemoveAsync(Guid id, Guid userId);
    Task<bool> IsOwnedByUserAsync(Guid id, Guid userId);
}