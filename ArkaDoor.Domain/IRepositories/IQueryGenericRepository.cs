namespace ArkaDoor.Domain.IRepositories;

public interface IQueryGenericRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> Table { get; }

    IQueryable<TEntity> TableNoTracking { get; }

    TEntity GetById(params object[] ids);

    Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);
}
