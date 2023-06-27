using DAL.Contracts.Base;

namespace DAL.Contracts.App;

public interface IItemComponentRepository 
    : IBaseRepository<DAL.DTO.ItemComponent>, IItemComponentRepositoryCustom<DAL.DTO.ItemComponent>
{

}
public interface IItemComponentRepositoryCustom<TEntity>
{
    // add here shared methods between repo and service
    public TEntity? AddOrUpdate(TEntity entity);
    public Task<IEnumerable<TEntity>> AllAsync(Guid userId);
    Task<TEntity?> FindAsync(Guid id, Guid userId);
    Task<TEntity?> RemoveAsync(Guid id, Guid userId);
    Task<bool> IsOwnedByUserAsync(Guid id, Guid userId);
}