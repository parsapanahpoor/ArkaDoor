using ArkaDoor.Domain.DTOs.Common;
using System.ComponentModel.DataAnnotations;

namespace ArkaDoor.Domain.DTOs.Admin.User;

public class FilterUserDTO : BasePaging<Entities.Users.User>
{
    #region properties

    public string Username { get; set; }

    public string Mobile { get; set; }

    public bool IsDelete { get; set; }

    public FilterUserOrderType OrderType { get; set; }

    #endregion
}

public enum FilterUserOrderType
{
    [Display(Name = "نزولی")]
    CreateDate_DES,
    [Display(Name = "صعودی")]
    CreateDate_ASC,
}
