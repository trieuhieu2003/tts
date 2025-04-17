using System.Threading.Tasks;
using Abp.Application.Services;
using tts.Authorization.Accounts.Dto;

namespace tts.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
