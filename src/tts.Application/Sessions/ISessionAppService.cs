using System.Threading.Tasks;
using Abp.Application.Services;
using tts.Sessions.Dto;

namespace tts.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
