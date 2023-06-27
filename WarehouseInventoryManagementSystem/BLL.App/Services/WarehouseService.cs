using BLL.Base;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contracts.App;
using DAL.DTO;

namespace BLL.App.Services;
public class WarehouseService: 
    BaseEntityService<BLL.DTO.Warehouse, DAL.DTO.Warehouse, IWarehouseRepository>, IWarehouseService
{
    protected IAppUOW Uow;

    public WarehouseService(IAppUOW uow, IMapper<BLL.DTO.Warehouse, Warehouse> mapper) 
        : base(uow.WarehouseRepository, mapper)
    {
        Uow = uow;
    }

    public async Task<DTO.Warehouse?> CheckAndRemoveAsync(Guid id)
    {
        return Mapper.Map(await Uow.WarehouseRepository.CheckAndRemoveAsync(id));
    }

    public async Task<IEnumerable<DTO.Warehouse>> AllAsync(Guid userId)
    {
        return (await Uow.WarehouseRepository.AllAsync(userId)).Select(c => Mapper.Map(c)!);
    }

    public async Task<DTO.Warehouse?> FindAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await Uow.WarehouseRepository.FindAsync(id, userId));
    }

    public async Task<DTO.Warehouse?> RemoveAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await Uow.WarehouseRepository.RemoveAsync(id, userId));
    }

    public async Task<bool> IsOwnedByUserAsync(Guid id, Guid userId)
    {
        return await Uow.WarehouseRepository.IsOwnedByUserAsync(id, userId);
    }
    
    
}