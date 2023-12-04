using ArkaDoor.Domain.DTOs.SiteSide.Account;

namespace ArkaDoor.Application.Services.Interfaces;

public interface IUserService
{
    #region General

    Task<RegisterUserResponse> RegisterUserAsync(UserRegisterDTO userRegisterDTO, CancellationToken cancellationToken);

    #endregion
}
