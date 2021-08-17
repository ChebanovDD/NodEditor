using System;
using NodEditor.App.Extensions;
using NodEditor.Core.Interfaces;

namespace NodEditor.App.Sockets
{
    public class InputSocket<TValue> : BaseSocket, IInputSocket
    {
        public override Type Type => typeof(TValue);
        public IConnection Connection => _connections[0];
        
        public TValue GetValue()
        {
            return Connection.Output.GetValue<TValue>();
        }
    }
}