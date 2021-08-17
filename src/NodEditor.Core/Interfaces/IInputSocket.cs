namespace NodEditor.Core.Interfaces
{
    public interface IInputSocket : ISocket
    {
        Connection Connection { get; }
    }
}