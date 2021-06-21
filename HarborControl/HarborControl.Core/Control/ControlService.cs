using HarborControl.Core.Exceptions;
using HarborControl.Core.Vessels;
using HarborControl.Interfaces.Enums;
using HarborControl.Interfaces.Repositories;
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
        private readonly IRepository<IVessel, string> vesselRepository;
        private readonly Queue<IVessel> vessels;

        public ControlService(IClockService clockService,
                                IWeatherService weatherService,
                                IRepository<IVessel, string> context)
        {
            this.clockService = clockService;
            this.weatherService = weatherService;
            this.vesselRepository = context;
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

            var isQueueEmpty = vessels.Count == 0;

            var newLocation = isQueueEmpty ? Location.Transit : location;
            var direction = location == Location.Harbor ? Direction.ToPerimeter : Direction.ToHarbor;
            var transitStartTime = isQueueEmpty ? clockService.CurrentTime : (DateTime?)null;

            IVessel vessel = type switch
            {
                VesselType.Cargoship => new Cargoship { 
                    Name = name, 
                    Direction = direction, 
                    Location = newLocation,
                    Arrival = clockService.CurrentTime,
                    TransitStart = transitStartTime
                },
                VesselType.Sailboat => new Sailboat
                {
                    Name = name,
                    Direction = direction,
                    Location = newLocation,
                    Arrival = clockService.CurrentTime,
                    TransitStart = transitStartTime
                },
                VesselType.Speedboat => new Speedboat
                {
                    Name = name,
                    Direction = direction,
                    Location = newLocation,
                    Arrival = clockService.CurrentTime,
                    TransitStart = transitStartTime
                },
                _ => throw new ControlException("Vessel type not found")
            };

            vessels.Enqueue(vessel);
            vesselRepository.Create(vessel);
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
                    DequeueTransitVessel(name);
                    break;

                case Location.Perimeter:
                    DequeueVessel(name);
                    break;

                default:
                    throw new ControlException("Location not found");
            }
            vesselRepository.Delete(name);
        }

        private void DequeueTransitVessel(string name)
        {
            DequeueVessel(name);

            if (vessels.Count == 0)
            {
                return;
            }

            if (vessels.Peek().Type == VesselType.Sailboat
                && (weatherService.WindSpeed < 10f || weatherService.WindSpeed > 30f))
            { 
                var vessel = vessels.FirstOrDefault(vessel => vessel.Type != VesselType.Sailboat);
                if (vessel == null)
                {
                    return;
                }

                DequeueVessel(vessel.Name);
                SetTransitVessel(vessel);
            }

            SetTransitVessel(vessels.Dequeue());
        }

        private void SetTransitVessel(IVessel vessel)
        {
            vessel.Location = Location.Transit;
            vessel.TransitStart = clockService.CurrentTime;

            vessels.Enqueue(vessel);
            vesselRepository.Update(vessel);
        }

        private void DequeueVessel(string name)
        {
            if (StringEquals(vessels.Peek().Name, name))
            {
                vessels.Dequeue();
                return;
            }

            var newVesselQueue = new Queue<IVessel>(vessels.Count - 1);
            do
            {
                var vessel = vessels.Dequeue();
                if (StringEquals(vessel.Name, name))
                {
                    continue;
                }

                newVesselQueue.Enqueue(vessel);
            } while (vessels.Count != 0);
            foreach (var vessel in newVesselQueue)
            {
                vessels.Enqueue(vessel);
            }
        }

        private static bool StringEquals(string a, string b)
            => string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
    }
}