using ArkaDoor.Domain.Entities.Account;
using Microsoft.EntityFrameworkCore;

namespace ArkaDoor.Domain.IRepositories.Role;

public interface IRoleCommandRepository
{
    #region Admin Panel 

    void Update(Domain.Entities.Account.Role role);

    Task AddAsync(Domain.Entities.Account.Role role, CancellationToken cancellationToken);

    Task RemoveUserRolesByUserId(ulong userId, CancellationToken cancellationToken);

    Task AddUserSelectedRole(UserRole userRole, CancellationToken cancellationToken);

    #endregion
}
