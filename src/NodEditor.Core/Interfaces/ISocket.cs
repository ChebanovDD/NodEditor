using System;

namespace NodEditor.Core.Interfaces
{
    public interface ISocket
    {
        Type Type { get; }
        bool HasConnections { get; }
        
        void AddConnection(Connection connection);
        void RemoveConnection(Connection connection);
    }
}