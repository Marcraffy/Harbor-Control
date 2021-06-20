using HarborControl.Interfaces.Services;
using System;

namespace HarborControl.Core
{
    public class BaseService : IService
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}