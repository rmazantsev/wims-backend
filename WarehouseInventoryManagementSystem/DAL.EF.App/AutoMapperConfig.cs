using AutoMapper;
using Domain.App;

namespace DAL.EF.App;

public class AutoMapperConfig: Profile
{
    public AutoMapperConfig()
    {
        CreateMap<DAL.DTO.Category, Category>().ReverseMap();
        CreateMap<DAL.DTO.Item, Item>().ReverseMap();
        CreateMap<DAL.DTO.Warehouse, Warehouse>().ReverseMap();
        CreateMap<DAL.DTO.WarehouseInventory, WarehouseInventory>().ReverseMap();
        CreateMap<DAL.DTO.Store, Store>().ReverseMap();
        CreateMap<DAL.DTO.StoreInventory, StoreInventory>().ReverseMap();
        CreateMap<DAL.DTO.Location, Location>().ReverseMap();
        CreateMap<DAL.DTO.ItemComponent, ItemComponent>().ReverseMap();
        CreateMap<DAL.DTO.InventoryTransaction, InventoryTransaction>().ReverseMap();
        CreateMap<DAL.DTO.Unit, Unit>().ReverseMap();
        // CreateMap<DAL.DTO.ItemWithItemComponents, Item>(); todo do it in repo
    }
    
}