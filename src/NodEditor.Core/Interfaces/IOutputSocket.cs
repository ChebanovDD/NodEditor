using System.Collections.Generic;

namespace NodEditor.Core.Interfaces
{
    public interface IOutputSocket : ISocket
    {
        IReadOnlyList<Connection> Connections { get; }
    }
}