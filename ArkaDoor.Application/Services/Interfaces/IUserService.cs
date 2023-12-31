﻿using ArkaDoor.Domain.DTOs.Admin.User;
using ArkaDoor.Domain.DTOs.SiteSide.Account;
using ArkaDoor.Domain.Entities.Users;
using ArkaDoor.Domain.IRepositories.Users;
using Microsoft.AspNetCore.Http;

namespace ArkaDoor.Application.Services.Interfaces;

public interface IUserService
{
    #region General

    Task<User> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);

    Task<RegisterUserResponse> RegisterUserAsync(UserRegisterDTO userRegisterDTO, CancellationToken cancellationToken);

    Task<bool> IsExistUserByMobileAsync(string mobile, CancellationToken cancellationToken);

    Task<User?> GetUserByMobileAsync(string mobile, CancellationToken cancellation);

    Task<SendActivationCodeDTO> SendActivationCodeForUser(string Mobile, CancellationToken cancellationToken, bool Resend = false);

    Task<ActiveMobileByActivationCodeResult> ActiveUserMobile(ActiveMobileByActivationCodeDTO activeMobileByActivationCodeDTO,
                                                                        CancellationToken cancellationToken);

    Task<LoginUserDTOResponse> LoginUserAsync(LoginUserDTO model, CancellationToken cancellationToken);

    #endregion

    #region Admin Side 

    Task<EditUserDTO> GetUserForEdit(ulong userId, CancellationToken cancellation);

    Task<FilterUserDTO> FilterUsers(FilterUserDTO filter);

    Task<EditUserResult> EditUser(EditUserDTO user, IFormFile avatar, CancellationToken cancellation);

    Task<bool> RemoveUserById(ulong userId, CancellationToken cancellation);

    #endregion
}
