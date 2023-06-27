using System.Text.Json;
using Contracts.Base;
using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class ItemComponentRepository : BaseRepository<ItemComponent, DAL.DTO.ItemComponent>, IItemComponentRepository
{

    public async Task<IEnumerable<DTO.ItemComponent>> AllAsync(Guid userId)
    {
        return (await RepositoryDbSet
            .Select(c => Mapper.Map(c))
            .ToListAsync())!;
    }

    public async Task<DTO.ItemComponent?> FindAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await RepositoryDbSet
            .FirstOrDefaultAsync(m => m.Id == id));
    }

    public async Task<DTO.ItemComponent?> RemoveAsync(Guid id, Guid userId)
    {
        var entity = await FindAsync(id, userId);
        return entity == null ? null : await RemoveAsync(entity.Id);
    }
    
    public DTO.ItemComponent? AddOrUpdate(DTO.ItemComponent component)
    {
        var existingComponent =
            RepositoryDbSet
                .FirstOrDefaultAsync(i => i.ComponentItemId == component.ComponentItemId && i.ItemId == component.ItemId).Result;
        if (existingComponent == null)
        {
            return Add(component);
        }
        else
        {
            component.Id = existingComponent.Id;
            return Update(component);
        }
    }

    public Task<bool> IsOwnedByUserAsync(Guid id, Guid userId)
    {
        throw new NotImplementedException();
    }

    public ItemComponentRepository(ApplicationDbContext dataContext, IMapper<DTO.ItemComponent, ItemComponent> mapper) : base(dataContext, mapper)
    {
    }
}