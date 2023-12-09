namespace ArkaDoor.Domain.IRepositories.Role;

public interface IRoleQueryRepository
{
    #region General Methods 

    Task<bool> IsUserIsSuperAdmin(ulong userId, CancellationToken cancellationToken);

    Task<List<string?>> GetListOfUserUniqueRolesName(ulong userId, CancellationToken cancellationToken);

    #endregion
}
