using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppCommandProject.ContextDB;
using WebAppCommandProject.Services;

namespace WebAppCommandProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorController : ControllerBase
    {
        private readonly WorkerServices _worker;

        public VisitorController(WorkerServices worker)
        {
            _worker = worker;
        }

        // GET: api/Visitor/get-cashreg
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
