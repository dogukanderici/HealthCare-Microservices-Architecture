using Core.WebUI.Application.Commons.Parameters;
using Core.WebUI.Application.Dtos.WorkflowEngineDtos.ProcessTaskDtos;
using Core.WebUI.Application.Interfaces.Services.WorkflowEngine;
using Core.WebUI.Persistence.Commons.Extensions;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace Core.WebUI.Persistence.ExternalServices.WorkflowEngine
{
    public class ProcessTaskService : BaseService, IProcessTaskService
    {
        protected override string ServiceBaseUrl { get; set; }
        protected override string Endpoint { get; set; }
        protected override string HttpClientName { get; set; }

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;


        public ProcessTaskService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;

            ServiceBaseUrl = _configuration.GetSection("ApiSetting:WorkflowEngine:ServiceBaseUrl").Value;
            Endpoint = _configuration.GetSection("ApiSetting:WorkflowEngine:APIUrl:ProcessTask").Value;
            HttpClientName = _configuration.GetSection("ApiSetting:ApiHttpClientName").Value;
        }

        public async Task<UIApiResponse<IReadOnlyCollection<ProcessTaskResultDto>>> GetListAsync(Guid? id = null)
        {
            // id => Process Id
            if ((id == null) || (Guid.Empty == id))
            {
                return UIApiResponse<IReadOnlyCollection<ProcessTaskResultDto>>.Fail("Invalid Process Id Value!");
            }

            HttpClient client = _httpClientFactory.CreateClient(HttpClientName);

            HttpResponseMessage response = await client.GetAsync($"{Endpoint}/process/{id}");

            return await response.ReturnServiceResultAsync<IReadOnlyCollection<ProcessTaskResultDto>>();
        }

        public async Task<UIApiResponse<ProcessTaskResultDto>> GetAsync(Guid id)
        {
            HttpClient client = _httpClientFactory.CreateClient(HttpClientName);

            HttpResponseMessage response = await client.GetAsync($"{Endpoint}/{id}");

            return await response.ReturnServiceResultAsync<ProcessTaskResultDto>();
        }

        public async Task<UIApiResponse<Guid>> CreateAsync(ProcessTaskCreateDto createDto)
        {
            HttpClient client = _httpClientFactory.CreateClient(HttpClientName);

            HttpResponseMessage response = await client.PostAsJsonAsync(Endpoint, createDto);

            return await response.ReturnServiceResultAsync<Guid>();
        }

        public async Task<UIApiResponse<DateTimeOffset>> UpdateAsync(ProcessTaskUpdateDto updateDto)
        {
            HttpClient client = _httpClientFactory.CreateClient(HttpClientName);

            HttpResponseMessage response = await client.PutAsJsonAsync(Endpoint, updateDto);

            return await response.ReturnServiceResultAsync<DateTimeOffset>();
        }

        public async Task<UIApiResponse<bool>> DeleteAsync(Guid id)
        {
            HttpClient client = _httpClientFactory.CreateClient(HttpClientName);

            HttpResponseMessage response = await client.DeleteAsync($"{Endpoint}/{id}");

            return await response.ReturnServiceResultAsync<bool>();
        }
    }
}