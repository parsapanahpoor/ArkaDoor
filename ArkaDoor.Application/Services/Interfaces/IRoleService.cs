using ArkaDoor.Domain.DTOs.Admin;
namespace ArkaDoor.Application.Services.Interfaces;

public interface IRoleService
{
    #region Admin Side 

    Task<bool> IsUserAdmin(ulong userId, CancellationToken cancellationToken);

    Task<FilterRolesDTO> FilterRoles(FilterRolesDTO filter, CancellationToken cancellation);

    Task<Domain.Entities.Account.Role?> GetRoleById(ulong roleId, CancellationToken cancellation);

    #endregion
}
