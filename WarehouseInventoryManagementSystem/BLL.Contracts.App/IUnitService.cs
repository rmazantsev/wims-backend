using DAL.Contracts.App;
using DAL.Contracts.Base;
using Domain.App;

namespace BLL.Contracts.App;

public interface IUnitService: IBaseRepository<BLL.DTO.Unit>, IUnitRepositoryCustom<BLL.DTO.Unit>
{
    // add your custom service method here
}