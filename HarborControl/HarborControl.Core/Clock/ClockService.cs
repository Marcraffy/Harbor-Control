using HarborControl.Interfaces.Services;
using System;

namespace HarborControl.Core.Clock
{
    public class ClockService : BaseService, IClockService
    {
        public DateTime CurrentTime => DateTime.Now;
    }
}