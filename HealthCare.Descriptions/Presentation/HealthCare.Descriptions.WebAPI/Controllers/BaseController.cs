using HealthCare.Descriptions.WebAPI.Policies.DynamicPolicy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare.Descriptions.WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "IdentityServerAccessToken")]
    [DynamicRole]
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
    }
}