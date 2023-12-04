#region Using

using ArkaDoor.Domain.Entities.Users;
using ArkaDoor.Domain.IRepositories.Users;
using ArkaDoor.Infrastructure.Persistence.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace ArkaDoor.Infrastructure.Persistence.Repositories.Users;

#endregion

public class UsersQueryRepository : QueryGenericRepository<User> , IUserQueryRepository
{
    #region Ctor

    private readonly AkaDoorDbContext _context;

    public UsersQueryRepository(AkaDoorDbContext context) : base(context)
    {
        _context = context;
    }

    #endregion

    #region General 

    public async Task<bool> IsExistUserByMobileAsync(string mobile , CancellationToken cancellationToken)
    {
        return await _context.Users
                             .AsNoTracking()
                             .AnyAsync(p => p.Mobile == mobile);
    }

    #endregion
}
