using NodEditor.Core.Interfaces;

namespace NodEditor.App.Internal.Controllers
{
    internal class OutputFlowController : SocketsController<IOutputFlowSocket>
    {
        public OutputFlowController(INode node) : base(node)
        {
        }
        
        protected override void ConfigureSocket(IOutputFlowSocket socket, int index = 0)
        {
            base.ConfigureSocket(socket, index);
            socket.SocketOpened += OnSocketOpened;
        }
        
        private void OnSocketOpened(object sender, int socketIndex)
        {
            var outputFlow = Sockets[socketIndex];
            if (outputFlow.HasConnections == false)
            {
                return;
            }
            
            if (outputFlow.ConnectionsCount == 1)
            {
                outputFlow.Connections[0].Input.Node.Execute();
                return;
            }

            for (var i = 0; i < outputFlow.ConnectionsCount; i++)
            {
                outputFlow.Connections[i].Input.Node.Execute();
            }
        }
    }
}