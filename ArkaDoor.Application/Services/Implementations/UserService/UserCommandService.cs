#region Using

using ArkaDoor.Domain.IRepositories.Users;

namespace ArkaDoor.Application.Services.Implementations.UserService;

#endregion

public class UserCommandService : IUsersCommandRepository
{
	#region Ctor

	private readonly IUsersCommandRepository _usersCommandRepository;

    public UserCommandService(IUsersCommandRepository usersCommandRepository)
    {
            _usersCommandRepository = usersCommandRepository;
    }

    #endregion
}
