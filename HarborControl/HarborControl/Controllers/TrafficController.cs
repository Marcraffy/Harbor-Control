using HarborControl.Interfaces.Enums;
using HarborControl.Interfaces.Services;
using HarborControl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HarborControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrafficController : ControllerBase
    {
        private readonly ILogger<TrafficController> logger;
        private readonly IControlService controlService;

        public TrafficController(ILogger<TrafficController> logger,
            IControlService controlService)
        {
            this.logger = logger;
            this.controlService = controlService;
        }
        
        /// <summary>
        /// Get current state of Queue
        /// </summary>
        /// <returns>TrafficState which contains an array of vessels waiting at the harbour, an array of vessels waiting at the perimeter, and a vessel traversing the perimeter if there is one.</returns>
        [HttpGet]
        public ActionResult<TrafficState> Get()
        {
            logger.LogInformation($"Queue state request recieved");
            var output = new TrafficState
            {
                Harbor = controlService.VesselsAtHarbor,
                Perimeter = controlService.VesselsAtPerimeter,
                Transit = controlService.VesselInTransit
            };

            return new ActionResult<TrafficState>(output);
        }


        /// <summary>
        /// Add a vessel to the queue
        /// </summary>
        /// <param name="name">Name of vessel (must be unique)</param>
        /// <param name="location">Location of vessel: Either 0 - Perimeter or 1 - Harbor</param>
        /// <param name="type">Type of vessel: Either 0 - Cargoship, 1 - Sailboat or 2 - Speedboat</param>
        [HttpPost]
        public ActionResult Post([FromQuery] string name, [FromQuery] Location location, [FromQuery] VesselType type)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new BadRequestResult();
            }

            logger.LogInformation($"Vessel arrived: {name}, at {location}, of type {type}");
            controlService.VesselArrived(name, location, type);
            return new OkResult();
        }

        /// <summary>
        /// Remove a vessel from the queue
        /// </summary>
        /// <param name="name">Name of vessel in the queue</param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult Delete([FromQuery] string name)
        {
            logger.LogInformation($"Vessel departed: {name}");
            controlService.VesselDeptarted(name);
            return new OkResult();
        }
    }
}