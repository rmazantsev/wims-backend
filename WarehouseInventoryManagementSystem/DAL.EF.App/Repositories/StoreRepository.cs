using Contracts.Base;
using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class StoreRepository : BaseRepository<Store, DAL.DTO.Store>, IStoreRepository
{


    public async Task<IEnumerable<DTO.Store>> AllAsync(Guid userId)
    {
        return (await RepositoryDbSet
            .Select(c => Mapper.Map(c))
            .ToListAsync())!;
    }

    public async Task<DTO.Store?> FindAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await RepositoryDbSet
            .FirstOrDefaultAsync(m => m.Id == id));
    }

    public async Task<DTO.Store?> RemoveAsync(Guid id, Guid userId)
    {
        var entity = await FindAsync(id, userId);
        return entity == null ? null : await RemoveAsync(entity.Id);
    }
    
    public async Task<DTO.Store?> CheckAndRemoveAsync(Guid id)
    {
        var entity = RepositoryDbSet
            .Include(i => i.StoreInventories)
            .Where(i => i.Id == id)
            .Any(i => i.StoreInventories != null && i.StoreInventories.Count > 0);

        return entity ? null : await RemoveAsync(id);
    }

    public Task<bool> IsOwnedByUserAsync(Guid id, Guid userId)
    {
        throw new NotImplementedException();
    }

    public StoreRepository(ApplicationDbContext dataContext, IMapper<DTO.Store, Store> mapper) : base(dataContext, mapper)
    {
    }
}