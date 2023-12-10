using ArkaDoor.Application.Common.IUnitOfWork;
using ArkaDoor.Application.Services.Interfaces;
using ArkaDoor.Domain.DTOs.Admin;
using ArkaDoor.Domain.Entities.Account;
using ArkaDoor.Domain.IRepositories.Role;
using Microsoft.EntityFrameworkCore;

namespace ArkaDoor.Application.Services.Implementations;

public class RoleService : IRoleService
{
    #region Ctor

    private readonly IRoleCommandRepository _commandRepository;
    private IRoleQueryRepository _queryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RoleService(IRoleCommandRepository roleCommandRepository,
                       IRoleQueryRepository roleQueryRepository , 
                       IUnitOfWork unitOfWork)
    {
        _commandRepository = roleCommandRepository;
        _queryRepository = roleQueryRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    #region General Methods

    public async Task<bool> IsRoleNameValid(string name, ulong roleId, CancellationToken cancellationToken)
    {
        return await _queryRepository.IsRoleNameValid(name, roleId, cancellationToken);
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

    public async Task<FilterRolesDTO> FilterRoles(FilterRolesDTO filter , CancellationToken cancellation)
    {
        return await _queryRepository.FilterRoles(filter , cancellation);
    }

    public async Task<Domain.Entities.Account.Role?> GetRoleById(ulong roleId, CancellationToken cancellation)
    {
        return await _queryRepository.GetRoleById(roleId , cancellation);
    }

    public async Task<bool> CreateRole(CreateRoleDTO create , CancellationToken cancellation)
    {
        if (!await IsRoleNameValid(create.RoleUniqueName, 0 , cancellation)) return false;

        // add role
        var role = new Role
        {
            RoleUniqueName = create.RoleUniqueName,
            Title = create.Title
        };

        await _commandRepository.AddRole(role , cancellation);
        await _unitOfWork.SaveChangesAsync(cancellation);

        return true;
    }

    #endregion
}
