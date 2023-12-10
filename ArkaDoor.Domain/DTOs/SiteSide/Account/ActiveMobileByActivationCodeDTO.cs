using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ArkaDoor.Domain.DTOs.SiteSide.Account;

public record ActiveMobileByActivationCodeDTO
{
    #region Properties

    [Display(Name = "تلفن همراه")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
    [DataType(DataType.MultilineText)]
    [RegularExpression(@"^([0-9]{11})$", ErrorMessage = "موبایل وارد شده معتبر نمی باشد")]
    public string Mobile { get; set; }

    [Display(Name = "کد فعالسازی ارسال شده")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
    public string MobileActiveCode { get; set; }

    #endregion
}

public record SendActivationCodeDTO
{
    public SendActivationCodeResult SendActivationCodeResult { get; set; }

    public double Time { get; set; }
}

public enum SendActivationCodeResult
{
    Success,
    UserNotFound,
    Faild
}

public enum ActiveMobileByActivationCodeResult
{
    Success,
    AccountNotFound
}