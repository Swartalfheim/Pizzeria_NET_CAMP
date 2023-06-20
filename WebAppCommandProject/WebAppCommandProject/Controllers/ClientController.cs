using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppCommandProject.Model.ProjectDTO;
using WebAppCommandProject.Services;

namespace WebAppCommandProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly WorkerServices _worker;

        public ClientController(WorkerServices worker)
        {
            _worker = worker;
        }

        // POST: api/Client/create-order
        [Route("create-order")]
        [HttpPost]
        public async Task<IActionResult> PostOrder([FromBody] OrderDto dish)
        {
            try
            {
                _worker.AddOrder(dish);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e}");
            }
        }

        // GET: api/Client/get-menu
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

        // GET: api/Client/get-cashreg
        [Route("get-cashreg")]
        [HttpGet]
        public async Task<IActionResult> GetCashRegister()
        {
            try
            {
                string cashRegister = _worker.GetCashRegisters();
                return Ok(cashRegister);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e}");
            }
        }
    }
}
