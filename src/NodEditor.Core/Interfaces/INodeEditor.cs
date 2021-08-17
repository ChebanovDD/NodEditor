namespace NodEditor.Core.Interfaces
{
    public interface INodeEditor
    {
        Connection Connect(IOutputSocket output, IInputSocket input);
        void Disconnect(Connection connection);
    }
}