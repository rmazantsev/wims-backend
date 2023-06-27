using BLL.Base;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contracts.App;
using DAL.DTO;
using DAL.EF.Base;

namespace BLL.App.Services;
public class StoreInventoryService: 
    BaseEntityService<BLL.DTO.StoreInventory, DAL.DTO.StoreInventory, IStoreInventoryRepository>, IStoreInventoryService
{
    protected IAppUOW Uow;

    public StoreInventoryService(IAppUOW uow, IMapper<BLL.DTO.StoreInventory, StoreInventory> mapper) 
        : base(uow.StoreInventoryRepository, mapper)
    {
        Uow = uow;
    }

    public async Task<DTO.StoreInventory?> FindAsyncWithData(Guid id)
    {
        return Mapper.Map(await Uow.StoreInventoryRepository.FindAsyncWithData(id));
    }

    public async Task<IEnumerable<DTO.StoreInventory>> AllAsync(Guid userId)
    {
        return (await Uow.StoreInventoryRepository.AllAsync(userId)).Select(c => Mapper.Map(c)!);
    }

    public async Task<DTO.StoreInventory?> FindAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await Uow.StoreInventoryRepository.FindAsync(id, userId));
    }
    
    public async Task<DTO.StoreInventory?> RemoveAsync(Guid id, Guid userId)
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
            StoreId = entity.StoreId,
            AppUserId = userId,
        });
        return Mapper.Map(await Uow.StoreInventoryRepository.RemoveAsync(id, userId));
    }

    public async Task<bool> IsOwnedByUserAsync(Guid id, Guid userId)
    {
        return await Uow.StoreInventoryRepository.IsOwnedByUserAsync(id, userId);
    }

    public async Task<DTO.StoreInventory?> AddWithTransaction(DTO.StoreInventory entity, Guid userId)
    {
        var existingComponent = await Uow.StoreInventoryRepository.IsAlreadyExist(Mapper.Map(entity)!);
        if (existingComponent?.Quantity == entity.Quantity
            && existingComponent.StoreId == entity.StoreId
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
                StoreId = entity.StoreId,
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
                StoreId = entity.StoreId,
                AppUserId = userId,
            });
            entity.Id = existingComponent.Id;
            return Update(entity);
        }
    }

    public async Task<DTO.StoreInventory?> UpdateWithTransaction(DTO.StoreInventory entity, Guid userId)
    {
        var oldEntity = await FindAsync(entity.Id);
        if (oldEntity == null)
        {
            return null;
        }
        if (oldEntity.Quantity == entity.Quantity
            && oldEntity.StoreId == entity.StoreId
            && oldEntity.ItemId == entity.ItemId)
        {
            return null;
        }

        var differenceInAmount = entity.Quantity - oldEntity.Quantity;
        var transactionType = oldEntity.Quantity < entity.Quantity
            ? (char)TransactionType.Addition
            : (char)TransactionType.Withdrawing;
        if (entity.StoreId == oldEntity.StoreId &&
            entity.ItemId == oldEntity.ItemId)
        {
            Uow.InventoryTransactionRepository.Add(new InventoryTransaction()
            {
                ItemId = entity.ItemId,
                Quantity = Math.Abs(differenceInAmount),
                TransactionType = transactionType,
                TimeStamp = DateTime.UtcNow,
                StoreId = entity.StoreId,
                AppUserId = userId,
            });
            return Mapper.Map(Uow.StoreInventoryRepository.Update(Mapper.Map(entity)!));
        }
        else
        {
            Uow.InventoryTransactionRepository.Add(new InventoryTransaction()
            {
                ItemId = oldEntity.ItemId,
                Quantity = oldEntity.Quantity,
                TransactionType = (char)TransactionType.Withdrawing,
                TimeStamp = DateTime.UtcNow,
                StoreId = oldEntity.StoreId,
                AppUserId = userId,
            });
            Uow.InventoryTransactionRepository.Add(new InventoryTransaction()
            {
                ItemId = entity.ItemId,
                Quantity = entity.Quantity,
                TransactionType = (char)TransactionType.Addition,
                TimeStamp = DateTime.UtcNow,
                StoreId = entity.StoreId,
                AppUserId = userId,
            });
            return Mapper.Map(Uow.StoreInventoryRepository.Update(Mapper.Map(entity)!));
        }
    }
}