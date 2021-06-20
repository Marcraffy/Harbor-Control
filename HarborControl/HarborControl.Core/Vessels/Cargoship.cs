using HarborControl.Interfaces.Enums;
using HarborControl.Interfaces.Vessels;
using System;

namespace HarborControl.Core.Vessels
{
    public class Cargoship : BaseVessel, IVessel
    {
        public new int Speed => 5;
        public new VesselType Type => VesselType.Cargoship;
    }
}