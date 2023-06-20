using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppCommandProject.Model.ProjectDTO;
using WebAppCommandProject.Services;

namespace WebAppCommandProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly WorkerServices _worker;

        public ManagerController(WorkerServices worker)
        {
            _worker = worker;
        }

        // GET: api/Manager/get-ingredients
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

        // GET: api/Manager/get-menu
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



        // POST: api/Manager/create-dish
        [Route("create-dish")]
        [HttpPost]
        public async Task<IActionResult> PostDish([FromBody] CreateDishDto dish)
        {
            try
            {
                _worker.AddDish(dish);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e}");
            }
        }
    }
}
