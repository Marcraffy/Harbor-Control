using System;

namespace HarborControl.Interfaces.Services
{
    public interface IClockService : IService
    {
        public DateTime CurrentTime { get; }
    }
}