using Contracts.Base;
using DAL.Contracts.App;
using DAL.DTO;
using Domain.App.Identity;
using Microsoft.EntityFrameworkCore;
using InventoryTransaction = Domain.App.InventoryTransaction;

namespace DAL.EF.App.Repositories;

public class InventoryTransactionRepository : BaseRepository<InventoryTransaction, DAL.DTO.InventoryTransaction>, IInventoryTransactionRepository
{
    
    public InventoryTransactionRepository(ApplicationDbContext dataContext, IMapper<DTO.InventoryTransaction, InventoryTransaction> mapper) : base(dataContext, mapper)
    {
    }

    public override async Task<IEnumerable<DTO.InventoryTransaction>> AllAsync()
    {
        return (await RepositoryDbSet
            .OrderByDescending(i => i.TimeStamp)
            .Select(c => new DAL.DTO.InventoryTransaction()
            {
                Id = c.Id,
                Quantity = c.Quantity,
                Warehouse = c.Warehouse != null ? new DAL.DTO.Warehouse()
                {
                    Id = c.Warehouse.Id,
                    Name = c.Warehouse.Name,
                    Address = c.Warehouse.Address,
                } : null,
                TransactionType = c.TransactionType,
                Description = c.Description,
                TimeStamp = c.TimeStamp,
                Store = c.Store != null ? new DAL.DTO.Store()
                {
                    Id = c.Store.Id,
                    Name = c.Store.Name,
                    Address = c.Store.Address,
                } : null,
                Item = new Item()
                {
                    Id = c.Item!.Id,
                    Name = c.Item!.Name,
                    Description = c.Item!.Description,
                    Unit = new Unit()
                    {
                        Id = c.Item.Unit!.Id,
                        Name = c.Item.Unit.Name,
                        Description = c.Item.Unit.Description,
                    }
                },
                AppUser = new AppUser()
                {
                    FirstName = c.AppUser!.FirstName,
                    LastName = c.AppUser.LastName,
                }
            }).ToListAsync())!;
    }
    
    public async Task<IEnumerable<DTO.InventoryTransaction?>> AllAsyncById(Guid id)
    {
        return (await RepositoryDbSet
            .Where(i => i.WarehouseId == id || i.StoreId == id)
            .Select(c => new DAL.DTO.InventoryTransaction()
            {
                Id = c.Id,
                Quantity = c.Quantity,
                Warehouse = c.Warehouse != null ? new DAL.DTO.Warehouse()
                {
                    Id = c.Warehouse.Id,
                    Name = c.Warehouse.Name,
                    Address = c.Warehouse.Address,
                } : null,
                TransactionType = c.TransactionType,
                Description = c.Description,
                TimeStamp = c.TimeStamp,
                Store = c.Store != null ? new DAL.DTO.Store()
                {
                    Id = c.Store.Id,
                    Name = c.Store.Name,
                    Address = c.Store.Address,
                } : null,
                Item = new Item()
                {
                    Name = c.Item!.Name,
                    Description = c.Item!.Description,
                    Unit = new Unit()
                    {
                        Id = c.Item.Unit!.Id,
                        Name = c.Item.Unit.Name,
                        Description = c.Item.Unit.Description,
                    }
                },
                AppUser = new AppUser()
                {
                    FirstName = c.AppUser!.FirstName,
                    LastName = c.AppUser.LastName,
                }
            }).ToListAsync())!;
    }

    public async Task<DTO.InventoryTransaction?> FindAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await RepositoryDbSet
            .FirstOrDefaultAsync(m => m.Id == id));
    }

    public async Task<DTO.InventoryTransaction?> RemoveAsync(Guid id, Guid userId)
    {
        var entity = await FindAsync(id, userId);
        return entity == null ? null : await RemoveAsync(entity.Id);
    }

    public async Task<bool> IsOwnedByUserAsync(Guid id, Guid userId)
    {
        return await RepositoryDbSet.AnyAsync(t => t.Id == id && t.AppUserId == userId);
    }

}