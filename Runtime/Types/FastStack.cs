using System;

namespace Hirame.Pantheon
{
    public class FastStack<T>
    {
        private T[] items;
        private int itemCount;
        private int capacity;

        public int Count => itemCount;
        public int Capacity => capacity;

        public bool HasRoom => capacity > itemCount;

        public FastStack (int startCapacity)
        {
            capacity = startCapacity;
            items = new T[startCapacity];
        }

        public void Push (T item)
        {
            if (!HasRoom)
                throw new IndexOutOfRangeException ("Capacity reached");

            items[itemCount++] = item;
        }

        public T Pop ()
        {
            return itemCount == 0 ? default : items[--itemCount];
        }

        public void Resize (int newCapacity)
        {
            Array.Resize (ref items, newCapacity);
        }
    }
}