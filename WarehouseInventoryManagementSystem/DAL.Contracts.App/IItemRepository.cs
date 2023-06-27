using DAL.Contracts.Base;
using DAL.DTO;
using Item = Domain.App.Item;

namespace DAL.Contracts.App;

public interface IItemRepository: IBaseRepository<DAL.DTO.Item>, IItemRepositoryCustom<DAL.DTO.Item>
{
    // add here custom methods for repo only

}

public interface IItemRepositoryCustom<TEntity>
{
    public Task<IEnumerable<TEntity>> AllAsyncWithData();
    // add here shared methods between repo and service
    public Task<IEnumerable<TEntity>> AllAsync(Guid userId);
    Task<TEntity?> FindAsync(Guid id, Guid userId);
    Task<TEntity?> RemoveAsync(Guid id, Guid userId);
    Task<bool> IsOwnedByUserAsync(Guid id, Guid userId);

}