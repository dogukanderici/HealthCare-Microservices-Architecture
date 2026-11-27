using Core.WebUI.Application.Commons.Parameters;
using Core.WebUI.Application.Dtos.WorkflowEngineDtos.ProcessTaskActionDtos;
using Core.WebUI.Application.Interfaces.Services.WorkflowEngine;
using Core.WebUI.Persistence.Commons.Extensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebUI.Persistence.ExternalServices.WorkflowEngine
{
    public class ProcessTaskActionService : BaseService, IProcessTaskActionService
    {
        protected override string ServiceBaseUrl { get; set; }
        protected override string Endpoint { get; set; }
        protected override string HttpClientName { get; set; }

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ProcessTaskActionService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;

            ServiceBaseUrl = _configuration.GetSection("ApiSetting:WorkflowEngine:ServiceBaseUrl").Value;
            Endpoint = _configuration.GetSection("ApiSetting:WorkflowEngine:APIUrl:ProcessTaskAction").Value;
            HttpClientName = _configuration.GetSection("ApiSetting:ApiHttpClientName").Value;
        }

        public async Task<UIApiResponse<IReadOnlyCollection<ProcessTaskActionResultDto>>> GetListAsync(Guid? id = null)
        {
            if (id == null || (Guid.Empty == id))
                return UIApiResponse<IReadOnlyCollection<ProcessTaskActionResultDto>>.Fail("Invalid guid data!");

            HttpClient client = _httpClientFactory.CreateClient(HttpClientName);

            HttpResponseMessage response = await client.GetAsync($"{Endpoint}/task/{id}");

            return await response.ReturnServiceResultAsync<IReadOnlyCollection<ProcessTaskActionResultDto>>();
        }

        public async Task<UIApiResponse<ProcessTaskActionResultDto>> GetAsync(Guid id)
        {
            HttpClient client = _httpClientFactory.CreateClient(HttpClientName);

            HttpResponseMessage response = await client.GetAsync($"{Endpoint}/{id}");

            return await response.ReturnServiceResultAsync<ProcessTaskActionResultDto>();
        }

        public async Task<UIApiResponse<Guid>> CreateAsync(ProcessTaskActionCreateDto createDto)
        {
            HttpClient client = _httpClientFactory.CreateClient(HttpClientName);

            HttpResponseMessage response = await client.PostAsJsonAsync(Endpoint, createDto);

            return await response.ReturnServiceResultAsync<Guid>();
        }

        public async Task<UIApiResponse<DateTimeOffset>> UpdateAsync(ProcessTaskActionUpdateDto updateDto)
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