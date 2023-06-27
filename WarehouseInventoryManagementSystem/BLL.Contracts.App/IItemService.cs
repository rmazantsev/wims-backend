using DAL.Contracts.App;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IItemService : IBaseRepository<BLL.DTO.Item>, IItemRepositoryCustom<BLL.DTO.Item>
{
    // add your custom service method here
}