using HarborControl.Interfaces.Vessels;
using System.Collections.Generic;

namespace HarborControl.Interfaces.Services
{
    public interface IControlService : IService
    {
        public IList<IVessel> VesselsAtPerimeter { get; }
        public IList<IVessel> VesselsAtHarbor { get; }
        public IVessel VesselInTransit { get; }
    }
}