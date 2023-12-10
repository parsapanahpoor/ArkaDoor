#region Using

using ArkaDoor.Domain.DTOs.Admin.User;
using ArkaDoor.Domain.Entities.Users;
using ArkaDoor.Domain.IRepositories.Users;
using ArkaDoor.Infrastructure.Persistence.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

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

    public async Task<User?> GetUserByMobileAsync(string mobile, CancellationToken cancellation)
    {
        return await _context.Users
                             .FirstOrDefaultAsync(p => !p.IsDelete && p.Mobile == mobile); 
    }

    #endregion

    #region Admin Side 

    public async Task<FilterUserDTO> FilterUsers(FilterUserDTO filter)
    {
        var query = _context.Users
                            .AsNoTracking()
                            .OrderByDescending(p => p.CreateDate)
                            .AsQueryable();

        #region order

        switch (filter.OrderType)
        {
            case FilterUserOrderType.CreateDate_DES:
                query = query.OrderByDescending(u => u.CreateDate);
                break;

            case FilterUserOrderType.CreateDate_ASC:
                query = query.OrderBy(u => u.CreateDate);
                break;
        }

        #endregion

        #region filter

        if ((!string.IsNullOrEmpty(filter.Mobile)))
        {
            query = query.Where(u => u.Mobile.Contains(filter.Mobile));
        }

        if ((!string.IsNullOrEmpty(filter.Username)))
        {
            query = query.Where(u => u.Username.Contains(filter.Username));
        }

        #endregion

        #region paging

        await filter.Paging(query);

        #endregion

        return filter;
    }

    public async Task<bool> IsMobileExist(string mobile , CancellationToken cancellation)
    {
        return await _context.Users
                             .AnyAsync(u => u.Mobile == mobile);
    }

    #endregion
}
