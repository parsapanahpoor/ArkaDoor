using System.Security.Claims;
using System.Security.Principal;
using ArkaDoor.Domain.Entities.Users;

namespace ArkaDoor.Application.Utilities.Extensions
{
    public static class UserExtensions
    {
        public static ulong GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var data = claimsPrincipal.Claims.SingleOrDefault(s => s.Type == ClaimTypes.NameIdentifier);

            return ulong.Parse(data.Value);
        }

        public static ulong GetUserId(this IPrincipal principal)
        {
            var user = (ClaimsPrincipal)principal;

            return user.GetUserId();
        }

        //public static string GetUserFullName(this User user)
        //{
        //    return $"{user.Name} {user.Family}";
        //}

        //public static string GetUserAvatar(this User user)
        //{
        //    if (!string.IsNullOrEmpty(user.Avatar))
        //    {
        //        return Path.Combine(PathTools.UserAvatarPathThumb, user.Avatar);
        //    }

        //    return PathTools.DefaultUserAvatar;
        //}
    }
}
