using System.Collections.Generic;
using tts.Roles.Dto;

namespace tts.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
