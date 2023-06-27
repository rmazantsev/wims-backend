using DAL.Contracts.App;
using DAL.Contracts.Base;
using Domain.App;

namespace BLL.Contracts.App;

public interface IStoreService: IBaseRepository<BLL.DTO.Store>, IStoreRepositoryCustom<BLL.DTO.Store>
{
    // add your custom service method here
}