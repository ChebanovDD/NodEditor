using System;

namespace NodEditor.Core.Interfaces
{
    public interface ISocket : INodeElement
    {
        Type Type { get; }
        INode Node { get; }
        bool HasValue { get; }
        bool HasConnections { get; }
        
        public event EventHandler<IConnection> Connected;
        public event EventHandler<IConnection> Disconnected;
        public event EventHandler<IConnection> Disconnecting;
        
        void SetNode(INode node);
        void AddConnection(IConnection connection);
        void RemoveConnection(IConnection connection);
    }
}