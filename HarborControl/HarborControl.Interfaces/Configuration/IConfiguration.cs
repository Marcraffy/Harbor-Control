namespace HarborControl.Interfaces.Configuration
{
    public interface IConfiguration
    {
        public string OpenWeatherKey { get; set; }
        public string OpenWeatherEndpoint { get; set; }
        public string OpenWeatherCity { get; set; }

    }
}