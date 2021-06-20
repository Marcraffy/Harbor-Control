using HarborControl.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HarborControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly ILogger<WeatherController> logger;
        private readonly IWeatherService weatherService;

        public WeatherController(ILogger<WeatherController> logger,
            IWeatherService weatherService)
        {
            this.logger = logger;
            this.weatherService = weatherService;
        }

        [HttpGet]
        public float Get()
        {
            logger.LogInformation($"Windspeed requested");
            return weatherService.WindSpeed;
        }
    }
}