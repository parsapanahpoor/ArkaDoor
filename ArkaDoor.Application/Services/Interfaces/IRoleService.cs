using ArkaDoor.Domain.DTOs.Admin;
namespace ArkaDoor.Application.Services.Interfaces;

public interface IRoleService
{
    #region General Methods

    Task<bool> IsRoleNameValid(string name, ulong roleId, CancellationToken cancellationToken);

    Task<Domain.Entities.Account.Role?> GetRoleById(ulong roleId, CancellationToken cancellation);

    #endregion

    #region Admin Side 

    Task<EditRoleDTO?> FillEditRoleViewModel(ulong roleId, CancellationToken cancellationToken);

    Task<bool> IsUserAdmin(ulong userId, CancellationToken cancellationToken);

    Task<FilterRolesDTO> FilterRoles(FilterRolesDTO filter, CancellationToken cancellation);

    Task<bool> CreateRole(CreateRoleDTO create, CancellationToken cancellation);

    Task<EditRoleResult> EditRole(EditRoleDTO edit, CancellationToken cancellationToken);

    Task<bool> DeleteRole(ulong roleId, CancellationToken cancellation);

    #endregion
}
