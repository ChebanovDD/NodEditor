using System;
using System.Collections.Generic;
using NodEditor.Core.Interfaces;

namespace NodEditor.App.Sockets
{
    public class OutputSocket<TValue> : BaseSocket, IOutputSocket
    {
        public TValue Value { get; set; }
        public override Type Type => typeof(TValue);
        public IReadOnlyList<IConnection> Connections => _connections;

        public OutputSocket()
        {
        }

        public OutputSocket(TValue value)
        {
            Value = value;
        }
        
        public void UpdateAllInputValues()
        {
            var count = _connections.Count;
            if (count == 0)
            {
                return;
            }
            
            for (var i = 0; i < count; i++)
            {
                var connection = _connections[i];
                if (connection.IsCompatible)
                {
                    ((InputSocket<TValue>)connection.Input).Value = Value;
                }
            }
        }

        public void UpdateLastInputValue()
        {
            ((InputSocket<TValue>)_connections[^1].Input).Value = Value;
        }
        
        public override void ResetValue()
        {
            Value = default;
        }
    }
}