using HarborControl.Interfaces.Enums;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HarborControl.Traffic
{
    public class TrafficClient : IDisposable
    {
        private readonly HttpClient client;
        private const string endpoint = "http://localhost:50598/api/";

        public TrafficClient()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(endpoint)
            };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<int> GetMultiplierAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"clock");
            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(response.ReasonPhrase, null, response.StatusCode);
            }

            var content = await response.Content.ReadAsStringAsync();

            var clock = JsonConvert.DeserializeObject<dynamic>(content);
            return (int)clock.multiplier;
        }

        public async Task SendVessalArrivalAsync(string name, Location location, VesselType type)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"traffic?name={name}&location={location}&type={type}");
            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(response.ReasonPhrase, null, response.StatusCode);
            }
        }

        public async Task SendVessalDepartureAsync(string name)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"traffic?name={name}");
            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(response.ReasonPhrase, null, response.StatusCode);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}