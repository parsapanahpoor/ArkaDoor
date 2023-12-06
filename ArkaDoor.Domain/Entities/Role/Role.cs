﻿using ArkaDoor.Domain.Common;
namespace Window.Domain.Entities.Account;

public class Role : BaseEntities<ulong>
{
    #region Properties

    public string  Title { get; set; }

    public string  RoleUniqueName { get; set; }

    #endregion
}
