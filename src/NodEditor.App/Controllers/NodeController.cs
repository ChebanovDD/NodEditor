using NodEditor.Core.Interfaces;

namespace NodEditor.App.Controllers
{
    internal abstract class NodeController<T> where T : ISocket
    {
        private readonly INode _node;
        
        protected NodeController(INode node)
        {
            _node = node;
        }
        
        protected virtual void ConfigureSocket(T socket, int index = 0)
        {
            socket.SetNode(_node);
            socket.ElementIndex = index;
            
            socket.Connected += OnSocketConnected;
            socket.Disconnected += OnSocketDisconnected;
        }

        protected virtual void OnSocketConnected(object sender, IConnection connection)
        {
        }

        protected virtual void OnSocketDisconnected(object sender, IConnection connection)
        {
        }
    }
}