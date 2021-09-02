using NodEditor.Core.Interfaces;

namespace NodEditor
{
    public class Connection : IConnection
    {
        public bool IsCompatible { get; }
        public IInputSocket Input { get; }
        public IOutputSocket Output { get; }
        
        public Connection(IOutputSocket output, IInputSocket input)
        {
            Output = output;
            Input = input;
            IsCompatible = output.Type == input.Type || input.Type == typeof(object);
        }
        
        public void Remove()
        {
            Input.RemoveConnection(this);
            Output.RemoveConnection(this);
        }
    }
}