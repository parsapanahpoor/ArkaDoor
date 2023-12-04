using ArkaDoor.Domain.DTOs.SiteSide.Account;
using ArkaDoor.Domain.Entities.Users;

namespace ArkaDoor.Application.Services.Interfaces;

public interface IUserService
{
    #region General

    Task<RegisterUserResponse> RegisterUserAsync(UserRegisterDTO userRegisterDTO, CancellationToken cancellationToken);

    Task<bool> IsExistUserByMobileAsync(string mobile, CancellationToken cancellationToken);

    Task<User?> GetUserByMobileAsync(string mobile, CancellationToken cancellation);

    Task<SendActivationCodeDTO> SendActivationCodeForUser(string Mobile, CancellationToken cancellationToken, bool Resend = false);

    Task<ActiveMobileByActivationCodeResult> ActiveUserMobile(ActiveMobileByActivationCodeDTO activeMobileByActivationCodeDTO,
                                                                        CancellationToken cancellationToken);

    Task<LoginUserDTOResponse> LoginUserAsync(LoginUserDTO model, CancellationToken cancellationToken);

    #endregion
}
