using HarborControl.Interfaces.Enums;
using HarborControl.Interfaces.Vessels;

namespace HarborControl.Core.Vessels
{
    public class Speedboat : BaseVessel, IVessel
    {
        public new int Speed => 30;
        public new VesselType Type => VesselType.Cargoship;
    }
}