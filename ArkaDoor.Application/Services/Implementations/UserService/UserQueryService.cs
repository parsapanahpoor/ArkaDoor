#region Usings

using ArkaDoor.Application.Services.Interfaces.IUserService;
using ArkaDoor.Domain.IRepositories.Users;

namespace ArkaDoor.Application.Services.Implementations.UserService;

#endregion

public class UserQueryService : IUserQueryService
{
	#region Ctor

	private readonly IUserQueryRepository _userQueryRepository;

	public UserQueryService(IUserQueryRepository userQueryRepository)
	{
		_userQueryRepository = userQueryRepository;
	}

	#endregion
}
