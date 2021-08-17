using System;

namespace NodEditor.Core.Exceptions
{
    public class SocketNullReferenceException : Exception
    {
        public SocketNullReferenceException()
            : base($"Socket can not be null.")
        {
        }
    }
}