using NodEditor.Core.Exceptions;
using NodEditor.Core.Interfaces;

namespace NodEditor.App.Controllers
{
    internal abstract class SocketController<T> : NodeController<T> where T : ISocket
    {
        public bool HasSocket => Socket != null;
        public T Socket { get; private set; }
        
        protected SocketController(INode node) : base(node)
        {
        }

        public void Add(T socket)
        {
            if (Socket != null)
            {
                throw new SocketAlreadyAddedException();
            }
            
            Socket = socket ?? throw new SocketCanNotBeNullReferenceException();
            ConfigureSocket(socket);
        }
    }
}