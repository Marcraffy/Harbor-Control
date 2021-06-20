using HarborControl.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace HarborControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClockController : ControllerBase
    {
        private readonly ILogger<ClockController> logger;
        private readonly IClockService clockService;

        public ClockController(ILogger<ClockController> logger,
            IClockService clockService)
        {
            this.logger = logger;
            this.clockService = clockService;
        }

        [HttpGet]
        public DateTime Get()
        {
            logger.LogInformation($"Time requested");
            return clockService.CurrentTime;
        }

        [HttpPost]
        public void Post([FromQuery] int multiplier)
        {
            clockService.Multiplier = multiplier;
            logger.LogInformation($"Multiplier set to {multiplier}");
        }
    }
}