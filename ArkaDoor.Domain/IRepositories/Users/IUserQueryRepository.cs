using ArkaDoor.Domain.DTOs.Admin.User;
using ArkaDoor.Domain.Entities.Users;

namespace ArkaDoor.Domain.IRepositories.Users; 

public interface IUserQueryRepository
{
    #region General Methods

    Task<bool> IsExistUserByMobileAsync(string mobile, CancellationToken cancellationToken);

    Task<User?> GetUserByMobileAsync(string mobile, CancellationToken cancellation);

    #endregion

    #region Admin Side

    Task<FilterUserDTO> FilterUsers(FilterUserDTO filter);

    Task<User> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);

    #endregion
}
