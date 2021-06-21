using HarborControl.Interfaces.Repositories;
using HarborControl.Interfaces.Vessels;
using System;
using System.Collections.Generic;

namespace HarborControl.EntityFramework
{
    public class VesselRepository : IRepository<IVessel, string>
    {

        public void Create(IVessel entity)
        {

        }

        public void Delete(string id)
        {

        }

        public IVessel GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(IVessel entity)
        {

        }
        public IList<IVessel> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}