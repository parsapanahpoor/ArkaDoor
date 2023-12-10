using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using ArkaDoor.Domain.Entities.Users;

namespace ArkaDoor.Domain.DTOs.SiteSide.Account;

public record LoginUserDTO
{
    #region Properties

    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [DisplayName("موبایل")]
    public string Mobile { get; set; }

    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [DataType(DataType.Password)]
    [DisplayName("Password")]
    public string Password { get; set; }

    public bool RememberMe { get; set; }

    #endregion
}

public record LoginUserDTOResponse
{
    public LoginUserResponse LoginUserResponse { get; set; }

    public User User { get; set; }
}

public enum LoginUserResponse
{
    Success, 
    UserNotActive, 
    WrongPassword, 
    UserNotFound
}