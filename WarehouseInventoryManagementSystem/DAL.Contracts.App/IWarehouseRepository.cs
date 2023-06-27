using DAL.Contracts.Base;
using Domain.App;

namespace DAL.Contracts.App;

public interface IWarehouseRepository 
    : IBaseRepository<DAL.DTO.Warehouse>, IWarehouseRepositoryCustom<DAL.DTO.Warehouse>
{

}

public interface IWarehouseRepositoryCustom<TEntity>
{
    // add here shared methods between repo and service
    public Task<TEntity?> CheckAndRemoveAsync(Guid id);
    public Task<IEnumerable<TEntity>> AllAsync(Guid userId);
    Task<TEntity?> FindAsync(Guid id, Guid userId);
    Task<TEntity?> RemoveAsync(Guid id, Guid userId);
    Task<bool> IsOwnedByUserAsync(Guid id, Guid userId);
}