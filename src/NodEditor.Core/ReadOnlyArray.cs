using NodEditor.Core.Interfaces;

namespace NodEditor.Core
{
    public class ReadOnlyArray
    {
        private readonly IInputSocket[] _inputs;
        
        public ReadOnlyArray(IInputSocket[] inputs)
        {
            _inputs = inputs;
            Count = inputs.Length;
        }
        
        public int Count { get; }
        public IInputSocket this[int index] => _inputs[index];
    }
}