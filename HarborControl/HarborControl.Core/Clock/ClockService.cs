using HarborControl.Core.Exceptions;
using HarborControl.Interfaces.Services;
using System;

namespace HarborControl.Core.Clock
{
    public class ClockService : BaseService, IClockService
    {
        private int multiplier = 1;
        private DateTime lastPolledTime = DateTime.Now;
        private DateTime simulatedTime = DateTime.Now;

        public DateTime CurrentTime => GetSimulatedTime();

        public int Multiplier { get => multiplier; set => SetMultiplier(value); }

        private void SetMultiplier(int val)
        {
            if (val < 1 || val > 16)
            {
                throw new ClockException("Multiplier must be between 1 and 16 inclusive");
            }

            CalculateSimulatedTime();
            multiplier = val;
        }

        private DateTime GetSimulatedTime()
        {
            CalculateSimulatedTime();
            return simulatedTime;
        }

        private void CalculateSimulatedTime()
        {
            var timeSinceLastPoll = DateTime.Now - lastPolledTime;
            lastPolledTime = DateTime.Now;
            simulatedTime = simulatedTime.AddSeconds(timeSinceLastPoll.TotalSeconds * multiplier);
        }
    }
}