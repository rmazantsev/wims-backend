using Contracts.Base;
using DAL.Contracts.Base;
using Domain.Contracts.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Base;

public class EFBaseRepository<TEntity, TDalEntity, TDbContext>: EFBaseRepository<TEntity,TDalEntity, Guid, TDbContext>, IBaseRepository<TDalEntity>
    where TEntity : class, IDomainEntityId
    where TDbContext: DbContext
    where TDalEntity : class, IDomainEntityId
{
    public EFBaseRepository(TDbContext dataContext, IMapper<TDalEntity, TEntity> mapper) : base(dataContext, mapper)
    {
    }
}

public class EFBaseRepository<TEntity, TDalEntity, TKey, TDbContext>: IBaseRepository<TDalEntity, TKey> 
    where TEntity : class, IDomainEntityId<TKey> 
    where TKey : struct, IEquatable<TKey>
    where TDbContext: DbContext
    where TDalEntity : class, IDomainEntityId<TKey>
{
    protected TDbContext RepositoryDbContext;
    protected DbSet<TEntity> RepositoryDbSet;
    protected readonly IMapper<TDalEntity, TEntity> Mapper;

    public EFBaseRepository(TDbContext dataContext, IMapper<TDalEntity, TEntity> mapper){
        RepositoryDbContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        Mapper = mapper;
        RepositoryDbSet = RepositoryDbContext.Set<TEntity>();
    }
    
    public virtual async Task<IEnumerable<TDalEntity>> AllAsync()
    {
        return await RepositoryDbSet
            .Select(e => Mapper.Map(e)!)
            .ToListAsync();
    }

    public virtual async Task<TDalEntity?> FindAsync(TKey id)
    {
        return Mapper.Map(await RepositoryDbSet.FindAsync(id));
    }

    public virtual TDalEntity Add(TDalEntity entity)
    {
        return Mapper.Map(RepositoryDbSet.Add(Mapper.Map(entity)!).Entity)!;
    }

    public virtual TDalEntity Update(TDalEntity entity)
    {
        return Mapper.Map(RepositoryDbSet.Update(Mapper.Map(entity)!).Entity)!;
    }

    public virtual TDalEntity Remove(TDalEntity entity)
    {
        return Mapper.Map(RepositoryDbSet.Remove(Mapper.Map(entity)!).Entity)!;
    }

    public virtual async Task<TDalEntity?> RemoveAsync(TKey id)
    {
        var entity = await FindAsync(id);
        return entity != null ? Remove(entity) : null;
    }
}