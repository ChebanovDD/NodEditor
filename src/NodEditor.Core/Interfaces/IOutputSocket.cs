using System.Collections.Generic;

namespace NodEditor.Core.Interfaces
{
    public interface IOutputSocket : ISocket
    {
        int ConnectionsCount { get; }
        IReadOnlyList<IConnection> Connections { get; }
    }
}