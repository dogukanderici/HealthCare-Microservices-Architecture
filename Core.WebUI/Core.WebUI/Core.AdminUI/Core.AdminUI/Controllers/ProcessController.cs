using Core.AdminUI.Models.WorkflowEngineModels.ProcessDefinitionModels;
using Core.WebUI.Application.Commons.Parameters;
using Core.WebUI.Application.Dtos.WorkflowEngineDtos.ProcessDefinitionDtos;
using Core.WebUI.Application.Interfaces.Services.WorkflowEngine;
using Microsoft.AspNetCore.Mvc;

namespace Core.AdminUI.Controllers
{
    [Route("WorkflowEngine/Process")]
    public class ProcessController : BaseController
    {
        private readonly IProcessDefinitionService _service;

        public ProcessController(IProcessDefinitionService service)
        {
            _service = service;
        }

        [HttpGet()]
        public async Task<IActionResult> Index()
        {
            UIApiResponse<IReadOnlyCollection<ProcessDefinitionResultDto>> result = await _service.GetListAsync();

            ProcessDefinitionResultModel<IReadOnlyCollection<ProcessDefinitionResultDto>> model = new ProcessDefinitionResultModel<IReadOnlyCollection<ProcessDefinitionResultDto>>();

            model.ModelData = result.Data;

            return View(model);
        }
    }}