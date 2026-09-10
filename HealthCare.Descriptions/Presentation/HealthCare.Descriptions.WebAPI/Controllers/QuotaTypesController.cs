using HealthCare.Descriptions.Application.Features.Mediators.QuotaTypes.Commands;
using HealthCare.Descriptions.Application.Features.Mediators.QuotaTypes.Queries;
using HealthCare.Descriptions.WebAPI.Common.Helpers.ControllerHelpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare.Descriptions.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotaTypesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IControllerHelper<QuotaTypesController> _controllerHelper;

        public QuotaTypesController(IMediator mediator, IControllerHelper<QuotaTypesController> controllerHelper)
        {
            _mediator = mediator;
            _controllerHelper = controllerHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetQuotaTypesAsync()
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(new GetQuotaTypesQuery()),
                nameof(GetQuotaTypesAsync)
                );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuotaTypeByIdAsync(Guid id)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(new GetQuotaTypeByIdQuery(id)),
                nameof(GetQuotaTypeByIdAsync)
                );
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuotaTypeAsync(CreateQuotaTypeCommand command)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(command),
                nameof(CreateQuotaTypeAsync)
                );
        }

        [HttpPut]
        public async Task<IActionResult> UpdateQuotaTypeAsync(UpdateQuotaTypeCommand command)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(command),
                nameof(UpdateQuotaTypeAsync)
                );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuotaTypeAsync(Guid id)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(new RemoveQuotaTypeCommand(id)),
                nameof(DeleteQuotaTypeAsync)
                );
        }
    }
}