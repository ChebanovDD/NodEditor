namespace NodEditor.Core.Interfaces
{
    public interface IInputSocket : ISocket
    {
        IConnection Connection { get; }
    }
}