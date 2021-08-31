using System;

namespace NodEditor.Core.Exceptions
{
    public class SocketAlreadyAddedException : Exception
    {
        public SocketAlreadyAddedException()
            : base("Cannot add socket twice.")
        {
        }
    }
}