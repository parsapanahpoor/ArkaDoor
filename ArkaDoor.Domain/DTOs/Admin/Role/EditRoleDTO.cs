using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ArkaDoor.Domain.DTOs.Admin;

public record EditRoleDTO
{
    #region properties

    [DisplayName("Title")]
    [Required(ErrorMessage = "Please Enter {0}")]
    public string Title { get; set; }

    [DisplayName("Unique Name")]
    [Required(ErrorMessage = "Please Enter {0}")]
    public string RoleUniqueName { get; set; }

    public ulong Id { get; set; }

    #endregion
}

public enum EditRoleResult
{
    Success,
    RoleNotFound,
    UniqueNameExists
}
