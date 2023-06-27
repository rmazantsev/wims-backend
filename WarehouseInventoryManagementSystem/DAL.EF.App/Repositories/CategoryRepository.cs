using Contracts.Base;
using DAL.Contracts.App;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class CategoryRepository : BaseRepository<Category, DAL.DTO.Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext dataContext, IMapper<DTO.Category, Category> mapper)
        : base(dataContext, mapper)
    {   
    }
    
    public async Task<IEnumerable<DTO.Category>> AllAsync(Guid userId)
    {
        return (await RepositoryDbSet
            .Select(c => Mapper.Map(c))
            .ToListAsync())!;
    }

    public async Task<DTO.Category?> FindAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await RepositoryDbSet
            .FirstOrDefaultAsync(m => m.Id == id));
    }

    public async Task<DTO.Category?> RemoveAsync(Guid id, Guid userId)
    {
        var entity = await FindAsync(id, userId);
         return entity == null ? null : await RemoveAsync(entity.Id);
    }

    public Task<bool> IsOwnedByUserAsync(Guid id, Guid userId)
    {
        throw new NotImplementedException();
    }
}