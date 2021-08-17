using System;
using System.Collections.Generic;
using NodEditor.Core;
using NodEditor.Core.Interfaces;

namespace NodEditor.App.Sockets
{
    public class OutputSocket<TValue> : BaseSocket, IOutputSocket
    {
        private TValue _value;

        public override Type Type => typeof(TValue);
        public IReadOnlyList<Connection> Connections => _connections;

        public OutputSocket()
        {
        }

        public OutputSocket(TValue value)
        {
            _value = value;
        }

        public TValue GetValue()
        {
            return _value;
        }

        public void SetValue(TValue value)
        {
            _value = value;
        }
    }
}