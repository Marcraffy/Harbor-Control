using HarborControl.Core.Vessels;
using HarborControl.Interfaces.Enums;
using HarborControl.Interfaces.Repositories;
using HarborControl.Interfaces.Vessels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HarborControl.EntityFramework
{
    public class VesselRepository : IRepository<IVessel, string>
    {
        private readonly HarborControlContext context;

        public VesselRepository(HarborControlContext context)
        {
            this.context = context;
        }

        public void Create(IVessel entity)
        {
            if (context.Vessels.Any(vessel => vessel.Name == entity.Name))
            {
                throw new ArgumentException("Name cannot match an existing name");
            }

            var newVessel = new Vessel(entity);

            context.Vessels.Add(newVessel);
            context.SaveChanges();
        }

        public void Delete(string id)
        {
            if (!context.Vessels.Any(vessel => StringEquals(vessel.Name, id)))
            {
                throw new ArgumentException("Name must match an existing name");
            }

            var vessel = context.Vessels.First(vessel => StringEquals(vessel.Name, id));

            context.Vessels.Remove(vessel);
            context.SaveChanges();
        }

        public IVessel GetById(string id)
        {
            if (!context.Vessels.Any(vessel => StringEquals(vessel.Name, id)))
            {
                throw new ArgumentException("Name must match an existing name");
            }

            var vessel = context.Vessels.First(vessel => StringEquals(vessel.Name, id));
            IVessel output = vessel.Type switch
            {
                VesselType.Cargoship => new Cargoship
                {
                    Name = vessel.Name,
                    Location = vessel.Location,
                    Arrival = vessel.Arrival,
                    Direction = vessel.Direction,
                    TransitStart = vessel.TransitStart
                },
                VesselType.Sailboat => new Sailboat
                {
                    Name = vessel.Name,
                    Location = vessel.Location,
                    Arrival = vessel.Arrival,
                    Direction = vessel.Direction,
                    TransitStart = vessel.TransitStart
                },
                VesselType.Speedboat => new Speedboat
                {
                    Name = vessel.Name,
                    Location = vessel.Location,
                    Arrival = vessel.Arrival,
                    Direction = vessel.Direction,
                    TransitStart = vessel.TransitStart
                },
                _ => throw new ArgumentException("Unexpected Enum for VesselType"),
            };
            return output;
        }

        public void Update(IVessel entity)
        {
            if (!context.Vessels.Any(vessel => vessel.Name == entity.Name))
            {
                throw new ArgumentException("Name must match an existing name");
            }

            var vessel = context.Vessels.First(vessel => vessel.Name == entity.Name);

            vessel.Location = vessel.Location;
            vessel.TransitStart = vessel.TransitStart;

            context.Update(vessel);
            context.SaveChanges();
        }

        public IList<IVessel> GetAll()
        {
            return context.Vessels.Select(vessel => vessel as IVessel).ToList();
        }

        private static bool StringEquals(string a, string b)
            => string.Equals(a, b, StringComparison.OrdinalIgnoreCase);

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}