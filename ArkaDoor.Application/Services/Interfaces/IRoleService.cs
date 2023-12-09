namespace ArkaDoor.Application.Services.Interfaces;

public interface IRoleService
{
    #region Admin Side 

    Task<bool> IsUserAdmin(ulong userId, CancellationToken cancellationToken);

    #endregion
}
