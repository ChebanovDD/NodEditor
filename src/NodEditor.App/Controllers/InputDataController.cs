using NodEditor.Core.Interfaces;

namespace NodEditor.App.Controllers
{
    internal class InputDataController : SocketsController<IInputSocket>
    {
        private bool _allInputsConnected = true;

        public bool AllInputsConnected => _allInputsConnected;
        
        public InputDataController(INode node) : base(node)
        {
        }

        protected override void OnSocketConnected(object sender, IConnection connection)
        {
            _allInputsConnected = AllInputsReady();
        }
        
        protected override void OnSocketDisconnected(object sender, IConnection connection)
        {
            _allInputsConnected = false;
        }
        
        private bool AllInputsReady()
        {
            for (var i = 0; i < Sockets.Count; i++)
            {
                var input = Sockets[i];
                if (input.HasConnections == false)
                {
                    return false;
                }
                
                if (input.Connection.IsCompatible == false)
                {
                    return false;
                }
            }
            
            return true;
        }
    }
}