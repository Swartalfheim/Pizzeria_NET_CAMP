using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAppCommandProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using WebAppCommandProject.ContextDB;

namespace WebAppCommandProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public AccountController(ApplicationContext context)
        {
            _context = context;
        }

        //POST: account/login
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] EntityLoginModel data)
        {
            // receive from form email and password
            // if email and/or password not set, send error code 400
            if (data.Email == "" || data.Password == "")
                return BadRequest("Email and/or password not set");
            string email = data.Email;
            string password = data.Password;

            // find user
            User user = await _context.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            // if user not found, send 401
            if (user is null) return Unauthorized();

            await Authenticate(user);

            
            return Ok(new { message = $"User with email: {email} and Role: {user.Role?.Name} successfully logged" });
        }


        // GET: account/logout
        [Route("logout")]
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok(new { message = "The user has logged out" });
        }


        //POST: account/registration
        [Route("registration")]
        [HttpPost]
        public async Task<IActionResult> Registration([FromBody] EntityRegisterModel data)
        {
            // receive from form email and password
            // if email and/or password not set, send error code 400
            if (data.Email == "" || data.Password == "" || data.ConfirmPassword == "" || data.Password != data.ConfirmPassword)
                return BadRequest("Email and/or password not set");
            string email = data.Email;
            string password = data.Password;

            // find user
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            // if user not found, registration
            if (user is null)
            {
                user = new User { Email = email, Password = password };
                Role userRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "user");

                if (userRole != null)
                {
                    user.Role = userRole;
                }
                
                _context.Users.Add(user);

                await _context.SaveChangesAsync();

                await Authenticate(user);
            }
            else
            {

                return Ok(new { message = "User is already registered" });
            }

            //return Redirect("/");

            return Ok(new { message = $"User with email: {email} successfully registered" });
        }

        private async Task Authenticate(User user)
        {
            // create one claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };
            // create object ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // set cookies
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

    }
}
