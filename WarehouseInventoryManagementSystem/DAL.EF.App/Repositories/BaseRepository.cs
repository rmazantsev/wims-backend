using Contracts.Base;
using DAL.EF.Base;
using Domain.Contracts.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class BaseRepository<TEntity, TDalEntity>: BaseRepository<TEntity, TDalEntity, ApplicationDbContext> 
    where TEntity : class, IDomainEntityId
    where TDalEntity : class, IDomainEntityId
{
    public BaseRepository(ApplicationDbContext dataContext, IMapper<TDalEntity, TEntity> mapper) : base(dataContext, mapper)
    {
    }
}

public class BaseRepository<TEntity, TDalEntity, TDbContext>: EFBaseRepository<TEntity, TDalEntity, TDbContext> 
    where TEntity : class, IDomainEntityId 
    where TDbContext : DbContext
    where TDalEntity : class, IDomainEntityId
{
    public BaseRepository(TDbContext dataContext, IMapper<TDalEntity, TEntity> mapper) : base(dataContext, mapper)
    {
    }
    
}