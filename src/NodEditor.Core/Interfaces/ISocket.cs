using System;

namespace NodEditor.Core.Interfaces
{
    public interface ISocket
    {
        Type Type { get; }
        bool HasConnections { get; }
        
        void AddConnection(IConnection connection);
        void RemoveConnection(IConnection connection);
    }
}