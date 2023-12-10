using ArkaDoor.Application.Common.IUnitOfWork;
using ArkaDoor.Application.Extensions;
using ArkaDoor.Application.Generators;
using ArkaDoor.Application.Security;
using ArkaDoor.Application.Services.Interfaces;
using ArkaDoor.Application.StaticTools;
using ArkaDoor.Application.Utilities.Security;
using ArkaDoor.Domain.DTOs.Admin.User;
using ArkaDoor.Domain.DTOs.SiteSide.Account;
using ArkaDoor.Domain.Entities.Account;
using ArkaDoor.Domain.Entities.Users;
using ArkaDoor.Domain.IRepositories.Role;
using ArkaDoor.Domain.IRepositories.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ArkaDoor.Application.Services.Implementations;

public class UserService : IUserService
{
    #region Ctor

    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IUsersCommandRepository _usersCommandRepository;
    private readonly IUnitOfWork _unitOfWork;
    private static readonly HttpClient client = new HttpClient();
    private readonly IRoleQueryRepository _roleQueryRepository;
    private readonly IRoleCommandRepository _roleCommandRepository;

    public UserService(IUserQueryRepository userQueryRepository,
                       IUsersCommandRepository usersCommandRepository,
                       IUnitOfWork unitOfWork,
                       IRoleQueryRepository roleQueryRepository , 
                       IRoleCommandRepository roleCommandRepository)
    {
        _userQueryRepository = userQueryRepository;
        _usersCommandRepository = usersCommandRepository;
        _unitOfWork = unitOfWork;
        _roleQueryRepository = roleQueryRepository;
        _roleCommandRepository = roleCommandRepository;
    }

    #endregion

    #region General 

    public async Task<User> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
    {
        return await _userQueryRepository.GetByIdAsync(cancellationToken, ids);
    }

    #endregion

    #region Site Side 

    public async Task<RegisterUserResponse> RegisterUserAsync(UserRegisterDTO userRegisterDTO,
                                                             CancellationToken cancellationToken)
    {
        #region Check That is exist any user by mobile

        if (await _userQueryRepository.IsExistUserByMobileAsync(userRegisterDTO.Mobile.Trim().ToLower(),
                                                                   cancellationToken))
            return RegisterUserResponse.MobileExist;

        #endregion

        #region Fill Model

        var user = new User
        {
            Mobile = userRegisterDTO.Mobile.Trim().ToLower(),
            Username = userRegisterDTO.Mobile,
            Password = PasswordHelper.EncodePasswordMd5(userRegisterDTO.Password),
            CreateDate = DateTime.Now,
            IsAdmin = false,
            MobileActivationCode = new Random().Next(10000, 999999).ToString(),
            ExpireMobileSMSDateTime = DateTime.Now,
            IsMobileConfirm = true
        };

        #endregion

        #region Add User To The Data Base

        await _usersCommandRepository.AddAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        #endregion

        #region Send Verification Code SMS

        //var result = $"https://api.kavenegar.com/v1/6A427559367558527A76485753667A5779587337736735753945747946474F347A346A65356E7A567A51413D/verify/lookup.json?receptor={user.Mobile}&token={user.MobileActivationCode}&template=Register";
        //var results = client.GetStringAsync(result);

        #endregion

        return RegisterUserResponse.Success;
    }

    public async Task<bool> IsExistUserByMobileAsync(string mobile, CancellationToken cancellationToken)
    {
        return await _userQueryRepository.IsExistUserByMobileAsync(mobile, cancellationToken);
    }

    public async Task<User?> GetUserByMobileAsync(string mobile, CancellationToken cancellation)
    {
        return await _userQueryRepository.GetUserByMobileAsync(mobile, cancellation);
    }

    public async Task<SendActivationCodeDTO> SendActivationCodeForUser(string Mobile, CancellationToken cancellationToken, bool Resend = false)
    {
        SendActivationCodeDTO returnModel = new();

        #region Is Exist User 

        if (!await IsExistUserByMobileAsync(Mobile, cancellationToken))
        {
            returnModel.SendActivationCodeResult = SendActivationCodeResult.UserNotFound;

            return returnModel;
        }

        #endregion

        #region Get User By User ID

        var user = await _userQueryRepository.GetUserByMobileAsync(Mobile, cancellationToken);
        if (user == null)
        {
            returnModel.SendActivationCodeResult = SendActivationCodeResult.UserNotFound;

            return returnModel;
        }

        #endregion

        #region Resend SMS

        if (Resend)
        {
            user.MobileActivationCode = new Random().Next(10000, 999999).ToString();
            user.ExpireMobileSMSDateTime = DateTime.Now;

            _usersCommandRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();

            #region Send Verification Code SMS

            //var result = $"https://api.kavenegar.com/v1/6A427559367558527A76485753667A5779587337736735753945747946474F347A346A65356E7A567A51413D/verify/lookup.json?receptor={user.Mobile}&token={user.MobileActivationCode}&template=Register";
            //var results = client.GetStringAsync(result);

            #endregion
        }

        #endregion

        #region Time Counter Initilize

        var SiteSettingSMSTimer = 2;

        DateTime expireMinut = user.ExpireMobileSMSDateTime.Value.AddMinutes(SiteSettingSMSTimer);

        var TimerMinut = expireMinut - DateTime.Now;

        returnModel.Time = TimerMinut.TotalMinutes * 60;
        returnModel.SendActivationCodeResult = SendActivationCodeResult.Success;

        #endregion

        return returnModel;
    }

