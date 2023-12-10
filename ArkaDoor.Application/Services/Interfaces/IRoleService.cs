using ArkaDoor.Domain.DTOs.Admin;
namespace ArkaDoor.Application.Services.Interfaces;

public interface IRoleService
{
    #region General Methods

    Task<bool> IsRoleNameValid(string name, ulong roleId, CancellationToken cancellationToken);

    #endregion

    #region Admin Side 

    Task<bool> IsUserAdmin(ulong userId, CancellationToken cancellationToken);

    Task<FilterRolesDTO> FilterRoles(FilterRolesDTO filter, CancellationToken cancellation);

    Task<Domain.Entities.Account.Role?> GetRoleById(ulong roleId, CancellationToken cancellation);

    Task<bool> CreateRole(CreateRoleDTO create, CancellationToken cancellation);

    #endregion
}
