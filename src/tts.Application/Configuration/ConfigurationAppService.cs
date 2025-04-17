using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using tts.Configuration.Dto;

namespace tts.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : ttsAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
