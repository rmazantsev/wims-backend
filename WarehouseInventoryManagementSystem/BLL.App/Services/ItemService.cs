using BLL.Base;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contracts.App;
using DAL.DTO;

namespace BLL.App.Services;
public class ItemService: 
    BaseEntityService<BLL.DTO.Item, DAL.DTO.Item, IItemRepository>, IItemService
{
    protected IAppUOW Uow;

    public ItemService(IAppUOW uow, IMapper<BLL.DTO.Item, Item> mapper) 
        : base(uow.ItemRepository, mapper)
    {
        Uow = uow;
    }


    public async Task<IEnumerable<DTO.Item>> AllAsyncWithData()
    {
        return (await Uow.ItemRepository.AllAsyncWithData()).Select(c => Mapper.Map(c)!);
    }

    public async Task<IEnumerable<DTO.Item>> AllAsync(Guid userId)
    {
        return (await Uow.ItemRepository.AllAsync(userId)).Select(c => Mapper.Map(c)!);
    }

    public async Task<DTO.Item?> FindAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await Uow.ItemRepository.FindAsync(id, userId));
    }

    public async Task<DTO.Item?> RemoveAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await Uow.ItemRepository.RemoveAsync(id, userId));
    }

    public async Task<bool> IsOwnedByUserAsync(Guid id, Guid userId)
    {
        return await Uow.ItemRepository.IsOwnedByUserAsync(id, userId);
    }
    
}