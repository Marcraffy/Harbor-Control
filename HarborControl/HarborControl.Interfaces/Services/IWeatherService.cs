namespace HarborControl.Interfaces.Services
{
    public interface IWeatherService : IService
    {
        public float WindSpeed { get; }
    }
}