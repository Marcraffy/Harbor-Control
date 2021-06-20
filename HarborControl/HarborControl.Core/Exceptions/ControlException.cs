using System;

namespace HarborControl.Core.Exceptions
{
    public class ControlException : Exception
    {
        public ControlException(string message) : base(message)
        {
        }
    }
}