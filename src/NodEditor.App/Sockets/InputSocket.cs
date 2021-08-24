using System;
using NodEditor.Core.Interfaces;

namespace NodEditor.App.Sockets
{
    public class InputSocket<TValue> : BaseSocket, IInputSocket
    {
        public TValue Value { get; internal set; }
        public override Type Type => typeof(TValue);
        public IConnection Connection => _connections[0];
        
        public override void ResetValue()
        {
            Value = default;
        }
    }
}