using Core.WorkflowEngine.Application.Features.Mediator.Queries.InboxQueries;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.WebAPI.Helpers.ControllerResponseHelpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using static Core.WorkflowEngine.WebAPI.Constants.LogConstants;

namespace Core.WorkflowEngine.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InboxesController : BaseController
    {
        private ICurrentUserService _currentUserService;
        private readonly IMediator _mediator;
        private IControllerReponseHelper<InboxesController> _controllerHelper;

        public InboxesController(ICurrentUserService currentUserService, IMediator mediator, IControllerReponseHelper<InboxesController> controllerHelper)
        {
            _currentUserService = currentUserService;
            _mediator = mediator;
            _controllerHelper = controllerHelper;
        }

        [Authorize(AuthenticationSchemes = "IdentityServerAccessToken", Roles = "Read")]
        [HttpGet]
        public Task<IActionResult> GetInboxesForUser()
        {
            return _controllerHelper.ExecuteAsync(
                () => _mediator.Send(new GetInboxByUserIdQuery(_currentUserService.UserId)),
                nameof(GetInboxesForUser),
                SuccessMessage.CallingSuccess,
                ErrorMessage.CallingFail
                );
        }

        [Authorize(AuthenticationSchemes = "IdentityServerAccessToken", Roles = "Admin")]
        [HttpGet("userId/{userId}")]
        public Task<IActionResult> GetInboxesByUserId(Guid userId)
        {
            return _controllerHelper.ExecuteAsync(
                () => _mediator.Send(new GetInboxByUserIdQuery(userId)),
                nameof(GetInboxesByUserId),
                SuccessMessage.CallingSuccess,
                ErrorMessage.CallingFail
                );
        }
    }
}