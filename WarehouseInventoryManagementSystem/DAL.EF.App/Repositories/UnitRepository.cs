using Contracts.Base;
using DAL.Contracts.App;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class UnitRepository : BaseRepository<Unit, DAL.DTO.Unit>, IUnitRepository
{

    public async Task<IEnumerable<DTO.Unit>> AllAsync(Guid userId)
    {
        return (await RepositoryDbSet
            .Select(c => Mapper.Map(c))
            .ToListAsync())!;
    }

    public async Task<DTO.Unit?> FindAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await RepositoryDbSet
            .FirstOrDefaultAsync(m => m.Id == id));
    }

    public async Task<DTO.Unit?> RemoveAsync(Guid id, Guid userId)
    {
        var entity = await FindAsync(id, userId);
        return entity == null ? null : await RemoveAsync(entity.Id);
    }

    public Task<bool> IsOwnedByUserAsync(Guid id, Guid userId)
    {
        throw new NotImplementedException();
    }

    public UnitRepository(ApplicationDbContext dataContext, IMapper<DTO.Unit, Unit> mapper) : base(dataContext, mapper)
    {
    }
}