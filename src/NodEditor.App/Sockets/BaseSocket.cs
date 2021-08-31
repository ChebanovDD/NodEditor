using System;
using System.Collections.Generic;
using NodEditor.Core;
using NodEditor.Core.Interfaces;

namespace NodEditor.App.Sockets
{
    public abstract class BaseSocket : NodeElement, ISocket
    {
        protected readonly List<IConnection> _connections = new();
        
        public abstract Type Type { get; }
        public INode Node { get; private set; }
        public bool HasConnections => _connections.Count > 0;
        
        public event EventHandler<IConnection> Connected;
        public event EventHandler<IConnection> Disconnected;
        
        public void SetNode(INode node)
        {
            Node = node;
        }
        
        public void AddConnection(IConnection connection)
        {
            _connections.Add(connection);
            Connected?.Invoke(this, connection);
        }
        
        public void RemoveConnection(IConnection connection)
        {
            _connections.Remove(connection);

            if (_connections.Count == 0)
            {
                ResetValue();
            }
            
            Disconnected?.Invoke(this, connection);
        }

        public abstract void ResetValue();
    }
}