using Contracts.Base;
using DAL.Contracts.App;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class WarehouseRepository : BaseRepository<Warehouse, DAL.DTO.Warehouse>, IWarehouseRepository
{
    public WarehouseRepository(ApplicationDbContext dataContext, IMapper<DTO.Warehouse, Warehouse> mapper) : base(dataContext, mapper)
    {
    }
    
    public async Task<IEnumerable<DTO.Warehouse>> AllAsync(Guid userId)
    {
        return (await RepositoryDbSet
            .Select(c => Mapper.Map(c))
            .ToListAsync())!;
    }
    
    public async Task<DTO.Warehouse?> FindAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await RepositoryDbSet
            .FirstOrDefaultAsync(m => m.Id == id));
    }

    public Task<DTO.Warehouse?> RemoveAsync(Guid id, Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<DTO.Warehouse?> CheckAndRemoveAsync(Guid id)
    {
        var entity = RepositoryDbSet
            .Include(i => i.WarehouseInventories)
            .Where(i => i.Id == id)
            .Any(i => i.WarehouseInventories != null && i.WarehouseInventories.Count > 0);

        return entity ? null : await RemoveAsync(id);
    }

    public Task<bool> IsOwnedByUserAsync(Guid id, Guid userId)
    {
        throw new NotImplementedException();
    }
}