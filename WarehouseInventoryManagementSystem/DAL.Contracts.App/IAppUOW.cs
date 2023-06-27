using DAL.Contracts.Base;

namespace DAL.Contracts.App;

public interface IAppUOW: IBaseUOW
{
    // list your repositories 
    ICategoryRepository CategoryRepository { get; }
    IInventoryTransactionRepository InventoryTransactionRepository { get; }
    IItemComponentRepository ItemComponentRepository { get; }
    IItemRepository ItemRepository { get; }
    ILocationRepository LocationRepository { get; }
    IStoreInventoryRepository StoreInventoryRepository { get; }
    IStoreRepository StoreRepository { get; }
    IUnitRepository UnitRepository { get; }
    IWarehouseInventoryRepository WarehouseInventoryRepository { get; }
    IWarehouseRepository WarehouseRepository { get; }
}