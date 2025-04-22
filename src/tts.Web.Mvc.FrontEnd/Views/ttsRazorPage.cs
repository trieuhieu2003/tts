using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace tts.Web.Views
{
    public abstract class ttsRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected ttsRazorPage()
        {
            LocalizationSourceName = ttsConsts.LocalizationSourceName;
        }
    }
}
