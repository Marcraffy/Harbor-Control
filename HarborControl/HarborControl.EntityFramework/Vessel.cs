using HarborControl.Interfaces.Enums;
using HarborControl.Interfaces.Vessels;
using System;

namespace HarborControl.EntityFramework
{
    public sealed class Vessel : IVessel
    {
        public Vessel(IVessel vessel)
        {
            Name = vessel.Name;
            Speed = vessel.Speed;
            Type = vessel.Type;
            Location = vessel.Location;
            Direction = vessel.Direction;
            Arrival = vessel.Arrival;
            TransitStart = vessel.TransitStart;
        }

        public int Id { get; set; }

        public string Name { get; init; }

        public int Speed { get; set; }

        public VesselType Type { get; set; }

        public Location Location { get; set; }

        public Direction Direction { get; init; }

        public DateTime Arrival { get; init; }

        public DateTime? TransitStart { get; set; }
    }
}