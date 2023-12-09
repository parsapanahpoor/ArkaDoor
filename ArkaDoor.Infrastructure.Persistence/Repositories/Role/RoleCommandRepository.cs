using ArkaDoor.Domain.IRepositories.Role;
using ArkaDoor.Infrastructure.Persistence.ApplicationDbContext;

namespace ArkaDoor.Infrastructure.Persistence.Repositories.Role;

public class RoleCommandRepository : IRoleCommandRepository
{
	#region Ctor

	private readonly AkaDoorDbContext _context;

    public RoleCommandRepository(AkaDoorDbContext context)
    {
        _context = context;
    }

    #endregion
}
