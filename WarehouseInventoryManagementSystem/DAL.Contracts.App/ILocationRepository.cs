using DAL.Contracts.Base;

namespace DAL.Contracts.App;

public interface ILocationRepository : IBaseRepository<DAL.DTO.Location>, ILocationRepositoryCustom<DAL.DTO.Location>
{

}

public interface ILocationRepositoryCustom<TEntity>
{
    // add here shared methods between repo and service
    public Task<IEnumerable<TEntity>> AllAsync(Guid userId);
    Task<TEntity?> FindAsync(Guid id, Guid userId);
    Task<TEntity?> RemoveAsync(Guid id, Guid userId);
    Task<bool> IsOwnedByUserAsync(Guid id, Guid userId);
}