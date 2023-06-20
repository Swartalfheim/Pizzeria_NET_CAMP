using Microsoft.AspNetCore.Mvc;
using WebAppCommandProject.Services;

namespace WebAppCommandProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly WorkerServices _worker;

        public WorkerController(WorkerServices worker)
        {
            _worker = worker;
        }


        // POST: api/Worker/set_message
        [Route("set_message")]
        [HttpPost]
        public async Task<IActionResult> PostMessage([FromBody] string messageNew)
        {
            try
            {
                _worker.AddWaiter(messageNew);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e}");
            }
        }

        // POST: api/Worker/stop
        [Route("stop")]
        [HttpPost]
        public async Task<IActionResult> PostStop()
        {
            try
            {
                _worker.ManualControl(Model.CommandManual.Stop);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e}");
            }
        }

        // POST: api/Worker/pause
        [Route("pause")]
        [HttpPost]
        public async Task<IActionResult> PostPause()
        {
            try
            {
                _worker.ManualControl(Model.CommandManual.Pause);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e}");
            }
        }

        // POST: api/Worker/start
        [Route("start")]
        [HttpPost]
        public async Task<IActionResult> PostStart()
        {
            try
            {
                _worker.ManualControl(Model.CommandManual.Start);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e}");
            }
        }

        // POST: api/Worker/continue
        [Route("continue")]
        [HttpPost]
        public async Task<IActionResult> PostContinue()
        {
            try
            {
                _worker.ManualControl(Model.CommandManual.Continue);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e}");
            }
        }
    }
}
