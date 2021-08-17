using System;
using System.Collections.Generic;
using NodEditor.Core.Interfaces;

namespace NodEditor.App.Sockets
{
    public abstract class BaseSocket : ISocket
    {
        protected readonly List<IConnection> _connections = new();

        public abstract Type Type { get; }
        public bool HasConnections => _connections.Count > 0;
        
        public void AddConnection(IConnection connection)
        {
            _connections.Add(connection);
        }

        public void RemoveConnection(IConnection connection)
        {
            _connections.Remove(connection);
        }
    }
}