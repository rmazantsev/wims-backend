using BLL.Base;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contracts.App;
using DAL.DTO;
using DAL.EF.Base;
using Microsoft.AspNetCore.Identity;

namespace BLL.App.Services;

public class WarehouseInventoryService :
    BaseEntityService<BLL.DTO.WarehouseInventory, DAL.DTO.WarehouseInventory, IWarehouseInventoryRepository>,
    IWarehouseInventoryService
{
    protected IAppUOW Uow;

    public WarehouseInventoryService(IAppUOW uow, IMapper<BLL.DTO.WarehouseInventory, WarehouseInventory> mapper)
        : base(uow.WarehouseInventoryRepository, mapper)
    {
        Uow = uow;
    }

    public async Task<DTO.WarehouseInventory?> FindAsyncWithData(Guid id)
    {
        return Mapper.Map(await Uow.WarehouseInventoryRepository.FindAsyncWithData(id));
    }

    public async Task<IEnumerable<DTO.WarehouseInventory>> AllAsync(Guid userId)
    {
        return (await Uow.WarehouseInventoryRepository.AllAsync(userId)).Select(c => Mapper.Map(c)!);
    }

    public async Task<DTO.WarehouseInventory?> FindAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await Uow.WarehouseInventoryRepository.FindAsync(id, userId));
    }

    public async Task<DTO.WarehouseInventory?> RemoveAsync(Guid id, Guid userId)
    {
        var entity = await FindAsync(id);
        if (entity == null)
        {
            return null;
        }

        Uow.InventoryTransactionRepository.Add(new InventoryTransaction()
        {
            ItemId = entity.ItemId,
            Quantity = entity.Quantity,
            TransactionType = (char)TransactionType.Withdrawing,
            TimeStamp = DateTime.UtcNow,
            WarehouseId = entity.WarehouseId,
            AppUserId = userId,
        });
        return Mapper.Map(await Uow.WarehouseInventoryRepository.RemoveAsync(id));
    }

    public async Task<bool> IsOwnedByUserAsync(Guid id, Guid userId)
    {
        return await Uow.WarehouseInventoryRepository.IsOwnedByUserAsync(id, userId);
    }

    public async Task<BLL.DTO.WarehouseInventory?> AddWithTransaction(BLL.DTO.WarehouseInventory entity, Guid userId)
    {
        var existingComponent = await Uow.WarehouseInventoryRepository.IsAlreadyExist(Mapper.Map(entity)!);
        if (existingComponent?.Quantity == entity.Quantity
            && existingComponent.WarehouseId == entity.WarehouseId
            && existingComponent.ItemId == entity.ItemId)
        {
            return null;
        }
        if (existingComponent == null)
        {
            Uow.InventoryTransactionRepository.Add(new InventoryTransaction()
            {
                ItemId = entity.ItemId,
                Quantity = entity.Quantity,
                TransactionType = (char)TransactionType.Addition,
                TimeStamp = DateTime.UtcNow,
                WarehouseId = entity.WarehouseId,
                AppUserId = userId,
            });
            return Add(entity);
        }
        else
        {
            var differenceInAmount = entity.Quantity - existingComponent.Quantity;
            var transactionType = existingComponent.Quantity < entity.Quantity
                ? (char)TransactionType.Addition
                : (char)TransactionType.Withdrawing;
            Uow.InventoryTransactionRepository.Add(new InventoryTransaction()
            {
                ItemId = entity.ItemId,
                Quantity = Math.Abs(differenceInAmount),
                TransactionType = transactionType,
                TimeStamp = DateTime.UtcNow,
                WarehouseId = entity.WarehouseId,
                AppUserId = userId,
            });
            entity.Id = existingComponent.Id;
            return Update(entity);
        }
    }

    public async Task<DTO.WarehouseInventory?> UpdateWithTransaction(DTO.WarehouseInventory entity, Guid userId)
    {
        var oldEntity = await FindAsync(entity.Id);
        if (oldEntity == null)
        {
            return null;
        }
        if (oldEntity.Quantity == entity.Quantity
            && oldEntity.WarehouseId == entity.WarehouseId
            && oldEntity.ItemId == entity.ItemId)
        {
            return null;
        }
        var differenceInAmount = entity.Quantity - oldEntity.Quantity;
        var transactionType = oldEntity.Quantity < entity.Quantity
            ? (char)TransactionType.Addition
            : (char)TransactionType.Withdrawing;
        if (entity.WarehouseId == oldEntity.WarehouseId &&
            entity.ItemId == oldEntity.ItemId)
        {
            Uow.InventoryTransactionRepository.Add(new InventoryTransaction()
            {
                ItemId = entity.ItemId,
                Quantity = Math.Abs(differenceInAmount),
                TransactionType = transactionType,
                TimeStamp = DateTime.UtcNow,
                WarehouseId = entity.WarehouseId,
                AppUserId = userId,
            });
            return Mapper.Map(Uow.WarehouseInventoryRepository.Update(Mapper.Map(entity)!));
        }
        else
        {
            Uow.InventoryTransactionRepository.Add(new InventoryTransaction()
            {
                ItemId = oldEntity.ItemId,
                Quantity = oldEntity.Quantity,
                TransactionType = (char)TransactionType.Withdrawing,
                TimeStamp = DateTime.UtcNow,
                WarehouseId = oldEntity.WarehouseId,
                AppUserId = userId,
            });
            Uow.InventoryTransactionRepository.Add(new InventoryTransaction()
            {
                ItemId = entity.ItemId,
                Quantity = entity.Quantity,
                TransactionType = (char)TransactionType.Addition,
                TimeStamp = DateTime.UtcNow,
                WarehouseId = entity.WarehouseId,
                AppUserId = userId,
            });
            return Mapper.Map(Uow.WarehouseInventoryRepository.Update(Mapper.Map(entity)!));
        }
    }
}