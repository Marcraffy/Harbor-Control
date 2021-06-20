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

        [HttpDelete]
        public ActionResult Delete([FromQuery] string name)
        {
            logger.LogInformation($"Vessel departed: {name}");
            controlService.VesselDeptarted(name);
            return new OkResult();
        }
    }
}