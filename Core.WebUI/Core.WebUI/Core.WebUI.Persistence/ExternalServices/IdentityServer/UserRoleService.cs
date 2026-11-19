using Core.WebUI.Application.Commons.Parameters;
using Core.WebUI.Application.Dtos.IdentityDtos.UserRoleDtos;
using Core.WebUI.Application.Interfaces.Services.IdentityServer.UserRoleService;
using Microsoft.Extensions.Configuration;

namespace Core.WebUI.Persistence.ExternalServices.IdentityServer
{
    public class UserRoleService : BaseService, IUserRoleService
    {
        protected override string ServiceBaseUrl { get; set; }
        protected override string Endpoint { get; set; }
        protected override string HttpClientName { get; set; }

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public UserRoleService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;

            ServiceBaseUrl = _configuration.GetSection("ApiSetting:IdentityServerBaseUrl").Value;
            Endpoint = _configuration.GetSection("ApiSetting:IdentityServer:APIUrl:UserRole").Value;
            HttpClientName = _configuration.GetSection("ApiSetting:HttpClientName").Value;
        }

        public Task<UIApiResponse<IReadOnlyCollection<UserRoleResultDto>>> GetListAsync(Guid? id = null)
        {
            throw new NotImplementedException();
        }

        public Task<UIApiResponse<UserRoleResultDto>> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UIApiResponse<bool>> CreateAsync(UserRoleCreateDto createDto)
        {
            throw new NotImplementedException();
        }

        public Task<UIApiResponse<bool>> UpdateAsync(UserRoleUpdateDto updateDto)
        {
            throw new NotImplementedException();
        }

        public Task<UIApiResponse<bool>> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}