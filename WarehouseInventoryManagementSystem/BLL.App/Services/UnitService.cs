using BLL.Base;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contracts.App;
using DAL.DTO;

namespace BLL.App.Services;
public class UnitService: 
    BaseEntityService<BLL.DTO.Unit, DAL.DTO.Unit, IUnitRepository>, IUnitService
{
    protected IAppUOW Uow;

    public UnitService(IAppUOW uow, IMapper<BLL.DTO.Unit, Unit> mapper) 
        : base(uow.UnitRepository, mapper)
    {
        Uow = uow;
    }

    public async Task<IEnumerable<DTO.Unit>> AllAsync(Guid userId)
    {
        return (await Uow.UnitRepository.AllAsync(userId)).Select(c => Mapper.Map(c)!);
    }

    public async Task<DTO.Unit?> FindAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await Uow.UnitRepository.FindAsync(id, userId));
    }

    public async Task<DTO.Unit?> RemoveAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await Uow.UnitRepository.RemoveAsync(id, userId));
    }

    public async Task<bool> IsOwnedByUserAsync(Guid id, Guid userId)
    {
        return await Uow.UnitRepository.IsOwnedByUserAsync(id, userId);
    }
    
}