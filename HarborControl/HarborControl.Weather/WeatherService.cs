using HarborControl.Interfaces.Configuration;
using HarborControl.Interfaces.Services;
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
        private const string key = "5301fa5fad04317c0223308595bcee00";
        private const string endpoint = "https://api.openweathermap.org/data/2.5/";
        private const string city = "Durban, ZA";

        private string Query => $"weather?q={configuration.OpenWeatherCity}&appid={configuration.OpenWeatherKey}";

        public WeatherService(IConfiguration configuration)
        {
            this.configuration = configuration;
            client = new HttpClient
            {
                BaseAddress = new Uri(configuration.OpenWeatherEndpoint)
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