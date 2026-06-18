using HealthCare.Descriptions.Application.Features.Mediator.Commands.CityCommands;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.CityQueries;
using HealthCare.Descriptions.Application.Features.Mediator.Results.CityResults;
using HealthCare.Descriptions.WebAPI.Constants;
using HealthCare.Descriptions.WebAPI.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;

namespace HealthCare.Descriptions.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CitiesController> _logger;

        public CitiesController(IMediator mediator, ILogger<CitiesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "IdentityServerAccessToken", Roles = "Admin,Read")]
        public async Task<IActionResult> GetCitiesData()
        {
            try
            {
                List<GetCitiesQueryResult> result = await _mediator.Send(new GetCitiesQuery());

                // Validasyon Kuralları Yazılıp InvalidDataException Fırlatılacak.

                _logger.LogInformation(LogConstant.LogMessageTemplate,
                    nameof(CitiesController),
                    nameof(GetCitiesData),
                    LogConstant.SuccessMessage.CallingSuccess);

                return Ok(new GenericApiResponse<GetCitiesQueryResult>
                {
                    StatusCode = 200,
                    Message = LogConstant.SuccessMessage.CallingSuccess,
                    TimeStamp = DateTime.UtcNow,
                    Datas = result
                });
            }
            catch (Exception ex)
            {

                _logger.LogError(LogConstant.LogMessageTemplate,
                    nameof(CitiesController),
                    nameof(GetCitiesData),
                    LogConstant.ErrorMessage.CallingFail);

                return BadRequest(new GenericApiResponse
                {
                    StatusCode = 400,
                    Message = LogConstant.ErrorMessage.CallingFail,
                    TimeStamp = DateTime.UtcNow
                });
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Read")]
        public async Task<IActionResult> GetCityByIdData(Guid id)
        {
            try
            {
                GetCityByIdQueryResult result = await _mediator.Send(new GetCityByIdQuery(id));

                _logger.LogInformation(LogConstant.LogMessageTemplate,
                    nameof(CitiesController),
                    nameof(GetCitiesData),
                    LogConstant.SuccessMessage.CallingSuccess);

                return Ok(new GenericApiResponse<GetCityByIdQueryResult>
                {
                    StatusCode = 200,
                    Message = LogConstant.SuccessMessage.CallingSuccess,
                    TimeStamp = DateTime.UtcNow,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstant.LogMessageTemplate,
                    nameof(CitiesController),
                    nameof(GetCitiesData),
                    LogConstant.ErrorMessage.CallingFail);

                return BadRequest(new GenericApiResponse
                {
                    StatusCode = 400,
                    Message = LogConstant.ErrorMessage.CallingFail,
                    TimeStamp = DateTime.UtcNow
                });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Write")]
        public async Task<IActionResult> CreateCityData(CreateCityCommand createCityCommand)
        {
            try
            {
                createCityCommand.CreatedBy = Guid.NewGuid();
                createCityCommand.CreatedAt = DateTimeOffset.UtcNow;
                createCityCommand.UpdatedBy = Guid.NewGuid();
                createCityCommand.UpdatedAt = DateTimeOffset.UtcNow;

                await _mediator.Send(createCityCommand);

                _logger.LogInformation(LogConstant.LogMessageTemplate,
                    nameof(CitiesController),
                    nameof(GetCitiesData),
                    LogConstant.SuccessMessage.AddingDataSuccessed);

                return Ok(new GenericApiResponse
                {
                    StatusCode = 200,
                    Message = LogConstant.SuccessMessage.AddingDataSuccessed,
                    TimeStamp = DateTime.UtcNow,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstant.LogMessageTemplate,
                    nameof(CitiesController),
                    nameof(GetCitiesData),
                    LogConstant.ErrorMessage.AddingDataFailed);

                return BadRequest(new GenericApiResponse
                {
                    StatusCode = 400,
                    Message = LogConstant.ErrorMessage.AddingDataFailed,
                    TimeStamp = DateTime.UtcNow
                });
            }
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Write")]
        public async Task<IActionResult> UpdateCityData(UpdateCityCommand updateCityCommand)
        {
            try
            {
                if (updateCityCommand.CreatedAt == null && updateCityCommand.CreatedBy == null)
                {
                    var result = await _mediator.Send(new GetCityByIdQuery(updateCityCommand.Id));

                    updateCityCommand.CreatedBy = result.CreatedBy;
                    updateCityCommand.CreatedAt = result.CreatedAt;
                }

                updateCityCommand.UpdatedBy = Guid.NewGuid();
                updateCityCommand.UpdatedAt = DateTimeOffset.UtcNow;

                await _mediator.Send(updateCityCommand);

                _logger.LogInformation(LogConstant.LogMessageTemplate,
                   nameof(CitiesController),
                   nameof(UpdateCityData),
                   LogConstant.SuccessMessage.UpdatingDataSuccessed);

                return Ok(new GenericApiResponse
                {
                    StatusCode = 200,
                    Message = LogConstant.SuccessMessage.UpdatingDataSuccessed,
                    TimeStamp = DateTime.UtcNow,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstant.LogMessageTemplate,
                   nameof(CitiesController),
                   nameof(GetCitiesData),
                   LogConstant.ErrorMessage.AddingDataFailed);

                return BadRequest(new GenericApiResponse
                {
                    StatusCode = 400,
                    Message = LogConstant.ErrorMessage.AddingDataFailed,
                    TimeStamp = DateTime.UtcNow
                });
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCityData(Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteCityCommand(id));

                _logger.LogInformation(LogConstant.LogMessageTemplate,
                   nameof(CitiesController),
                   nameof(DeleteCityData),
                   LogConstant.SuccessMessage.DeletingDataSuccessed);

                return Ok(new GenericApiResponse
                {
                    StatusCode = 200,
                    Message = LogConstant.SuccessMessage.DeletingDataSuccessed,
                    TimeStamp = DateTime.UtcNow,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(LogConstant.LogMessageTemplate,
                   nameof(CitiesController),
                   nameof(GetCitiesData),
                   LogConstant.ErrorMessage.DeletingDataFailed);

                return BadRequest(new GenericApiResponse
                {
                    StatusCode = 400,
                    Message = LogConstant.ErrorMessage.DeletingDataFailed,
                    TimeStamp = DateTime.UtcNow
                });
            }
        }
    }
}