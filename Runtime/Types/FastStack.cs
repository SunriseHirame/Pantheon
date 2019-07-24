using System;

namespace Hirame.Pantheon
{
    public class FastStack<T>
    {
        private T[] items;
        private int itemCount;
        private int capacity;

        public FastStack (int startCapacity)
        {
            capacity = startCapacity;
        }

        public void Push (T item)
        {
            if (itemCount == capacity)
                throw new IndexOutOfRangeException ("Capacity reached");
            
            items[itemCount++] = item;
        }

        public T Pop ()
        {
            return itemCount == 0 ? default : items[itemCount--];
        }

        public void Resize (int newCapacity)
        {
            Array.Resize (ref items, newCapacity);
        }
    }

}