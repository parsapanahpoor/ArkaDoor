using ArkaDoor.Domain.IRepositories.Role;
using ArkaDoor.Infrastructure.Persistence.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace ArkaDoor.Infrastructure.Persistence.Repositories.Role;

public class RoleQueryRepository : IRoleQueryRepository
{
    #region Ctor

    private readonly AkaDoorDbContext _context;

    public RoleQueryRepository(AkaDoorDbContext context)
    {
        _context = context;
    }

    #endregion

    #region General Methods 

    public async Task<bool> IsUserIsSuperAdmin(ulong userId , CancellationToken cancellationToken)
    { 
        return await _context.Users
                             .AsNoTracking()
                             .Where(p=> !p.IsDelete &&
                                    p.Id == userId)
                             .Select(p=> p.IsAdmin)
                             .FirstOrDefaultAsync();
    }

    public async Task<List<string?>> GetListOfUserUniqueRolesName(ulong userId , CancellationToken cancellationToken)
    {
        //Get User Selected Role Ids
        var roleIds = await _context.UserRoles  
                                    .AsNoTracking()
                                    .Where(p => !p.IsDelete && p.Id == userId)
                                    .Select(p=> p.RoleId)
                                    .ToListAsync();

        List<string?> roleNames = new List<string?>();

        foreach (var roleId in roleIds)
        {
            roleNames.Add(await _context.Roles
                                        .AsNoTracking()
                                        .Where(p=> !p.IsDelete &&
                                               p.Id == roleId)
                                        .Select(p=> p.RoleUniqueName)
                                        .FirstOrDefaultAsync());
        }

        return roleNames;
    }

    #endregion
}
