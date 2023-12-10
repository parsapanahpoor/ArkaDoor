using ArkaDoor.Domain.Entities.Account;
using ArkaDoor.Domain.IRepositories.Role;
using ArkaDoor.Infrastructure.Persistence.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

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

    public async Task RemoveUserRolesByUserId(ulong userId , CancellationToken cancellationToken)
    {
        var userSelectedRoles = await _context.UserRoles
                                              .AsNoTracking()
                                              .Where(p => p.UserId == userId)
                                              .ToListAsync();

        _context.UserRoles.RemoveRange(userSelectedRoles);
    }

    public async Task AddUserSelectedRole(UserRole userRole , CancellationToken cancellationToken)
    {
        await _context.UserRoles.AddAsync(userRole);
    }

    #endregion
}
