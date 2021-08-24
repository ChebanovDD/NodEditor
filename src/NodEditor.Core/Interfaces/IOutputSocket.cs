using System.Collections.Generic;

namespace NodEditor.Core.Interfaces
{
    public interface IOutputSocket : ISocket
    {
        IReadOnlyList<IConnection> Connections { get; }
        void UpdateAllInputValues();
        void UpdateLastInputValue();
    }
}