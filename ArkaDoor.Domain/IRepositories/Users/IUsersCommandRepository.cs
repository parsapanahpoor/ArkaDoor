using ArkaDoor.Domain.Entities.Users;

namespace ArkaDoor.Domain.IRepositories.Users;

public interface IUsersCommandRepository
{
    #region General

    Task AddAsync(User entity, CancellationToken cancellationToken);

    #endregion
}
