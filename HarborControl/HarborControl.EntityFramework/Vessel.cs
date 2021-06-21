using HarborControl.Interfaces.Enums;
using HarborControl.Interfaces.Vessels;
using System;

namespace HarborControl.EntityFramework
{
    public sealed class Vessel : IVessel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public int Speed { get; set; }

        public VesselType Type { get; set; }

        public Location Location { get; set; }

        public Direction Direction { get; init; }

        public DateTime Arrival { get; init; }

        public DateTime? TransitStart { get; set; }
    }
}