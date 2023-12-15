using ArkaDoor.Domain.Common;
using System.ComponentModel.DataAnnotations;
namespace ArkaDoor.Domain.Entities.Video;

public sealed class VideoTag : BaseEntities<ulong>
{
    #region properties

    public ulong VideoId { get; set; }

    [Display(Name = "تگ")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string TagTitle { get; set; }

    #endregion
}
