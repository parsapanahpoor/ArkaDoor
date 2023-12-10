using ArkaDoor.Application.Common.IUnitOfWork;
using ArkaDoor.Application.Services.Interfaces;
using ArkaDoor.Domain.DTOs.Admin;
using ArkaDoor.Domain.DTOs.Common;
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

    public async Task<List<SelectListViewModel>> GetSelectRolesList(CancellationToken cancellation)
    {
        return await _queryRepository.GetSelectRolesList(cancellation);
    }

    #endregion

    #region Admin Side 

    public async Task<EditRoleDTO?> FillEditRoleViewModel(ulong roleId , CancellationToken cancellationToken)
    {
        var role = await GetRoleById(roleId , cancellationToken);
        if (role == null) return null;

        var result = new EditRoleDTO
        {
            Id = roleId,
            RoleUniqueName = role.RoleUniqueName,
            Title = role.Title,
        };

        return result;
    }

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

        await _commandRepository.AddAsync(role , cancellation);
        await _unitOfWork.SaveChangesAsync(cancellation);

        return true;
    }

    public async Task<EditRoleResult> EditRole(EditRoleDTO edit , CancellationToken cancellationToken)
    {
        //Get Role By Id
        var role = await GetRoleById(edit.Id , cancellationToken);
        if (role == null) return EditRoleResult.RoleNotFound;
        if (!await IsRoleNameValid(edit.RoleUniqueName, edit.Id , cancellationToken))return EditRoleResult.UniqueNameExists;

        //Fill Model
        role.Title = edit.Title;
        role.RoleUniqueName = edit.RoleUniqueName;

        //Edit Role
        _commandRepository.Update(role);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return EditRoleResult.Success;
    }

    public async Task<bool> DeleteRole(ulong roleId , CancellationToken cancellation)
    {
        //Get Role By Id 
        var role = await GetRoleById(roleId , cancellation);
        if (role == null) return false;

        //Delete Recorde
        role.IsDelete = true;

        _commandRepository.Update(role);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    #endregion
}
