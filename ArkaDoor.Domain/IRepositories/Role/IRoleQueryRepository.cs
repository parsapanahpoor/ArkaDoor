using ArkaDoor.Domain.DTOs.Admin;

namespace ArkaDoor.Domain.IRepositories.Role;

public interface IRoleQueryRepository
{
    #region General Methods 

    Task<bool> IsUserIsSuperAdmin(ulong userId, CancellationToken cancellationToken);

    Task<List<string?>> GetListOfUserUniqueRolesName(ulong userId, CancellationToken cancellationToken);

    Task<Domain.Entities.Account.Role?> GetRoleById(ulong roleId, CancellationToken cancellation);

    #endregion

    #region Admin Panel 

    Task<FilterRolesDTO> FilterRoles(FilterRolesDTO filter, CancellationToken cancellation);

    #endregion
}
