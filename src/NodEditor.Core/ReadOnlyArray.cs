namespace NodEditor.Core
{
    public class ReadOnlyArray<T>
    {
        private readonly T[] _items;
        
        public ReadOnlyArray(T[] items)
        {
            _items = items;
            Count = items.Length;
        }
        
        public int Count { get; }
        public T this[int index] => _items[index];
    }
}