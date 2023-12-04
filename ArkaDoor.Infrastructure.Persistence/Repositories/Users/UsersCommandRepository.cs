#region Usings

using ArkaDoor.Domain.Entities.Users;
using ArkaDoor.Domain.IRepositories.Users;
using ArkaDoor.Infrastructure.Persistence.ApplicationDbContext;

namespace ArkaDoor.Infrastructure.Persistence.Repositories.Users;

#endregion

public class UsersCommandRepository : CommandGenericRepository<User> , IUsersCommandRepository 
{
    #region Ctor

    private readonly AkaDoorDbContext _context;

    public UsersCommandRepository(AkaDoorDbContext context) : base(context)
    {
        _context = context;
    }

    #endregion
}
