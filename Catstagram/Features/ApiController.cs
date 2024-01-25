using Catstagram.Server.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catstagram.Server.Controllers
{
    [ApiController]
    [CustomAuthorization]
    [Route("[controller]")]
    public abstract class ApiController : ControllerBase
    {

    }
}
