using System;
using NodEditor.Core.Interfaces;

namespace NodEditor.App.Sockets
{
    public class OutputFlowSocket : OutputSocket<bool>, IOutputFlowSocket
    {
        public event EventHandler<int> SocketOpened;

        public void Open()
        {
            SocketOpened?.Invoke(this, ElementIndex);
        }
    }
}