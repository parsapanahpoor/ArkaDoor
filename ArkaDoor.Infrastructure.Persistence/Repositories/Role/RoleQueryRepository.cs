﻿using ArkaDoor.Domain.DTOs.Admin;
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

    public async Task<Domain.Entities.Account.Role?> GetRoleById(ulong roleId, CancellationToken cancellationToken )
    {
        return await _context.Roles
                             .AsNoTracking() 
                             .FirstOrDefaultAsync(s => s.Id == roleId && !s.IsDelete);
    }

    #endregion

    #region Admin Panel 

    public async Task<FilterRolesDTO> FilterRoles(FilterRolesDTO filter, CancellationToken cancellation)
    {
        var query = _context.Roles.Where(s => !s.IsDelete).AsQueryable();

        #region Filter

        if (!string.IsNullOrEmpty(filter.RoleTitle))
        {
            query = query.Where(s => s.Title.Contains(filter.RoleTitle));
        }

        if (!string.IsNullOrEmpty(filter.RoleUniqueName))
        {
            query = query.Where(s => s.RoleUniqueName.Contains(filter.RoleUniqueName));
        }

        #endregion

        await filter.Paging(query);

        return filter;
    }

    #endregion
}