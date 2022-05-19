using NodEditor.Core.Interfaces;

namespace NodEditor.App.Internal.Controllers
{
    internal class InputDataController : SocketsController<IInputSocket>
    {
        private int _validInputsCount;

        public bool AllInputsReady => HasSockets == false || _validInputsCount == Sockets.Length;

        public InputDataController(INode node) : base(node)
        {
        }

        protected override void ConfigureSocket(IInputSocket socket, int index = 0)
        {
            base.ConfigureSocket(socket, index);
            socket.Connected += OnSocketConnected;
            socket.Disconnected += OnSocketDisconnected;
        }

        private void OnSocketConnected(object sender, IConnection connection)
        {
            if (connection.IsCompatible)
            {
                _validInputsCount++;
            }
        }
        
        private void OnSocketDisconnected(object sender, IConnection connection)
        {
            if (connection.IsCompatible)
            {
                _validInputsCount--;
            }
        }
    }
}