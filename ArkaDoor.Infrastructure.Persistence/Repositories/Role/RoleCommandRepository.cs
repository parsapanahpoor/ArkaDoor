using ArkaDoor.Domain.IRepositories.Role;
using ArkaDoor.Infrastructure.Persistence.ApplicationDbContext;

namespace ArkaDoor.Infrastructure.Persistence.Repositories.Role;

public class RoleCommandRepository : CommandGenericRepository<Domain.Entities.Account.Role>,  IRoleCommandRepository 
{
	#region Ctor

	private readonly AkaDoorDbContext _context;

    public RoleCommandRepository(AkaDoorDbContext context) : base(context)
    {
        _context = context;
    }

    #endregion

    #region Admin Panel 

    #endregion
}
