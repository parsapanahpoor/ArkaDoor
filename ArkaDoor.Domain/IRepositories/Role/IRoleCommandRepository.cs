using Microsoft.EntityFrameworkCore;

namespace ArkaDoor.Domain.IRepositories.Role;

public interface IRoleCommandRepository
{
    #region Admin Panel 

    void Update(Domain.Entities.Account.Role role);

    Task AddAsync(Domain.Entities.Account.Role role , CancellationToken cancellationToken);

    #endregion
}
