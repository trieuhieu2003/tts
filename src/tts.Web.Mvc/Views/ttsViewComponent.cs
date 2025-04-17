using Abp.AspNetCore.Mvc.ViewComponents;

namespace tts.Web.Views
{
    public abstract class ttsViewComponent : AbpViewComponent
    {
        protected ttsViewComponent()
        {
            LocalizationSourceName = ttsConsts.LocalizationSourceName;
        }
    }
}
