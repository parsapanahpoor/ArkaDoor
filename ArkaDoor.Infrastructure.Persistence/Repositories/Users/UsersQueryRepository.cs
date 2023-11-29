#region Using

using ArkaDoor.Domain.Entities.Users;
using ArkaDoor.Domain.IRepositories.Users;
using ArkaDoor.Infrastructure.Persistence.ApplicationDbContext;

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
}
