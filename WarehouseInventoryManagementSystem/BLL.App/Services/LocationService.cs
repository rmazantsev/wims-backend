using BLL.Base;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contracts.App;
using DAL.DTO;

namespace BLL.App.Services;
public class LocationService: 
    BaseEntityService<BLL.DTO.Location, DAL.DTO.Location, ILocationRepository>, ILocationService
{
    protected IAppUOW Uow;

    public LocationService(IAppUOW uow, IMapper<BLL.DTO.Location, Location> mapper) 
        : base(uow.LocationRepository, mapper)
    {
        Uow = uow;
    }

    public async Task<IEnumerable<DTO.Location>> AllAsync(Guid userId)
    {
        return (await Uow.LocationRepository.AllAsync(userId)).Select(c => Mapper.Map(c)!);
    }

    public async Task<DTO.Location?> FindAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await Uow.LocationRepository.FindAsync(id, userId));
    }

    public async Task<DTO.Location?> RemoveAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await Uow.LocationRepository.RemoveAsync(id, userId));
    }

    public async Task<bool> IsOwnedByUserAsync(Guid id, Guid userId)
    {
        return await Uow.LocationRepository.IsOwnedByUserAsync(id, userId);
    }
    
}