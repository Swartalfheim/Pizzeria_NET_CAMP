using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace WebAppCommandProject.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        // Get: api/test/admin_user
        [Authorize(Roles = "admin, user")]
        [Route("admin_user")]
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var user = User.Identity;
                if (user is not null && user.IsAuthenticated)
                {
                    string role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
                    return Ok(new { message = $"Your role in endpoint for admin and user: {role}" });
                }
                else
                {
                    return Ok(new { message = $"Your role in endpoint for all: NO ROLE" });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e}");
            }
            
        }


        // Get: api/test/admin
        [Authorize(Roles = "admin")]
        [Route("admin")]
        [HttpGet]
        public IActionResult About()
        {
            try
            {
                var user = User.Identity;
                if (user is not null && user.IsAuthenticated)
                {
                    string role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
                    return Ok(new { message = $"Your role in endpoint for all: {role}" });
                }
                else
                {
                    return Ok(new { message = $"Your role in endpoint for all: NO ROLE" });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e}");
            }
        }

        // Get: api/test/general
        [Route("general")]
        [HttpGet]
        public IActionResult Getmessage()
        {
            try
            {
                var user = User.Identity;
                if (user is not null && user.IsAuthenticated)
                {
                    string role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
                    return Ok(new { message = $"Your role in endpoint for all: {role}" });
                }
                else
                {
                    return Ok(new { message = "Your role in endpoint for all: NO ROLE" });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e}");
            }
        }
    }
}
