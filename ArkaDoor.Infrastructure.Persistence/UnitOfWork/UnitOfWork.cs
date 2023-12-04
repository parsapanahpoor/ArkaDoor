using ArkaDoor.Application.Common.IUnitOfWork;
using ArkaDoor.Infrastructure.Persistence.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace ArkaDoor.Infrastructure.Persistence.UnitOfWork;

public partial class UnitOfWork : IUnitOfWork
{
    #region Using

    private readonly AkaDoorDbContext _context;

    public UnitOfWork(AkaDoorDbContext context)
    {
        _context = context;
    }

    #endregion

    #region Save Changes

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    #endregion

    #region Dispose

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
        GC.SuppressFinalize(this);
    }

    #endregion
}
