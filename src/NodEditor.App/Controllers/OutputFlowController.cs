using NodEditor.Core.Interfaces;

namespace NodEditor.App.Controllers
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
            
            // TODO: Convert Connections to List?
            var connections = outputFlow.Connections;
            
            if (outputFlow.ConnectionsCount == 1)
            {
                connections[0].Input.Node.Execute();
                return;
            }

            for (var i = 0; i < connections.Count; i++)
            {
                connections[i].Input.Node.Execute();
            }
        }
    }
}