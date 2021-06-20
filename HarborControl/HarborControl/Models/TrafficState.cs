using HarborControl.Interfaces.Vessels;
using System.Collections.Generic;

namespace HarborControl.Models
{
    public class TrafficState
    {
        public IVessel Transit { get; set; }

        public IList<IVessel> Harbor { get; set; }

        public IList<IVessel> Perimeter { get; set; }
    }
}