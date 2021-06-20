using HarborControl.Interfaces.Services;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HarborControl.Weather
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient client;
        private const string key = "5301fa5fad04317c0223308595bcee00";
        private const string endpoint = "api.openweathermap.org/data/2.5/";
        private const string city = "Durban, ZA";

        private string query => $"weather?q={city}&appid={key}";

        public WeatherService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(endpoint);
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
            var request = new HttpRequestMessage(HttpMethod.Get, query);
            var response = await client.SendAsync(request);
            var windspeed = response.
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}