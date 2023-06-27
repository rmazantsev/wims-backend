using BLL.Base;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contracts.App;
using DAL.DTO;

namespace BLL.App.Services;
public class ItemComponentService: 
    BaseEntityService<BLL.DTO.ItemComponent, DAL.DTO.ItemComponent, IItemComponentRepository>, IItemComponentService
{
    protected IAppUOW Uow;

    public ItemComponentService(IAppUOW uow, IMapper<BLL.DTO.ItemComponent, ItemComponent> mapper) 
        : base(uow.ItemComponentRepository, mapper)
    {
        Uow = uow;
    }

    public DTO.ItemComponent? AddOrUpdate(DTO.ItemComponent entity)
    {
        return Mapper.Map(Uow.ItemComponentRepository.AddOrUpdate(Mapper.Map(entity)!));
    }

    public async Task<IEnumerable<DTO.ItemComponent>> AllAsync(Guid userId)
    {
        return (await Uow.ItemComponentRepository.AllAsync(userId)).Select(c => Mapper.Map(c)!);
    }

    public async Task<DTO.ItemComponent?> FindAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await Uow.ItemComponentRepository.FindAsync(id, userId));
    }

    public async Task<DTO.ItemComponent?> RemoveAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await Uow.ItemComponentRepository.RemoveAsync(id, userId));
    }

    public async Task<bool> IsOwnedByUserAsync(Guid id, Guid userId)
    {
        return await Uow.ItemComponentRepository.IsOwnedByUserAsync(id, userId);
    }
    
}