﻿using ArkaDoor.Domain.Common;
namespace ArkaDoor.Domain.Entities.Account;

public class UserRole : BaseEntities<ulong>
{
    #region properties

    public ulong UserId { get; set; }

    public ulong RoleId { get; set; }

    #endregion
}
