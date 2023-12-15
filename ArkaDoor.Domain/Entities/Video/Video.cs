using ArkaDoor.Domain.Common;

namespace ArkaDoor.Domain.Entities.Video;

public sealed class Video : BaseEntities<ulong>
{
    #region properties

    public string Title { get; set; }

    public string ShortDescription { get; set; }

    public string longDescription { get; set; }

    public string File { get; set; }

    public string? Picture { get; set; }

    public bool ShowInLanding { get; set; }

    #endregion
}
