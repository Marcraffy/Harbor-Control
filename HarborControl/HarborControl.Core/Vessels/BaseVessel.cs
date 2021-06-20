using HarborControl.Interfaces.Enums;
using HarborControl.Interfaces.Vessels;
using System;

namespace HarborControl.Core.Vessels
{
    public abstract class BaseVessel : IVessel
    {
        public int Speed { get; }
        public string Name { get; set; }
        public VesselType Type { get; }
        public Location Location { get; set; }
        public Direction Direction { get; init; }
        public DateTime Arrival { get; init; }
        public DateTime? TransitStart { get; set; }
}
}