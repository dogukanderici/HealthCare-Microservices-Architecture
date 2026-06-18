using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.IdentityServer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
    }
}
