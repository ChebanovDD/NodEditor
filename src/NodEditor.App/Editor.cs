using NodEditor.App.Interfaces;
using NodEditor.Core.Interfaces;

namespace NodEditor.App
{
    public abstract class Editor : INodeEditor
    {
        private readonly IConnector _connector;
        private readonly IFlowManager _flowManager;
        
        protected Editor(IFlowManager flowManager, IConnector connector)
        {
            _connector = connector;
            _flowManager = flowManager;
        }
        
        public IConnection Connect(IOutputSocket output, IInputSocket input)
        {
            return _connector.Connect(output, input);
        }
        
        public void Disconnect(IConnection connection)
        {
            _connector.Disconnect(connection);
        }
    }
}