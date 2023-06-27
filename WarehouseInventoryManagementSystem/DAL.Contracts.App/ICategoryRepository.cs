using DAL.Contracts.Base;
using Domain.App;

namespace DAL.Contracts.App;

public interface ICategoryRepository : IBaseRepository<DAL.DTO.Category>, ICategoryRepositoryCustom<DAL.DTO.Category>
{

}

public interface ICategoryRepositoryCustom<TEntity>
{
    // add here shared methods between repo and service
    public Task<IEnumerable<TEntity>> AllAsync(Guid userId);
    Task<TEntity?> FindAsync(Guid id, Guid userId);
    Task<TEntity?> RemoveAsync(Guid id, Guid userId);
    Task<bool> IsOwnedByUserAsync(Guid id, Guid userId);
}