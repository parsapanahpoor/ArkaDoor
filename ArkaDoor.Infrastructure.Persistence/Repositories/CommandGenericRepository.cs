﻿#region Using

using ArkaDoor.Domain.IRepositories;
using ArkaDoor.Infrastructure.Persistence.ApplicationDbContext;
using ArkaDoor.Utilitiese;
using Microsoft.EntityFrameworkCore;

namespace ArkaDoor.Infrastructure.Persistence.Repositories;

#endregion

public class CommandGenericRepository<TEntity> where TEntity : class
{
    #region Ctor

    protected readonly AkaDoorDbContext _context;

    public DbSet<TEntity> Entities { get; }

    public CommandGenericRepository(AkaDoorDbContext context)
    {
        _context = context;
        Entities = _context.Set<TEntity>() ?? throw new ArgumentNullException(nameof(TEntity));
        _context.SaveChangesAsync();
    }

    #endregion

    #region Async Methods

    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        Assert.NotNull(entity, nameof(entity));
        await Entities.AddAsync(entity, cancellationToken);
    }

    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
    {
        Assert.NotNull(entities, nameof(entities));
        await Entities.AddRangeAsync(entities, cancellationToken);
    }

    public virtual void Add(TEntity entity)
    {
        Assert.NotNull(entity, nameof(entity));
        Entities.Add(entity);
    }

    public virtual void AddRange(IEnumerable<TEntity> entities)
    {
        Assert.NotNull(entities, nameof(entities));
        Entities.AddRange(entities);
    }

    public virtual void Update(TEntity entity)
    {
        Assert.NotNull(entity, nameof(entity));
        _context.Update(entity);
    }

    public virtual void Delete(TEntity entity)
    {
        Assert.NotNull(entity, nameof(entity));
        _context.Remove(entity);
    }

    public virtual void DeleteRange(IEnumerable<TEntity> entities)
    {
        Assert.NotNull(entities, nameof(entities));
        _context.Remove(entities);
    }

    #endregion
}
