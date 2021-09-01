﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NodEditor.Core.Interfaces;

namespace NodEditor.App.Sockets
{
    public class OutputSocket<TValue> : BaseSocket, IOutputSocket
    {
        private bool _hasValue;
        private TValue _value;

        public TValue Value
        {
            get => _value;
            set
            {
                _value = value;
                _hasValue = true;
                SetAllConnectionsValue(value);
            }
        }
        
        public override Type Type => typeof(TValue);
        public override bool HasValue => _hasValue;
        public int ConnectionsCount => _connections.Count;
        public IReadOnlyList<IConnection> Connections => _connections;

        public OutputSocket()
        {
        }
        
        public OutputSocket(TValue value)
        {
            _value = value;
        }

        public override void ResetValue()
        {
            _value = default;
            _hasValue = false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetAllConnectionsValue(TValue value)
        {
            if (_connections.Count == 1)
            {
                ((InputSocket<TValue>)_connections[0].Input).Value = value;
                return;
            }

            for (var i = 0; i < _connections.Count; i++)
            {
                ((InputSocket<TValue>)_connections[i].Input).Value = value;
            }
        }
    }
}