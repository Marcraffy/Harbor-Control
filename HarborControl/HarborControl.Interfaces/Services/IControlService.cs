using HarborControl.Interfaces.Enums;
using HarborControl.Interfaces.Vessels;
using System.Collections.Generic;

namespace HarborControl.Interfaces.Services
{
    public interface IControlService : IService
    {
        public IList<IVessel> VesselsAtPerimeter { get; }
        public IList<IVessel> VesselsAtHarbor { get; }
        public IVessel VesselInTransit { get; }

        public void VesselArrived(string name, Location location, VesselType type);
        public void VesselDeptarted(string name);
    }
}