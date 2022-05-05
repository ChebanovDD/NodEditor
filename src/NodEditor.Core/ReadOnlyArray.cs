namespace NodEditor.Core
{
    public class ReadOnlyArray<T>
    {
        private readonly T[] _items;
        
        public ReadOnlyArray(T[] items)
        {
            _items = items;
            Length = items.Length;
        }
        
        public int Length { get; }
        public T this[int index] => _items[index];
    }
}