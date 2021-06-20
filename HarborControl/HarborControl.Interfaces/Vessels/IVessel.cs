using HarborControl.Interfaces.Enums;
using System;

namespace HarborControl.Interfaces.Vessels
{
    public interface IVessel
    {
        /// <summary>
        /// Name of vessel
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Speed of vessel in KM/H
        /// </summary>
        public int Speed { get; }

        /// <summary>
        /// Type of vessel
        /// </summary>
        public VesselType Type { get; }

        /// <summary>
        /// Location of vessel relative to harbor
        /// </summary>
        public Location Location { get; set; }

        /// <summary>
        /// Direction of vessel relative to harbor
        /// </summary>
        public Direction Direction { get; init; }

        /// <summary>
        /// Time of Arrival
        /// </summary>
        public DateTime Arrival { get; init; }

        /// <summary>
        /// Start time of Transit
        /// </summary>
        public DateTime? TransitStart { get; set; }
    }
}