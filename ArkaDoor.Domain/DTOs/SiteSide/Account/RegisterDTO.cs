using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ArkaDoor.Domain.DTOs.SiteSide.Account;

public record UserRegisterDTO
{
    #region Properties

    [MaxLength(200, ErrorMessage = "Please Enter {0} Less Than {1} Character")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [DisplayName("کلمه ی عبور")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [MaxLength(200, ErrorMessage = "Please Enter {0} Less Than {1} Character")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [DisplayName("تکرار کلمه ی عبور")]
    [Compare("Password", ErrorMessage = "Password And Re Password Does Not Match")]
    [DataType(DataType.Password)]
    public string RePassword { get; set; }

    [MaxLength(200, ErrorMessage = "Please Enter {0} Less Than {1} Character")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [DisplayName("موبایل")]
    public string Mobile { get; set; }

    #endregion

}

public enum RegisterUserResponse
{
    MobileExist, 
    Success
}

