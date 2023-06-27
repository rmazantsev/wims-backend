using DAL.Contracts.App;
using DAL.EF.App.Mappers;
using DAL.EF.App.Repositories;
using DAL.EF.Base;

namespace DAL.EF.App;

public class AppUOW : EFBaseUOW<ApplicationDbContext>, IAppUOW
{
    private readonly AutoMapper.IMapper _mapper;

    public AppUOW(ApplicationDbContext dataContext, AutoMapper.IMapper mapper) : base(dataContext)
    {
        _mapper = mapper;
    }

    private ICategoryRepository? _categoryRepository;
    private IInventoryTransactionRepository? _inventoryTransactionRepository;
    private IItemComponentRepository? _itemComponentRepository;
    private IItemRepository? _itemRepository;
    private ILocationRepository? _locationRepository;
    private IStoreInventoryRepository? _storeInventoryRepository;
    private IStoreRepository? _storeRepository;
    private IUnitRepository? _unitRepository;
    private IWarehouseInventoryRepository? _warehouseInventoryRepository;
    private IWarehouseRepository? _warehouseRepository;

    public ICategoryRepository CategoryRepository =>
        _categoryRepository ??= new CategoryRepository(UowDbContext, new CategoryMapper(_mapper));

    public IInventoryTransactionRepository InventoryTransactionRepository =>
        _inventoryTransactionRepository ??=
            new InventoryTransactionRepository(UowDbContext, new InventoryTransactionMapper(_mapper));

    public IItemComponentRepository ItemComponentRepository =>
        _itemComponentRepository ??= new ItemComponentRepository(UowDbContext, new ItemComponentMapper(_mapper));

    public IItemRepository ItemRepository =>
        _itemRepository ??= new ItemRepository(UowDbContext, new ItemMapper(_mapper));

    public ILocationRepository LocationRepository =>
        _locationRepository ??= new LocationRepository(UowDbContext, new LocationMapper(_mapper));

    public IStoreInventoryRepository StoreInventoryRepository =>
        _storeInventoryRepository ??= new StoreInventoryRepository(UowDbContext, new StoreInventoryMapper(_mapper));

    public IStoreRepository StoreRepository =>
        _storeRepository ??= new StoreRepository(UowDbContext, new StoreMapper(_mapper));

    public IUnitRepository UnitRepository =>
        _unitRepository ??= new UnitRepository(UowDbContext, new UnitMapper(_mapper));

    public IWarehouseInventoryRepository WarehouseInventoryRepository =>
        _warehouseInventoryRepository ??= new WarehouseInventoryRepository(UowDbContext, new WarehouseInventoryMapper(_mapper));

    public IWarehouseRepository WarehouseRepository =>
        _warehouseRepository ??= new WarehouseRepository(UowDbContext, new WarehouseMapper(_mapper));
}