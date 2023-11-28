using ArkaDoor.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace ArkaDoor.Domain.Entities.Users;

public class User : BaseEntities<ulong>
{
    #region properties

    [MaxLength(50)]
    public string Username { get; set; }

    [MaxLength(50)]
    public string? FirstName { get; set; }

    [MaxLength(50)]
    public string? LastName { get; set; }

    [MaxLength(30)]
    public string? NationalId { get; set; }

    [MaxLength(30)]
    public string Mobile { get; set; }

    [MaxLength(50)]
    public string Password { get; set; }

    public string? Avatar { get; set; }

    [MaxLength(100)]
    public string MobileActivationCode { get; set; }

    public bool IsMobileConfirm { get; set; } = false;

    public bool IsAdmin { get; set; } = false;

    public int WalletBalance { get; set; }

    public DateTime? ExpireMobileSMSDateTime { get; set; }

    #endregion
}
