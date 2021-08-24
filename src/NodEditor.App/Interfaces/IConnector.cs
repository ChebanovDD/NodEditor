using NodEditor.Core.Interfaces;

namespace NodEditor.App.Interfaces
{
    public interface IConnector
    {
        IConnection Connect(IOutputSocket output, IInputSocket input);
        void Disconnect(IConnection connection);
    }
}