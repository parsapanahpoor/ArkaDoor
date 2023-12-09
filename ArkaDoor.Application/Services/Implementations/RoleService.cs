using ArkaDoor.Application.Services.Interfaces;
using ArkaDoor.Domain.IRepositories.Role;

namespace ArkaDoor.Application.Services.Implementations;

public class RoleService : IRoleService
{
    #region Ctor

    private readonly IRoleCommandRepository _commandRepository;
    private IRoleQueryRepository _queryRepository;

    public RoleService(IRoleCommandRepository roleCommandRepository,
                       IRoleQueryRepository roleQueryRepository)
    {
        _commandRepository = roleCommandRepository;
        _queryRepository = roleQueryRepository;
    }

    #endregion

    #region Admin Side 

    public async Task<bool> IsUserAdmin(ulong userId , CancellationToken cancellationToken)
    {
        //Check That is user super admin 
        var isUserSuperAdmin = await _queryRepository.IsUserIsSuperAdmin(userId , cancellationToken);
        if (isUserSuperAdmin) return true;

        //Get User Roles
        var userRolesName = await _queryRepository.GetListOfUserUniqueRolesName(userId , cancellationToken);
        if(userRolesName != null && userRolesName.Any() && userRolesName.Contains("Admin")) return true;

        return false;
    }

    #endregion
}
