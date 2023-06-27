﻿using Domain.Contracts.Base;

namespace DAL.Contracts.Base;

public interface IBaseRepository<TDalEntity> : IBaseRepository<TDalEntity, Guid>
    where TDalEntity : class, IDomainEntityId
{
    
}

public interface IBaseRepository<TEntity, in TKey>
    where TEntity : class, IDomainEntityId<TKey>
    where TKey : struct, IEquatable<TKey>
{
    // IEnumerable<TEntity> All();
    Task<IEnumerable<TEntity>> AllAsync();
    
    // TEntity Find(TKey id);
    Task<TEntity?> FindAsync(TKey id);

    TEntity Add(TEntity entity);

    TEntity Update(TEntity entity);

    TEntity Remove(TEntity entity);

    Task<TEntity?> RemoveAsync(TKey id);
}