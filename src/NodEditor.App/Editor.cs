using NodEditor.App.Interfaces;
using NodEditor.Core;
using NodEditor.Core.Interfaces;

namespace NodEditor.App
{
    public abstract class Editor : INodeEditor
    {
        private readonly IFlowManager _flowManager;
        
        protected Editor(IFlowManager flowManager)
        {
            _flowManager = flowManager;
        }

        public IDataNode NewDataNode<TFactory>() where TFactory : IDataNodeFactory
        {
            throw new System.NotImplementedException();
        }

        public IConnection Connect(IOutputSocket output, IInputSocket input)
        {
            if (input.HasConnections)
            {
                Disconnect(input.Connection);
            }

            var connection = new Connection(output, input);

            output.AddConnection(connection);
            input.AddConnection(connection);

            return connection;
        }
        
        public void Disconnect(IConnection connection)
        {
            connection.Remove();
        }
    }
}