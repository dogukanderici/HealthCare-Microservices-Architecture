using Core.WebUI.Application.Commons.Parameters;
using Core.WebUI.Application.Dtos.IdentityDtos.RoleDtos;
using Core.WebUI.Application.Interfaces.Services.IdentityServer.RoleService;
using Microsoft.Extensions.Configuration;

namespace Core.WebUI.Persistence.ExternalServices.IdentityServer
{
    public class RoleService : BaseService, IRoleService
    {
        protected override string ServiceBaseUrl { get; set; }
        protected override string Endpoint { get; set; }
        protected override string HttpClientName { get; set; }

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public RoleService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;

            ServiceBaseUrl = _configuration.GetSection("ApiSetting:IdentityServerBaseUrl").Value;
            Endpoint = _configuration.GetSection("ApiSetting:IdentityServer:APIUrl:Role").Value;
            HttpClientName = _configuration.GetSection("ApiSetting:HttpClientName").Value;
        }

        public Task<UIApiResponse<IReadOnlyCollection<RoleResultDto>>> GetListAsync(Guid? id = null)
        {
            throw new NotImplementedException();
        }

        public Task<UIApiResponse<RoleResultDto>> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UIApiResponse<bool>> CreateAsync(RoleCreateDto createDto)
        {
            throw new NotImplementedException();
        }

        public Task<UIApiResponse<bool>> UpdateAsync(RoleUpdateDto updateDto)
        {
            throw new NotImplementedException();
        }

        public Task<UIApiResponse<bool>> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}