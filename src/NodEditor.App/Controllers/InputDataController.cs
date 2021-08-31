using NodEditor.Core.Interfaces;

namespace NodEditor.App.Controllers
{
    internal class InputDataController : SocketsController<IInputSocket>
    {
        private int _validInputsCount;

        public bool AllInputsReady => _validInputsCount == Sockets.Count;
        
        public InputDataController(INode node) : base(node)
        {
        }

        protected override void OnSocketConnected(object sender, IConnection connection)
        {
            if (connection.IsCompatible)
            {
                _validInputsCount++;
            }
        }
        
        protected override void OnSocketDisconnected(object sender, IConnection connection)
        {
            if (connection.IsCompatible)
            {
                _validInputsCount--;
            }
        }
    }
}