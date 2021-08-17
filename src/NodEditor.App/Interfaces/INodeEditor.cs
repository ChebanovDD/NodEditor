using NodEditor.Core.Interfaces;

namespace NodEditor.App.Interfaces
{
    public interface INodeEditor
    {
        IDataNode NewDataNode<TFactory>() where TFactory : IDataNodeFactory;
        IConnection Connect(IOutputSocket output, IInputSocket input);
        void Disconnect(IConnection connection);
    }
}