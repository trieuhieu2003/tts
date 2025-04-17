using Abp.Authorization;
using tts.Authorization.Roles;
using tts.Authorization.Users;

namespace tts.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
