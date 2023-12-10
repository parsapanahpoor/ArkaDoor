using ArkaDoor.Domain.Entities.Users;

namespace ArkaDoor.Domain.IRepositories.Users;

public interface IUsersCommandRepository
{
    #region General

    Task AddAsync(User entity, CancellationToken cancellationToken);

    void Update(User entity);

    #endregion
}
