using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RoleBasedAuthorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DemoController : ControllerBase
    {
        [HttpGet(Name = "Demo")]
        [AllowAnonymous]
        public async Task<ActionResult> Get()
        {
            return Ok("Hello from the Demo API");
        }

        [HttpGet("admin", Name = "AdminDemo")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetAdminProtected()
        {
            return Ok("Hello from the Admin Only route");
        }

        [HttpGet("user", Name = "UserDemo")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> GetUserProtected()
        {
            return Ok("Hello from the User Route");
        }

        [HttpGet("authorize", Name = "DemoAuthorize")]
        public async Task<ActionResult> GetProtected()
        {
            return Ok("Hello");

        }
    }
}

