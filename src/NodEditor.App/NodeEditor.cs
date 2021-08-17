using NodEditor.Core;
using NodEditor.Core.Interfaces;

namespace NodEditor.App
{
    public class NodeEditor : INodeEditor
    {
        public Connection Connect(IOutputSocket output, IInputSocket input)
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
        
        public void Disconnect(Connection connection)
        {
            connection.Remove();
        }
    }
}