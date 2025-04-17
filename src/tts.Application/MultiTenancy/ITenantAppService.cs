using Abp.Application.Services;
using tts.MultiTenancy.Dto;

namespace tts.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

