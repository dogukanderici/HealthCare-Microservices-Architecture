using Core.WebUI.Application.Commons.Parameters;
using Core.WebUI.Application.Dtos.IdentityDtos.UserDtos;
using Core.WebUI.Application.Interfaces.Services.IdentityServer.UserService;
using Microsoft.Extensions.Configuration;

namespace Core.WebUI.Persistence.ExternalServices.IdentityServer
{
    public class UserService : BaseService, IUserService
    {
        protected override string ServiceBaseUrl { get; set; }
        protected override string Endpoint { get; set; }
        protected override string HttpClientName { get; set; }

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public UserService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;

            ServiceBaseUrl = _configuration.GetSection("ApiSetting:IdentityServerBaseUrl").Value;
            Endpoint = _configuration.GetSection("ApiSetting:IdentityServer:APIUrl:User").Value;
            HttpClientName = _configuration.GetSection("ApiSetting:HttpClientName").Value;
        }

        public Task<UIApiResponse<IReadOnlyCollection<UserResultDto>>> GetListAsync(Guid? id = null)
        {
            throw new NotImplementedException();
        }

        public Task<UIApiResponse<UserResultDto>> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UIApiResponse<Guid>> CreateAsync(UserCreateDto createDto)
        {
            throw new NotImplementedException();
        }

        public Task<UIApiResponse<DateTimeOffset>> UpdateAsync(UserUpdateDto updateDto)
        {
            throw new NotImplementedException();
        }

        public Task<UIApiResponse<bool>> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}