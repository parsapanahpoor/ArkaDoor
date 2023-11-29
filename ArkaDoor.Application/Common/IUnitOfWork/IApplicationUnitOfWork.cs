namespace ArkaDoor.Application.Common.IUnitOfWork;

public interface IApplicationUnitOfWork : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Save all entities in to database.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<SaveChangesResult> SaveChangesAsync(CancellationToken cancellationToken = default);
}
