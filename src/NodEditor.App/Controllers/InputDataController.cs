using System;
using NodEditor.Core.Interfaces;

namespace NodEditor.App.Controllers
{
    internal class InputDataController : SocketsController<IInputSocket>
    {
        private bool _readyToExecute;
        private int _validInputsCount;

        public bool AllInputsReady => _validInputsCount == Sockets.Count;
        
        public event EventHandler ReadyToExecute;
        public event EventHandler UnreadyToExecute;
        
        public InputDataController(INode node) : base(node)
        {
        }

        protected override void OnSocketConnected(object sender, IConnection connection)
        {
            if (connection.IsCompatible)
            {
                _validInputsCount++;
            }

            if (_readyToExecute == false && AllInputsReady)
            {
                _readyToExecute = true;
                ReadyToExecute?.Invoke(this, EventArgs.Empty);
            }
        }
        
        protected override void OnSocketDisconnected(object sender, IConnection connection)
        {
            if (connection.IsCompatible)
            {
                _validInputsCount--;
            }

            if (_readyToExecute && AllInputsReady == false)
            {
                _readyToExecute = false;
                UnreadyToExecute?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}