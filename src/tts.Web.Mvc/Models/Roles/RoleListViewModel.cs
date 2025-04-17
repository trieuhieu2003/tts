using System.Collections.Generic;
using tts.Roles.Dto;

namespace tts.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}
