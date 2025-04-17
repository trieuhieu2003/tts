using System.Threading.Tasks;
using tts.Configuration.Dto;

namespace tts.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
