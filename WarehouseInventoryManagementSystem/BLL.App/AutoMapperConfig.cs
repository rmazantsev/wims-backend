using AutoMapper;

namespace BLL.App;

public class AutoMapperConfig: Profile
{
    public AutoMapperConfig()
    {
        CreateMap<DAL.DTO.Category, BLL.DTO.Category>().ReverseMap();
        CreateMap<DAL.DTO.Item, BLL.DTO.Item>().ReverseMap();
        CreateMap<DAL.DTO.Warehouse, BLL.DTO.Warehouse>().ReverseMap();
        CreateMap<DAL.DTO.WarehouseInventory, BLL.DTO.WarehouseInventory>().ReverseMap();
        CreateMap<DAL.DTO.Store, BLL.DTO.Store>().ReverseMap();
        CreateMap<DAL.DTO.StoreInventory, BLL.DTO.StoreInventory>().ReverseMap();
        CreateMap<DAL.DTO.Location, BLL.DTO.Location>().ReverseMap();
        CreateMap<DAL.DTO.ItemComponent, BLL.DTO.ItemComponent>().ReverseMap();
        CreateMap<DAL.DTO.InventoryTransaction, BLL.DTO.InventoryTransaction>().ReverseMap();
        CreateMap<DAL.DTO.Unit, BLL.DTO.Unit>().ReverseMap();
        CreateMap<DAL.DTO.ItemWithItemComponents, BLL.DTO.ItemWithItemComponents>();
    }
    
}