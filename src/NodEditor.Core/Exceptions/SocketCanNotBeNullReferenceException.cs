using System;

namespace NodEditor.Core.Exceptions
{
    public class SocketCanNotBeNullReferenceException : Exception
    {
        public SocketCanNotBeNullReferenceException()
            : base($"Socket can not be null.")
        {
        }
    }
}