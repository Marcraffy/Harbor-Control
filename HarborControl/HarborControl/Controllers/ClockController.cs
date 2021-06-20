﻿using HarborControl.Interfaces.Services;
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
        public ActionResult<DateTime> Get()
        {
            logger.LogInformation($"Time requested");
            return new ActionResult<DateTime>(clockService.CurrentTime);
        }

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