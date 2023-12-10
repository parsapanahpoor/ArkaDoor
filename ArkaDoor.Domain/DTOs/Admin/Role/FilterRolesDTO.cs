using ArkaDoor.Domain.DTOs.Common;
using ArkaDoor.Domain.Entities.Account;

namespace ArkaDoor.Domain.DTOs.Admin;

public class FilterRolesDTO : BasePaging<Role>
{
    #region properties

    public string? RoleTitle { get; set; }

    public string? RoleUniqueName { get; set; }

    #endregion
}
