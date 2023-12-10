using Microsoft.EntityFrameworkCore;

namespace ArkaDoor.Domain.IRepositories.Role;

public interface IRoleCommandRepository
{
    #region Admin Panel 

    Task AddRole(Domain.Entities.Account.Role role, CancellationToken cancellationToken);

    #endregion
}
