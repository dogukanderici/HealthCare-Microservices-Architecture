using Core.WebUI.Application.Commons.Parameters;
using Core.WebUI.Application.Dtos.WorkflowEngineDtos.ProcessDefinitionDtos;
using Core.WebUI.Application.Interfaces.Services.WorkflowEngine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebUI.Persistence.ExternalServices.WorkflowEngine
{
    public class ProcessDefinitionService : BaseService, IProcessDefinitionService
    {
        protected override string ServiceBaseUrl { get; set; }
        protected override string Endpoint { get; set; }
        protected override string HttpClientName { get; set; }

        private readonly IConfiguration _config;

        private readonly IHttpClientFactory _httpClientFactory;

        public ProcessDefinitionService(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;

            ServiceBaseUrl = _config.GetSection("ApiSetting:WorkflowEngine:ServiceBaseUrl").Value;
            Endpoint = _config.GetSection("ApiSetting:WorkflowEngine:ProcessDefinition").Value;
            HttpClientName = _config.GetSection("ApiSetting:ApiHttpClientName").Value;
        }

        public async Task<UIApiResponse<IReadOnlyCollection<ProcessDefinitionResultDto>>> GetListAsync()
        {
            HttpClient client = _httpClientFactory.CreateClient(HttpClientName);

            HttpResponseMessage response = await client.GetAsync(Endpoint);

            if (response.IsSuccessStatusCode)
            {
                string jsonData = await response.Content.ReadAsStringAsync();
            }

            throw new Exception("opps!");
        }

        public Task<UIApiResponse<ProcessDefinitionResultDto>> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UIApiResponse<Guid>> CreateAsync(ProcessDefinitionCreateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<UIApiResponse<DateTimeOffset>> UpdateAsync(ProcessDefinitionUpdateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<UIApiResponse<bool>> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object? obj)
        {
            return obj is ProcessDefinitionService service &&
                   EqualityComparer<IConfiguration>.Default.Equals(_config, service._config);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_config);
        }

        public Task<UIApiResponse<IReadOnlyCollection<ProcessDefinitionResultDto>>> GetListAsync(Guid? id = null)
        {
            throw new NotImplementedException();
        }
    }
}