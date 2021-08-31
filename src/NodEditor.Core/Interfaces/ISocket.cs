using System;

namespace NodEditor.Core.Interfaces
{
    public interface ISocket : INodeElement
    {
        Type Type { get; }
        INode Node { get; }
        bool HasConnections { get; }
        
        public event EventHandler<IConnection> Connected;
        public event EventHandler<IConnection> Disconnected;
        
        void SetNode(INode node);
        void AddConnection(IConnection connection);
        void RemoveConnection(IConnection connection);
    }
}