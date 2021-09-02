using System.Collections.Generic;

namespace NodEditor.Core.Interfaces
{
    public interface IOutputSocket : ISocket
    {
        int ConnectionsCount { get; }
        List<IConnection> Connections { get; }
    }
}