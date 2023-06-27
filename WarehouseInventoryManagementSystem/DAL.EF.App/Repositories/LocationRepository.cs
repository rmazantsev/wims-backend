using Contracts.Base;
using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class LocationRepository : BaseRepository<Location, DAL.DTO.Location>, ILocationRepository
{


    public async Task<IEnumerable<DTO.Location>> AllAsync(Guid userId)
    {
        return (await RepositoryDbSet
            .Select(c => Mapper.Map(c))
            .ToListAsync())!;
    }

    public async Task<DTO.Location?> FindAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await RepositoryDbSet
            .FirstOrDefaultAsync(m => m.Id == id));
    }

    public async Task<DTO.Location?> RemoveAsync(Guid id, Guid userId)
    {
        var entity = await FindAsync(id, userId);
        return entity == null ? null : await RemoveAsync(entity.Id);
    }

    public Task<bool> IsOwnedByUserAsync(Guid id, Guid userId)
    {
        throw new NotImplementedException();
    }

    public LocationRepository(ApplicationDbContext dataContext, IMapper<DTO.Location, Location> mapper) : base(dataContext, mapper)
    {
    }
}