using AutoMapper;

namespace Public.DTO;

public class AutomapperConfig: Profile
{
    public AutomapperConfig()
    {
        CreateMap<BLL.DTO.Category, v1.Category>().ReverseMap();
        CreateMap<BLL.DTO.Item, v1.Item>().ReverseMap();
        CreateMap<BLL.DTO.Warehouse, v1.Warehouse>().ReverseMap();
        CreateMap<BLL.DTO.WarehouseInventory, v1.WarehouseInventory>().ReverseMap();
        CreateMap<BLL.DTO.Store, v1.Store>().ReverseMap();
        CreateMap<BLL.DTO.StoreInventory, v1.StoreInventory>().ReverseMap();
        CreateMap<BLL.DTO.Location, v1.Location>().ReverseMap();
        CreateMap<BLL.DTO.ItemComponent, v1.ItemComponent>().ReverseMap();
        CreateMap<BLL.DTO.InventoryTransaction, v1.InventoryTransaction>();
        CreateMap<BLL.DTO.Unit, v1.Unit>().ReverseMap();
        CreateMap<BLL.DTO.ItemWithItemComponents, v1.ItemWithItemComponents>().ReverseMap();
        CreateMap<Domain.App.Identity.AppUser, v1.Identity.AppUser>().ReverseMap();
    }
}