using HarborControl.Core.Exceptions;
using HarborControl.Core.Vessels;
using HarborControl.Interfaces.Enums;
using HarborControl.Interfaces.Services;
using HarborControl.Interfaces.Vessels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HarborControl.Core.Control
{
    public class ControlService : BaseService, IControlService
    {
        private readonly IClockService clockService;
        private readonly IWeatherService weatherService;
        private readonly Queue<IVessel> vessels;

        public ControlService(IClockService clockService,
                                IWeatherService weatherService)
        {
            this.clockService = clockService;
            this.weatherService = weatherService;
            vessels = new Queue<IVessel>();
        }

        public IList<IVessel> VesselsAtPerimeter => vessels.Where(vessel => vessel.Location == Location.Perimeter).ToList();
        public IList<IVessel> VesselsAtHarbor => vessels.Where(vessel => vessel.Location == Location.Harbor).ToList();
        public IVessel VesselInTransit => vessels.SingleOrDefault(vessel => vessel.Location == Location.Transit);

        public void VesselArrived(string name, Location location, VesselType type)
        {
            if (location == Location.Transit)
            {
                throw new ControlException("Vessel cannot arrive in transit");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ControlException("Vessel must have name");
            }

            if (vessels.Any(vessel => string.Equals(vessel.Name, name, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ControlException("Vessel is already in queue");
            }

            var direction = location == Location.Harbor ? Direction.ToPerimeter : Direction.ToHarbor;

            IVessel vessel = type switch
            {
                VesselType.Cargoship => new Cargoship { 
                    Name = name, 
                    Direction = direction, 
                    Location = location,
                    Arrival = clockService.CurrentTime 
                },
                VesselType.Sailboat => new Sailboat
                {
                    Name = name,
                    Direction = direction,
                    Location = location,
                    Arrival = clockService.CurrentTime
                },
                VesselType.Speedboat => new Speedboat
                {
                    Name = name,
                    Direction = direction,
                    Location = location,
                    Arrival = clockService.CurrentTime
                },
                _ => throw new ControlException("Vessel type not found")
            };

            vessels.Enqueue(vessel);
        }

        public void VesselDeptarted(string name)
        {
            if (vessels.Count == 0)
            {
                throw new ControlException("Vessel not in queue");
            }

            var vessel = vessels.SingleOrDefault(vessel => StringEquals(vessel.Name, name));
            if (vessel == null)
            {
                throw new ControlException("Vessel not in queue");
            }

            switch (vessel.Location)
            {
                case Location.Harbor:
                    DequeueVessel(name);
                    break;

                case Location.Transit:
                    DequeueVessel(name);
                    var nextVessel = vessels.Dequeue();
                    nextVessel.Location = Location.Transit;
                    nextVessel.TransitStart = clockService.CurrentTime;
                    vessels.Enqueue(nextVessel);
                    break;

                case Location.Perimeter:
                    DequeueVessel(name);
                    break;

                default:
                    throw new ControlException("Location not found");
            }
        }

        private void DequeueVessel(string name)
        {
            if (StringEquals(vessels.Peek().Name, name))
            {
                vessels.Dequeue();
                return;
            }

            var newVesselQueue = new Queue<IVessel>(vessels.Count - 1);
            foreach (var vessel in vessels)
            {
                if (StringEquals(vessel.Name, name))
                {
                    vessels.Dequeue();
                    continue;
                }

                newVesselQueue.Enqueue(vessels.Dequeue());
            }
        }

        private static bool StringEquals(string a, string b)
            => string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
    }
}