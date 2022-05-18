﻿using NodEditor.App.Interfaces;
using NodEditor.Core.Interfaces;

namespace NodEditor
{
    public class Connector : IConnector
    {
        public IConnection Connect(IOutputSocket output, IInputSocket input)
        {
            if (input.HasConnections)
            {
                Disconnect(input.Connection);
            }
            
            var connection = new Connection(output, input); // TODO: Pooling.
            
            input.AddConnection(connection);
            output.AddConnection(connection);
            
            return connection;
        }

        public void Disconnect(IConnection connection)
        {
            connection.Remove();
        }
    }
}