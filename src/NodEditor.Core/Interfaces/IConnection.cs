namespace NodEditor.Core.Interfaces
{
    public interface IConnection
    {
        bool IsCompatible { get; }
        IInputSocket Input { get; }
        IOutputSocket Output { get; }

        void Remove();
    }
}