using System;
using NodEditor.Core.Interfaces;

namespace NodEditor.App.Sockets
{
    public class InputSocket<TValue> : BaseSocket, IInputSocket
    {
        private bool _hasValue;
        private TValue _value;

        public TValue Value
        {
            get => _value;
            internal set
            {
                _value = value;
                _hasValue = true;
            }
        }
        
        public override Type Type => typeof(TValue);
        public override bool HasValue => _hasValue;
        public IConnection Connection => _connections[0];
        
        protected override void ResetValue()
        {
            _value = default;
            _hasValue = false;
        }
    }
}