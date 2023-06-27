using DAL.Contracts.App;
using DAL.Contracts.Base;
using Domain.App;

namespace BLL.Contracts.App;

public interface ICategoryService: IBaseRepository<BLL.DTO.Category>, ICategoryRepositoryCustom<BLL.DTO.Category>
{
    // add your custom service method here
}