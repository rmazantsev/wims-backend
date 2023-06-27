using DAL.Contracts.App;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IWarehouseService : IBaseRepository<BLL.DTO.Warehouse>, IWarehouseRepositoryCustom<BLL.DTO.Warehouse>
{
    // add your custom service method here
}