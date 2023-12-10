using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ArkaDoor.Domain.DTOs.Admin;

public record class CreateRoleDTO
{
    #region properties

    [DisplayName("عنوان")]
    [Required(ErrorMessage = "Please Enter {0}")]
    public string Title { get; set; }

    [DisplayName("نام یکتا")]
    [Required(ErrorMessage = "Please Enter {0}")]
    public string RoleUniqueName { get; set; }
    
    #endregion
}


