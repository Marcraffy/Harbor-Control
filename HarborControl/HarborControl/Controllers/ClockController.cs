using HarborControl.Interfaces.Services;
using HarborControl.Models;
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

        /// <summary>
        /// Get current time and time multiplier
        /// </summary>
        [HttpGet]
        public ActionResult<Clock> Get()
        {
            logger.LogInformation($"Time requested");
            return new ActionResult<Clock>(
                new Clock {
                    CurrentTime = clockService.CurrentTime.ToString("dd MMM yyyy, HH:mm"),
                    Multiplier = clockService.Multiplier
                }
            );
        }

        /// <summary>
        /// Change multiplier
        /// </summary>
        /// <param name="multiplier">any integer value between 1 and 16 inclusive</param>
        [HttpPost]
        public ActionResult Post([FromQuery] int multiplier)
        {
            if (multiplier < 0 || multiplier > 16)
            {
                return new BadRequestResult();
            }

            clockService.Multiplier = multiplier;
            logger.LogInformation($"Multiplier set to {multiplier}");
            return new OkResult();
        }
    }
}