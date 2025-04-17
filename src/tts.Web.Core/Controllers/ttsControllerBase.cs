using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace tts.Controllers
{
    public abstract class ttsControllerBase: AbpController
    {
        protected ttsControllerBase()
        {
            LocalizationSourceName = ttsConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
