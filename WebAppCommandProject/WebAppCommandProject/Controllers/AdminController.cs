using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppCommandProject.Model.ProjectDTO;
using WebAppCommandProject.Services;

namespace WebAppCommandProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly WorkerServices _worker;

        public AdminController(WorkerServices worker)
        {
            _worker = worker;
        }

        // POST: api/Admin/create-waiter
        [Route("create-waiter")]
        [HttpPost]
        public async Task<IActionResult> PostWaiter(string waiterName)
        {
            try
            {
                _worker.AddWaiter(waiterName);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e}");
            }
        }

        // POST: api/Admin/create-chef
        [Route("create-chef")]
        [HttpPost]
        public async Task<IActionResult> PostChef([FromBody] ChefDto chef)
        {
            try
            {
                _worker.AddChef(chef);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e}");
            }
        }

        // POST: api/Admin/create-client
        [Route("create-client")]
        [HttpPost]
        public async Task<IActionResult> PostClient([FromBody] ClientFromAdminDto client)
        {
            try
            {
                _worker.AddClient(client);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e}");
            }
        }


        // GET: api/Admin/get-menu
        [Route("get-menu")]
        [HttpGet]
        public async Task<IActionResult> GetMenu()
        {
            try
            {
                string menu = _worker.GetMenu();
                return Ok(menu);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e}");
            }
        }

        // GET: api/Admin/get-ingredients
        [Route("get-ingredients")]
        [HttpGet]
        public async Task<IActionResult> GetIngredients()
        {
            try
            {
                string ingredients = _worker.GetIngredients();
                return Ok(ingredients);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e}");
            }
        }
    }
}
