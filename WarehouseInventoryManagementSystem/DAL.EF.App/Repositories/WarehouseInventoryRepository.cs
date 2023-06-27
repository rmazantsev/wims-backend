using Contracts.Base;
using DAL.Contracts.App;
using DAL.DTO;
using Microsoft.EntityFrameworkCore;
using WarehouseInventory = Domain.App.WarehouseInventory;

namespace DAL.EF.App.Repositories;

public class WarehouseInventoryRepository 
    : BaseRepository<WarehouseInventory, DAL.DTO.WarehouseInventory>, IWarehouseInventoryRepository
{

    public WarehouseInventoryRepository(ApplicationDbContext dataContext, IMapper<DTO.WarehouseInventory, WarehouseInventory> mapper) : base(dataContext, mapper)
    {
    }
    public override async Task<IEnumerable<DTO.WarehouseInventory>> AllAsync()
    {
        return await RepositoryDbSet
            .OrderByDescending(i => i.LastUpdated)
            .Select(i => new DTO.WarehouseInventory()
            {
                Id = i.Id,
                Quantity = i.Quantity,
                LocationId = i.LocationId,
                Location = (i.Location == null) ? null : new Location()
                {
                    Name = i.Location.Name,
                    Description = i.Location.Description
                },
                LastUpdated = i.LastUpdated,
                WarehouseId = i.WarehouseId,
                Warehouse = new Warehouse()
                {
                    Id = i.Warehouse!.Id,
                    Name = i.Warehouse.Name,
                    Address = i.Warehouse.Address,
                },
                ItemId = i.ItemId,
                Item = new Item()
                {
                    Id = i.Item!.Id,
                    Name = i.Item!.Name,
                    Description = i.Item!.Description,
                    CategoryId = i.Item!.CategoryId,
                    UnitId = i.Item!.UnitId,
                    Unit = new DTO.Unit()
                    {
                        Id = i.Item.Unit!.Id,
                        Name = i.Item.Unit!.Name,
                        Description = i.Item.Unit.Description,
                    }
                }
            }).ToListAsync();
    }

    public override DTO.WarehouseInventory Update(DTO.WarehouseInventory entity)
    {
        entity.LastUpdated = DateTime.UtcNow;
        return base.Update(entity);
    }

    public async Task<DTO.WarehouseInventory?> RemoveAsync(Guid id, Guid userId)
    {
        var entity = await FindAsync(id);
        return entity == null ? null : await RemoveAsync(entity.Id);
    }

    public Task<bool> IsOwnedByUserAsync(Guid id, Guid userId)
    {
        throw new NotImplementedException();
    }
    public Task<IEnumerable<DTO.WarehouseInventory>> AllAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<DTO.WarehouseInventory?> FindAsync(Guid id, Guid userId)
    {
        throw new NotImplementedException();
    }
    
    public async Task<DTO.WarehouseInventory?> FindAsyncWithData(Guid id)
    {
        return await RepositoryDbSet
            .Where(m => m.Id == id)
            .OrderByDescending(i => i.LastUpdated)
            .Select(i => new DTO.WarehouseInventory()
            {
                Id = i.Id,
                Quantity = i.Quantity,
                LocationId = i.LocationId,
                Location = (i.Location == null)
                    ? null
                    : new Location()
                    {
                        Name = i.Location.Name,
                        Description = i.Location.Description
                    },
                LastUpdated = i.LastUpdated,
                WarehouseId = i.WarehouseId,
                Warehouse = new Warehouse()
                {
                    Name = i.Warehouse!.Name,
                    Address = i.Warehouse.Address,
                },
                ItemId = i.ItemId,
                Item = new Item()
                {
                    Id = i.Item!.Id,
                    Name = i.Item!.Name,
                    Description = i.Item!.Description,
                    CategoryId = i.Item!.CategoryId,
                    UnitId = i.Item!.UnitId,
                    Unit = new DTO.Unit()
                    {
                        Id = i.Item.Unit!.Id,
                        Name = i.Item.Unit!.Name,
                        Description = i.Item.Unit.Description,
                    }
                }
            }).FirstOrDefaultAsync();
    }

    
    public async Task<DTO.WarehouseInventory?> IsAlreadyExist(DTO.WarehouseInventory entity)
    {
        return Mapper.Map(await RepositoryDbSet
            .FirstOrDefaultAsync(i => i.WarehouseId == entity.WarehouseId && i.ItemId == entity.ItemId));
    }
    
}