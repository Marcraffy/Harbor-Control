using System;

namespace HarborControl.Core.Exceptions
{
    public class ClockException : Exception
    {
        public ClockException(string message) : base(message)
        {
        }
    }
}