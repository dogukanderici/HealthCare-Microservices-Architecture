using Core.WebUI.Application.Commons.Parameters;
using Core.WebUI.Application.Dtos.WorkflowEngineDtos.DynamicPolicyDtos;
using Core.WebUI.Application.Interfaces.Services.WorkflowEngine;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebUI.Persistence.ExternalServices.WorkflowEngine
{
    public class DynamicPolicyService : BaseService, IDynamicPolicyService
    {

        protected override string ServiceBaseUrl { get; set; }
        protected override string HttpClientName { get; set; }
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public DynamicPolicyService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;

            ServiceBaseUrl = _configuration.GetSection("ApiSetting:WorkflowEngine:ServiceBaseUrl").Value;
            HttpClientName = _configuration.GetSection("ApiSetting:ApiHttpClientName").Value;
        }

        public async Task<UIApiResponse<IReadOnlyCollection<DynamicPolicyResultDto>>> GetListAsync()
        {
            HttpClient client = _httpClientFactory.CreateClient(HttpClientName);

            HttpResponseMessage response = await client.GetAsync("dynamicpolicies");

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();

                ApiServiceResponse<IReadOnlyCollection<DynamicPolicyResultDto>> apiServiceResponse =
                    JsonConvert.DeserializeObject<ApiServiceResponse<IReadOnlyCollection<DynamicPolicyResultDto>>>(jsonData);

                if (apiServiceResponse.IsSuccess)
                {
                    return UIApiResponse<IReadOnlyCollection<DynamicPolicyResultDto>>.Success(apiServiceResponse.Data);
                }

                return UIApiResponse<IReadOnlyCollection<DynamicPolicyResultDto>>.Fail(apiServiceResponse.Message);
            }

            return UIApiResponse<IReadOnlyCollection<DynamicPolicyResultDto>>.Fail();
        }

        public Task<UIApiResponse<DynamicPolicyResultDto>> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UIApiResponse<bool>> CreateAsync(DynamicPolicyCreateDto createDto)
        {
            throw new NotImplementedException();
        }

        public Task<UIApiResponse<bool>> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UIApiResponse<bool>> UpdateAsync(DynamicPolicyUpdateDto updateDto)
        {
            throw new NotImplementedException();
        }
    }
}
