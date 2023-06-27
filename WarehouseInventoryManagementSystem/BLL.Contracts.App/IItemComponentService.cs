using DAL.Contracts.App;
using DAL.Contracts.Base;
using Domain.App;

namespace BLL.Contracts.App;

public interface IItemComponentService : IBaseRepository<BLL.DTO.ItemComponent>,
    IItemComponentRepositoryCustom<BLL.DTO.ItemComponent>
{
    // add your custom service method here
}