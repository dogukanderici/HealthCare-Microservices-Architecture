using Core.AdminUI.Models.WorkflowEngineModels.ProcessDefiniitonModels;
using Core.WebUI.Application.Commons.Parameters;
using Core.WebUI.Application.Dtos.WorkflowEngineDtos.ProcessDefinitionDtos;
using Core.WebUI.Application.Interfaces.Services.WorkflowEngine;
using Microsoft.AspNetCore.Mvc;

namespace Core.AdminUI.Controllers
{
    public class ProcessController : BaseController
    {
        private readonly IProcessDefinitionService _service;

        public ProcessController(IProcessDefinitionService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            UIApiResponse<IReadOnlyCollection<ProcessDefinitionResultDto>> result = await _service.GetListAsync();

            ProcessDefinitionResultModel<IReadOnlyCollection<ProcessDefinitionResultDto>> model = new ProcessDefinitionResultModel<IReadOnlyCollection<ProcessDefinitionResultDto>>();

            return View(model);
        }
    }
}