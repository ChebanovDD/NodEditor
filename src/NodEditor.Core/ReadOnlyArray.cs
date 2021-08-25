namespace NodEditor.Core
{
    public class ReadOnlyArray<T>
    {
        private readonly T[] _inputs;
        
        public ReadOnlyArray(T[] inputs)
        {
            _inputs = inputs;
            Count = inputs.Length;
        }
        
        public int Count { get; }
        public T this[int index] => _inputs[index];
    }
}