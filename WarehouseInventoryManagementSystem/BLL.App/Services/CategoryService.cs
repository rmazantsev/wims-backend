using BLL.Base;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contracts.App;
using DAL.DTO;

namespace BLL.App.Services;
public class CategoryService: 
    BaseEntityService<BLL.DTO.Category, DAL.DTO.Category, ICategoryRepository>, ICategoryService
{
    protected IAppUOW Uow;

    public CategoryService(IAppUOW uow, IMapper<BLL.DTO.Category, Category> mapper) 
        : base(uow.CategoryRepository, mapper)
    {
        Uow = uow;
    }

    public async Task<IEnumerable<DTO.Category>> AllAsync(Guid userId)
    {
        return (await Uow.CategoryRepository.AllAsync(userId)).Select(c => Mapper.Map(c)!);
    }

    public async Task<DTO.Category?> FindAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await Uow.CategoryRepository.FindAsync(id, userId));
    }

    public async Task<DTO.Category?> RemoveAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await Uow.CategoryRepository.RemoveAsync(id, userId));
    }

    public async Task<bool> IsOwnedByUserAsync(Guid id, Guid userId)
    {
        return await Uow.CategoryRepository.IsOwnedByUserAsync(id, userId);
    }
    
}