    public async Task<ActiveMobileByActivationCodeResult> ActiveUserMobile(ActiveMobileByActivationCodeDTO activeMobileByActivationCodeDTO,
                                                                           CancellationToken cancellationToken)
    {
        #region Get User By Mobile

        var user = await GetUserByMobileAsync(activeMobileByActivationCodeDTO.Mobile.SanitizeText(), cancellationToken);
        if (user == null) return ActiveMobileByActivationCodeResult.AccountNotFound;

        #endregion

        #region Validation Activation Code

        if (user.MobileActivationCode != activeMobileByActivationCodeDTO.MobileActiveCode)
        {
            return ActiveMobileByActivationCodeResult.AccountNotFound;
        }

        #endregion

        #region Update User 

        user.IsMobileConfirm = true;
        user.MobileActivationCode = new Random().Next(10000, 999999).ToString();

        await _unitOfWork.SaveChangesAsync();

        #endregion

        return ActiveMobileByActivationCodeResult.Success;
    }

    public async Task<LoginUserDTOResponse> LoginUserAsync(LoginUserDTO model, CancellationToken cancellationToken)
    {
        LoginUserDTOResponse returnModel = new();

        //Get User By Mobile 
        var user = await _userQueryRepository.GetUserByMobileAsync(model.Mobile.SanitizeText(), cancellationToken);

        if (user == null)
        {
            returnModel.LoginUserResponse = LoginUserResponse.UserNotFound;

            return returnModel;
        }

        if (!user.IsMobileConfirm)
        {
            returnModel.LoginUserResponse = LoginUserResponse.UserNotActive;

            return returnModel;
        }

        if (user.Password != PasswordHelper.EncodePasswordMd5(model.Password))
        {
            returnModel.LoginUserResponse = LoginUserResponse.WrongPassword;

            return returnModel;
        }

        returnModel.LoginUserResponse = LoginUserResponse.Success;
        returnModel.User = user;

        return returnModel;
    }

    #endregion

    #region Admin Side 

    public async Task<EditUserDTO> GetUserForEdit(ulong userId, CancellationToken cancellation)
    {
        //Get User By Id 
        var user = await GetByIdAsync(cancellation, userId);

        //Get User Selected Role By User Id
        var userRoleIds = await _roleQueryRepository.GetUserSelectedRoleIdByUserId(userId, cancellation);

        return new EditUserDTO()
        {
            Username = user.Username,
            Mobile = user.Mobile,
            Avatar = user.Avatar,
            UserRoles = userRoleIds
        };
    }

    public async Task<FilterUserDTO> FilterUsers(FilterUserDTO filter)
    {
        return await _userQueryRepository.FilterUsers(filter);
    }

    public async Task<EditUserResult> EditUser(EditUserDTO user, IFormFile avatar, CancellationToken cancellation)
    {
        //Get User By Id 
        var userOldInfos = await _userQueryRepository.GetByIdAsync(cancellation, user.Id);
        if (userOldInfos == null) return EditUserResult.Error;

        //Checkind incomin mobile 
        if (await _userQueryRepository.IsMobileExist(user.Mobile, cancellation) && user.Mobile != userOldInfos.Mobile)
        {
            return EditUserResult.DuplicateMobileNumber;
        }

        if (userOldInfos != null)
        {
            userOldInfos.Username = user.Username;
            userOldInfos.Mobile = user.Mobile.SanitizeText();

            if (user.Password != null)
            {
                userOldInfos.Password = user.Password.SanitizeText();
            }

            #region User Avatar

            if (avatar != null && avatar.IsImage())
            {
                if (!string.IsNullOrEmpty(userOldInfos.Avatar))
                {
                    userOldInfos.Avatar.DeleteImage(FilePaths.UserAvatarPathServer, FilePaths.UserAvatarPathThumbServer);
                }

                var imageName = CodeGenerator.GenerateUniqCode() + Path.GetExtension(avatar.FileName);
                avatar.AddImageToServer(imageName, FilePaths.UserAvatarPathServer, 270, 270, FilePaths.UserAvatarPathThumbServer);
                userOldInfos.Avatar = imageName;
            }

            #endregion

            _usersCommandRepository.Update(userOldInfos);

            #region Delete User Roles

            await _roleCommandRepository.RemoveUserRolesByUserId(user.Id , cancellation);

            #endregion

            #region Add User Roles

            if (user.UserRoles != null && user.UserRoles.Any())
            {
                foreach (var roleId in user.UserRoles)
                {
                    var userRole = new UserRole()
                    {
                        RoleId = roleId,
                        UserId = user.Id
                    };

                    await _roleCommandRepository.AddUserSelectedRole(userRole , cancellation);
                }
            }

            #endregion

            await _unitOfWork.SaveChangesAsync(cancellation);

            return EditUserResult.Success;
        }

        return EditUserResult.Error;
    }

    public async Task<bool> RemoveUserById(ulong userId , CancellationToken cancellation)
    {
        var removedUser = await GetByIdAsync(cancellation , userId);
        if (removedUser == null) return false;

        //Soft Delete
        removedUser.IsDelete = true;

        _usersCommandRepository.Update(removedUser);

        await _unitOfWork.SaveChangesAsync(cancellation);

        return true;
    }

    #endregion
}
