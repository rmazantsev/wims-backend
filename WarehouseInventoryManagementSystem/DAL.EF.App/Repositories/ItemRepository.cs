using Contracts.Base;
using DAL.Contracts.App;
using DAL.DTO;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class ItemRepository : BaseRepository<Domain.App.Item, DAL.DTO.Item>, IItemRepository
{
    public async Task<IEnumerable<DTO.Item>> AllAsync(Guid userId)
    {
        return (await RepositoryDbSet
            .Select(c => Mapper.Map(c))
            .ToListAsync())!;
    }

    public override async Task<Item?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Where(i => i.Id == id)
            .Select(i => new DTO.Item()
            {
                Id = i.Id,
                Name = i.Name,
                UnitId = i.UnitId,
                Unit = new Unit()
                {
                    Id = i.Unit!.Id,
                    Name = i.Unit.Name,
                    Description = i.Unit.Description
                },
                Description = i.Description,
                CategoryId = i.CategoryId,
                Category = i.Category == null
                    ? null
                    : new Category()
                    {
                        Id = i.Category.Id,
                        Name = i.Category.Name
                    },
                ItemComponents = i.ItemComponents!.Select(c => new ItemComponent()
                {
                    Id = c.Id,
                    ComponentItem = new DTO.Item()
                    {
                        Id = c.ComponentItem!.Id,
                        UnitId = c.ComponentItem.UnitId,
                        Unit = new Unit()
                        {
                            Id = c.ComponentItem!.Unit!.Id,
                            Name = c.ComponentItem!.Unit!.Name,
                            Description = c.ComponentItem.Unit.Description
                        },
                        Name = c.ComponentItem!.Name,
                    },
                    ComponentQuantity = c.ComponentQuantity,
                    ComponentItemId = c.ComponentItem.Id,
                    ItemId = i.Id
                }).ToList()
            }).FirstOrDefaultAsync();
    }

    public async Task<DTO.Item?> FindAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await RepositoryDbSet
            .FirstOrDefaultAsync(m => m.Id == id));
    }

    public async Task<DTO.Item?> RemoveAsync(Guid id, Guid userId)
    {
        var entity = await FindAsync(id, userId);
        return entity == null ? null : await RemoveAsync(entity.Id);
    }

    public Task<bool> IsOwnedByUserAsync(Guid id, Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<DTO.Item>> AllAsyncWithData()
    {
        return await RepositoryDbSet
            .Select(i => new DTO.Item()
            {
                Id = i.Id,
                Name = i.Name,
                UnitId = i.UnitId,
                Unit = new Unit()
                {
                    Id = i.Unit!.Id,
                    Name = i.Unit.Name,
                    Description = i.Unit.Description
                },
                Description = i.Description,
                CategoryId = i.CategoryId,
                Category = i.Category == null
                    ? null
                    : new Category()
                    {
                        Id = i.Category.Id,
                        Name = i.Category.Name
                    },
                ItemComponents = i.ItemComponents!.Select(c => new ItemComponent()
                {
                    Id = c.Id,
                    ComponentItem = new DTO.Item()
                    {
                        Id = c.ComponentItem!.Id,
                        UnitId = c.ComponentItem.UnitId,
                        Unit = new Unit()
                        {
                            Id = c.ComponentItem!.Unit!.Id,
                            Name = c.ComponentItem!.Unit!.Name,
                            Description = c.ComponentItem!.Unit!.Description
                        },
                        Name = c.ComponentItem!.Name,
                    },
                    ComponentQuantity = c.ComponentQuantity,
                    ComponentItemId = c.ComponentItem.Id,
                    ItemId = i.Id
                }).ToList()
            }).ToListAsync();
    }

    public ItemRepository(ApplicationDbContext dataContext, IMapper<Item, Domain.App.Item> mapper) : base(dataContext,
        mapper)
    {
    }
}