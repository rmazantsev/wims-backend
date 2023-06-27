using BLL.Base;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contracts.App;
using DAL.DTO;

namespace BLL.App.Services;

public class StoreService :
    BaseEntityService<BLL.DTO.Store, DAL.DTO.Store, IStoreRepository>, IStoreService
{
    protected IAppUOW Uow;

    public StoreService(IAppUOW uow, IMapper<BLL.DTO.Store, Store> mapper)
        : base(uow.StoreRepository, mapper)
    {
        Uow = uow;
    }

    public async Task<DTO.Store?> CheckAndRemoveAsync(Guid id)
    {
        return Mapper.Map(await Uow.StoreRepository.CheckAndRemoveAsync(id));
    }

    public async Task<IEnumerable<DTO.Store>> AllAsync(Guid userId)
    {
        return (await Uow.StoreRepository.AllAsync(userId)).Select(c => Mapper.Map(c)!);
    }

    public async Task<DTO.Store?> FindAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await Uow.StoreRepository.FindAsync(id, userId));
    }

    public async Task<DTO.Store?> RemoveAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await Uow.StoreRepository.RemoveAsync(id, userId));
    }

    public async Task<bool> IsOwnedByUserAsync(Guid id, Guid userId)
    {
        return await Uow.StoreRepository.IsOwnedByUserAsync(id, userId);
    }
}