using HarborControl.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HarborControl.Weather
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient client;
        private readonly IConfiguration configuration;

        private string Query => $"weather?q={configuration["OpenWeather.City"]}&appid={configuration["OpenWeather.Key"]}";

        public WeatherService(IConfiguration configuration)
        {
            this.configuration = configuration;
            client = new HttpClient
            {
                BaseAddress = new Uri(configuration["OpenWeather.Endpoint"])
            };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        
        public float WindSpeed => GetWindSpeed();

        private float GetWindSpeed()
        {
            var windspeed = QueryWindSpeedAsync().GetAwaiter().GetResult();
            return windspeed;
        }

        private async Task<float> QueryWindSpeedAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Query);
            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            var contentObject = JsonConvert.DeserializeObject<dynamic>(content);
            float windspeed = Convert.ToDouble(contentObject["wind"]["speed"]);
            return (float)(windspeed * 3.6);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}