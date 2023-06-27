using AutoMapper;
using BLL.App.Mappers;
using BLL.App.Services;
using BLL.Base;
using BLL.Contracts.App;
using DAL.Contracts.App;

namespace BLL.App;

public class AppBLL : BaseBLL<IAppUOW>, IAppBLL
{
    private readonly IMapper _mapper;

    public AppBLL(IAppUOW uow, IMapper mapper) : base(uow)
    {
        _mapper = mapper;
    }

    private IItemService? _itemService;
    private ICategoryService? _categoryService;
    private IInventoryTransactionService? _inventoryTransactionService;
    private IItemComponentService? _itemComponentService;
    private ILocationService? _locationService;
    private IStoreInventoryService? _storeInventoryService;
    private IStoreService? _storeService;
    private IUnitService? _unitService;
    private IWarehouseInventoryService? _warehouseInventoryService;
    private IWarehouseService? _warehouseService;

    public IItemService ItemService =>
        _itemService ??= new ItemService(Uow, new ItemMapper(_mapper));

    public ICategoryService CategoryService =>
        _categoryService ??= new CategoryService(Uow, new CategoryMapper(_mapper));

    public IInventoryTransactionService InventoryTransactionService =>
        _inventoryTransactionService ??= new InventoryTransactionService(Uow, new InventoryTransactionMapper(_mapper));

    public IItemComponentService ItemComponentService =>
        _itemComponentService ??= new ItemComponentService(Uow, new ItemComponentMapper(_mapper));

    public ILocationService LocationService =>
        _locationService ??= new LocationService(Uow, new LocationMapper(_mapper));

    public IStoreInventoryService StoreInventoryService =>
        _storeInventoryService ??= new StoreInventoryService(Uow, new StoreInventoryMapper(_mapper));

    public IStoreService StoreService => _storeService ??= new StoreService(Uow, new StoreMapper(_mapper));

    public IUnitService UnitService => _unitService ??= new UnitService(Uow, new UnitMapper(_mapper));

    public IWarehouseInventoryService WarehouseInventoryService =>
        _warehouseInventoryService ??= new WarehouseInventoryService(Uow, new WarehouseInventoryMapper(_mapper));

    public IWarehouseService WarehouseService =>
        _warehouseService ??= new WarehouseService(Uow, new WarehouseMapper(_mapper));
}