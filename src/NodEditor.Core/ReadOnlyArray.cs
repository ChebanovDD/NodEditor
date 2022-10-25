using System;

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

        public static ReadOnlyArray<T> Empty()
        {
            return EmptyReadOnlyArray.Value;
        }

        private static class EmptyReadOnlyArray
        {
            internal static readonly ReadOnlyArray<T> Value = new(Array.Empty<T>());
        }
    }
}