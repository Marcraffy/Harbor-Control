using HarborControl.Interfaces.Services;
using System;

namespace HarborControl.Tests.MockWeatherService
{
    public class WeatherService : IWeatherService
    {
        public float WindSpeed => 0;

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}