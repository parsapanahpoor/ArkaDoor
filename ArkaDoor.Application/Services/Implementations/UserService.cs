using ArkaDoor.Application.Common.IUnitOfWork;
using ArkaDoor.Application.Security;
using ArkaDoor.Application.Services.Interfaces;
using ArkaDoor.Domain.DTOs.SiteSide.Account;
using ArkaDoor.Domain.Entities.Users;
using ArkaDoor.Domain.IRepositories.Users;

namespace ArkaDoor.Application.Services.Implementations;

public class UserService : IUserService
{
    #region Ctor

    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IUsersCommandRepository _usersCommandRepository;
    private readonly IUnitOfWork _unitOfWork;
    private static readonly HttpClient client = new HttpClient();

    public UserService(IUserQueryRepository userQueryRepository,
                       IUsersCommandRepository usersCommandRepository , 
                       IUnitOfWork unitOfWork)
    {
        _userQueryRepository = userQueryRepository;
        _usersCommandRepository = usersCommandRepository;
        _unitOfWork = unitOfWork;
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
            ExpireMobileSMSDateTime = DateTime.Now
        };


        #endregion

        #region Add User To The Data Base

        await _usersCommandRepository.AddAsync(user , cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        #endregion

        #region Send Verification Code SMS

        var result = $"https://api.kavenegar.com/v1/6A427559367558527A76485753667A5779587337736735753945747946474F347A346A65356E7A567A51413D/verify/lookup.json?receptor={user.Mobile}&token={user.MobileActivationCode}&template=Register";
        var results = client.GetStringAsync(result);

        #endregion

        return RegisterUserResponse.Success;
    }

    #endregion
}
