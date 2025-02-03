using Microsoft.AspNetCore.Mvc;

namespace Mail.Engine.Service.Api.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {

    }
}