using HarborControl.Interfaces.Services;
using System;

namespace HarborControl.Core.Clock
{
    public class ClockService : BaseService, IClockService
    {
        private int multiplier = 1;

        public ClockService()
        {

        }

        public DateTime CurrentTime => DateTime.Now;

        public int Multiplier { get => multiplier; set => multiplier = value; }
    }
}