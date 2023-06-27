using DAL.Contracts.App;
using DAL.Contracts.Base;
using Domain.App;

namespace BLL.Contracts.App;

public interface ILocationService: IBaseRepository<BLL.DTO.Location>, ILocationRepositoryCustom<BLL.DTO.Location>
{
    // add your custom service method here
}