using ArkaDoor.Domain.Entities.Users;

namespace ArkaDoor.Domain.IRepositories.Users; 

public interface IUserQueryRepository
{
    #region General Methods

    Task<bool> IsExistUserByMobileAsync(string mobile, CancellationToken cancellationToken);

    Task<User?> GetUserByMobileAsync(string mobile, CancellationToken cancellation);

    #endregion
}
