using System;
using System.Collections.Generic;
using NodEditor.Core;
using NodEditor.Core.Interfaces;

namespace NodEditor.App.Sockets
{
    public abstract class BaseSocket : ISocket
    {
        protected readonly List<Connection> _connections = new();

        public abstract Type Type { get; }
        public bool HasConnections => _connections.Count > 0;
        
        public void AddConnection(Connection connection)
        {
            _connections.Add(connection);
        }

        public void RemoveConnection(Connection connection)
        {
            _connections.Remove(connection);
        }
    }
}