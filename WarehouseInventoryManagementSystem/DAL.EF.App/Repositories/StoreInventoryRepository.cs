using Contracts.Base;
using DAL.Contracts.App;
using DAL.DTO;
using Microsoft.EntityFrameworkCore;
using StoreInventory = Domain.App.StoreInventory;

namespace DAL.EF.App.Repositories;

public class StoreInventoryRepository : BaseRepository<StoreInventory, DAL.DTO.StoreInventory>, IStoreInventoryRepository
{

    public override async Task<IEnumerable<DTO.StoreInventory>> AllAsync()
    {
        return await RepositoryDbSet
            .OrderByDescending(i => i.LastUpdated)
            .Select(i => new DTO.StoreInventory()
            {
                Id = i.Id,
                Quantity = i.Quantity,
                LastUpdated = i.LastUpdated,
                StoreId = i.StoreId,
                Store = new Store()
                {
                    Name = i.Store!.Name,
                    Address = i.Store.Name,
                },
                ItemId = i.ItemId,
                Item = new DTO.Item()
                {
                    Id = i.Item!.Id,
                    Name = i.Item!.Name,
                    Description = i.Item!.Description,
                    CategoryId = i.Item!.CategoryId,
                    UnitId = i.Item!.UnitId,
                    Unit = new DTO.Unit()
                    {
                        Id = i.Item!.UnitId,
                        Name = i.Item.Unit!.Name,
                        Description = i.Item.Unit.Description,
                    }
                }
            }).ToListAsync();
    }

    public async Task<DTO.StoreInventory?> FindAsyncWithData(Guid id)
    {
        return await RepositoryDbSet
            .Where(m => m.Id == id)
            .OrderByDescending(i => i.LastUpdated)
            .Select(i => new DTO.StoreInventory()
            {
                Id = i.Id,
                Quantity = i.Quantity,
                LastUpdated = i.LastUpdated,
                StoreId = i.StoreId,
                Store = new Store()
                {
                    Name = i.Store!.Name,
                    Address = i.Store.Name,
                },
                ItemId = i.ItemId,
                Item = new DTO.Item()
                {
                    Id = i.Item!.Id,
                    Name = i.Item!.Name,
                    Description = i.Item!.Description,
                    CategoryId = i.Item!.CategoryId,
                    UnitId = i.Item!.UnitId,
                    Unit = new DTO.Unit()
                    {
                        Id = i.Item!.UnitId,
                        Name = i.Item.Unit!.Name,
                        Description = i.Item.Unit.Description,
                    }
                }
            }).FirstOrDefaultAsync();
    }

    public Task<IEnumerable<DTO.StoreInventory>> AllAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<DTO.StoreInventory?> FindAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await RepositoryDbSet
            .FirstOrDefaultAsync(m => m.Id == id));
    }

    public async Task<DTO.StoreInventory?> RemoveAsync(Guid id, Guid userId)
    {
        var entity = await FindAsync(id);
        return entity == null ? null : await RemoveAsync(entity.Id);
    }

    public override DTO.StoreInventory Update(DTO.StoreInventory entity)
    {
        entity.LastUpdated = DateTime.UtcNow;
        return base.Update(entity);
    }

    public Task<bool> IsOwnedByUserAsync(Guid id, Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<DTO.StoreInventory?> IsAlreadyExist(DTO.StoreInventory entity)
    {
        return Mapper.Map(await RepositoryDbSet
            .FirstOrDefaultAsync(i => i.StoreId == entity.StoreId && i.ItemId == entity.ItemId));
    }

    public StoreInventoryRepository(ApplicationDbContext dataContext, IMapper<DTO.StoreInventory, StoreInventory> mapper) : base(dataContext, mapper)
    {
    }
}