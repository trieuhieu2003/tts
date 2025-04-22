using System.Collections.Generic;
using tts.Roles.Dto;

namespace tts.Web.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }
    }
}