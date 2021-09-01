namespace NodEditor.Core
{
    public class ReadOnlyArray<T>
    {
        private readonly T[] _items;

        public ReadOnlyArray() : this(null, 0)
        {
        }
        
        public ReadOnlyArray(T[] items) : this(items, items.Length)
        {
        }
        
        private ReadOnlyArray(T[] items, int count)
        {
            _items = items;
            Count = count;
        }
        
        public int Count { get; }
        public T this[int index] => _items[index];
    }
}