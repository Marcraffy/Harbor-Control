using HarborControl.Interfaces.Enums;
using HarborControl.Interfaces.Services;
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

        [HttpPost]
        public void Post([FromQuery] string name, [FromQuery] Location location, [FromQuery] VesselType type)
        {
            logger.LogInformation($"Vessel arrived: {name}, at {location}, of type {type}");
            controlService.VesselArrived(name, location, type);
        }

        [HttpDelete]
        public void Delete([FromQuery] string name)
        {
            logger.LogInformation($"Vessel departed: {name}");
            controlService.VesselDeptarted(name);
        }
    }
}