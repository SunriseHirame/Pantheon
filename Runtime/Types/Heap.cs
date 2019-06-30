using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Hirame.Pantheon
{
    public class Heap<T> : IReadOnlyList<T>
    {
        private readonly HeapItem<T>[] items;
        private int itemCount;
        
        public int Capacity => items.Length;

        public int Count => itemCount;

        public T this [int index]
        {
            get
            {
                if (index < 0 || index >= itemCount)
                    throw new IndexOutOfRangeException();
                return items[index].Item;
            }
        }

        public Heap (int maxCapacity)
        {
            items = new HeapItem<T>[maxCapacity];
            itemCount = 0;
        }

        public HeapItem<T> Push (T itemToAdd, int priority)
        {
            var item = new HeapItem<T> (itemToAdd, priority);
            items[itemCount] = item;
            return item;
        }

        public T Pop ()
        {
            var item = items[0];
            
            itemCount--;
            items[0] = items[itemCount];
            items[0].Index = 0;
            
            SortDown (0);
            
            return item.Item;
        }

        public void UpdateItem (HeapItem<T> item)
        {
            
        }
        
        private void SortUp ()
        {
            
        }

        private void SortDown (int index)
        {
            
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        private void Swap (int indexA, int indexB)
        {
            var item = items[indexA];
            items[indexA] = items[indexB];
            items[indexB] = item;
        }
        
        public IEnumerator<T> GetEnumerator ()
        {
            throw new System.NotImplementedException ();
        }

        IEnumerator IEnumerable.GetEnumerator ()
        {
            return GetEnumerator ();
        }

    }
    
    public class HeapItem<T>
    {
        public readonly T Item;
        public readonly int Priority;
        public int Index;
            
        public HeapItem (T item, int priority, int index = 0)
        {
            Item = item;
            Priority = priority;
            Index = index;
        }
    }

}
