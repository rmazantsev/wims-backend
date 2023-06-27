using DAL.Contracts.Base;
using Domain.App;
using Store = DAL.DTO.Store;

namespace DAL.Contracts.App;

public interface IStoreRepository 
    : IBaseRepository<DAL.DTO.Store>, IStoreRepositoryCustom<DAL.DTO.Store>
{

}

public interface IStoreRepositoryCustom<TEntity>
{
    // add here shared methods between repo and service
    public Task<TEntity?> CheckAndRemoveAsync(Guid id);
    public Task<IEnumerable<TEntity>> AllAsync(Guid userId);
    Task<TEntity?> FindAsync(Guid id, Guid userId);
    Task<TEntity?> RemoveAsync(Guid id, Guid userId);
    Task<bool> IsOwnedByUserAsync(Guid id, Guid userId);
}