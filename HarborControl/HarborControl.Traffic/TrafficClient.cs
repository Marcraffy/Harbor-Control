using HarborControl.Interfaces.Enums;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HarborControl.Traffic
{
    public class TrafficClient : IDisposable
    {
        private readonly HttpClient client;
        private const string endpoint = "https://localhost:?????/";

        public TrafficClient()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(endpoint)
            };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task SendVessalArrival(string name, Location location)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"traffic?name={name}&location={location}");
            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(response.ReasonPhrase, null, response.StatusCode);
            }
        }

        public async Task SendVessalDeparture(string name)
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