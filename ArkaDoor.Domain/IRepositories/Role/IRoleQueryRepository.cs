using ArkaDoor.Domain.DTOs.Admin;
using ArkaDoor.Domain.DTOs.Common;

namespace ArkaDoor.Domain.IRepositories.Role;

public interface IRoleQueryRepository
{
    #region General Methods 

    Task<bool> IsUserIsSuperAdmin(ulong userId, CancellationToken cancellationToken);

    Task<List<string?>> GetListOfUserUniqueRolesName(ulong userId, CancellationToken cancellationToken);

    Task<Domain.Entities.Account.Role?> GetRoleById(ulong roleId, CancellationToken cancellation);

    Task<bool> IsRoleNameValid(string name, ulong roleId , CancellationToken cancellationToken);

    Task<List<SelectListViewModel>> GetSelectRolesList(CancellationToken cancellation);

    #endregion

    #region Admin Panel 

    Task<FilterRolesDTO> FilterRoles(FilterRolesDTO filter, CancellationToken cancellation);

    Task<List<ulong>> GetUserSelectedRoleIdByUserId(ulong userId, CancellationToken cancellation);

    #endregion
}
