using HealthCare.Descriptions.Application.Features.Mediators.Cities.Commands;
using HealthCare.Descriptions.Application.Features.Mediators.Cities.Queries;
using HealthCare.Descriptions.WebAPI.Common.Helpers.ControllerHelpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare.Descriptions.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IControllerHelper<CitiesController> _controllerHelper;

        public CitiesController(IMediator mediator, IControllerHelper<CitiesController> controllerHelper)
        {
            _mediator = mediator;
            _controllerHelper = controllerHelper;
        }

        [HttpPost("cities/paging")]
        public async Task<IActionResult> GetCities(GetCitiesQuery query)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(query),
                nameof(GetCities)
                );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCityById(Guid id)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(new GetCityByIdQuery(id)),
                nameof(GetCityById)
                );
        }

        [HttpPost("query")]
        public async Task<IActionResult> GetCitiesByFilter(GetCitiesByFilterQuery query)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(query),
                nameof(GetCitiesByFilter)
                );
        }

        [HttpPost()]
        public async Task<IActionResult> CreateCity(CreateCityCommand query)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(query),
                nameof(CreateCity)
                );
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateCity(UpdateCityCommand query)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(query),
                nameof(UpdateCity)
                );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(Guid id)
        {
            return await _controllerHelper.ExecuteAsync(
                () => _mediator.Send(new RemoveCityCommand(id)),
                nameof(DeleteCity)
                );
        }
    }
}