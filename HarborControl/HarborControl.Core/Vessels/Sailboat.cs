using HarborControl.Interfaces.Enums;
using HarborControl.Interfaces.Vessels;
using System;

namespace HarborControl.Core.Vessels
{
    public class Sailboat : BaseVessel, IVessel
    {
        public new int Speed => 15;
        public new VesselType Type => VesselType.Cargoship;
    }
}