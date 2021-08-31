using System;

namespace NodEditor.Core.Interfaces
{
    public interface IOutputFlowSocket : IOutputSocket
    {
        event EventHandler<int> SocketOpened;
        
        void Open();
    }
}