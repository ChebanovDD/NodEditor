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
        }
    }
}