using BLL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IAppBLL: IBaseBLL
{
    IItemService ItemService { get; }
    ICategoryService CategoryService { get; }
    IInventoryTransactionService InventoryTransactionService { get; }
    IItemComponentService ItemComponentService { get; }
    ILocationService LocationService { get; }
    IStoreInventoryService StoreInventoryService { get; }
    IStoreService StoreService { get; }
    IUnitService UnitService { get; }
    IWarehouseInventoryService WarehouseInventoryService { get; }
    IWarehouseService WarehouseService { get; }
}