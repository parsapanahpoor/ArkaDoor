namespace ArkaDoor.Domain.IRepositories.Users; 

public interface IUserQueryRepository
{
    #region General Methods

    Task<bool> IsExistUserByMobileAsync(string mobile, CancellationToken cancellationToken);

    #endregion
}
