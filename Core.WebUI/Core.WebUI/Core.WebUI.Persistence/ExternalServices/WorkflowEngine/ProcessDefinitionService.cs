using Core.WebUI.Application.Commons.Parameters;
using Core.WebUI.Application.Dtos.WorkflowEngineDtos.ProcessDefinitionDtos;
using Core.WebUI.Application.Interfaces.Services.WorkflowEngine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
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
        protected override string HttpClientName { get; set; }

        private readonly IConfiguration _config;

        private readonly IHttpClientFactory _httpClientFactory;

        public ProcessDefinitionService(IHttpClientFactory httpClientFactory)
        {
            ServiceBaseUrl = _config.GetSection("ApiSetting:WorkflowEngine:ServiceBaseUrl").Value;
            HttpClientName = _config.GetSection("ApiSetting:ApiHttpClientName").Value;

            _httpClientFactory = httpClientFactory;
        }

        public async Task<UIApiResponse<IReadOnlyCollection<ProcessDefinitionResultDto>>> GetListAsync()
        {
            HttpClient client = _httpClientFactory.CreateClient(HttpClientName);

            var response = await client.GetAsync("processes");

            throw new Exception("opps!");
        }

        public Task<UIApiResponse<ProcessDefinitionResultDto>> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UIApiResponse<bool>> CreateAsync(ProcessDefinitionCreateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<UIApiResponse<bool>> UpdateAsync(ProcessDefinitionUpdateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<UIApiResponse<bool>> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}