namespace ArkaDoor.Domain.Common;

public abstract class BaseEntities<TKey> where TKey : struct, IComparable<TKey>
{
    #region properties

    public TKey Id { get; set; }

    public DateTime CreateDate { get; set; } = DateTime.Now;

    public DateTime UpdateDate { get; set; }

    public bool IsDelete { get; set; }

    #endregion

    public void Update() => UpdateDate = DateTime.UtcNow;
}

