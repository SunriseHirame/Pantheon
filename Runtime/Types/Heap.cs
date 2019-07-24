using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Hirame.Pantheon
{
    public class Heap<T> : IReadOnlyList<T> 
        where T : IComparable<T>
    {
        private readonly HeapItem<T>[] items;
        private readonly int[] priorities;

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
            priorities = new int[maxCapacity];
            
            itemCount = 0;
        }

        public void Push (T itemToAdd, int priority)
        {
            var item = new HeapItem<T> (itemToAdd, itemCount);
            
            items[itemCount] = item;
            priorities[itemCount] = priority;
            itemCount++;
        }

        public HeapItem<T> PushAndGet (T itemToAdd, int priority)
        {
            var item = new HeapItem<T> (itemToAdd, itemCount);
           
            items[itemCount] = item;
            priorities[itemCount] = priority;
            itemCount++;
            
            return item;
        }

        public T Pop ()
        {
            var item = items[0];
            
            itemCount--;
            items[0] = items[itemCount];
            priorities[0] = priorities[itemCount];
            
            items[0].Index = 0;
            
            SortDown (item);
            
            return item.Item;
        }

        public void UpdateItem (HeapItem<T> item, int priority)
        {
            priorities[item.Index] = priority;
            SortUp (item);
        }

        public bool Contains (T item)
        {
            for (var i = 0; i < itemCount; i++)
            {
                // TODO: Find non-boxing version
                if (Equals (items[i].Item, item))
                    return true;
            }
            return false;
        }

        public bool Contains (HeapItem<T> item)
        {
            return Equals (item, items[item.Index]);
        }
        
        private void SortDown (HeapItem<T> item)
        {
            while (true)
            {
                var itemIndex = item.Index;
                var leftChildIndex = itemIndex * 2 + 1;
                var rightChildIndex = itemIndex * 2 + 2;


                if (leftChildIndex < itemCount)
                {
                    var swapIndex = leftChildIndex;

                    if (rightChildIndex < itemCount)
                    {
                        var leftPriority = priorities[leftChildIndex];
                        var rightPriority = priorities[rightChildIndex];

                        if (leftPriority < rightPriority)
                        {
                            swapIndex = rightChildIndex;
                        }
                    }

                    if (priorities[itemIndex] < priorities[swapIndex])
                    {
                        Swap (item.Index, swapIndex);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
        }
        
        private void SortUp (HeapItem<T> item)
        {
            var ownPriority = priorities[item.Index];
            
            while (true)
            {
                var parentIndex = GetParentIndex (item.Index);
                var parentPriority = priorities[parentIndex];

                if (ownPriority > parentPriority)
                {
                    Swap (item.Index, parentIndex);
                }
                else
                {
                    break;
                }
            }
        }
        

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        private void Swap (int indexA, int indexB)
        {
            var item = items[indexA];
            items[indexA] = items[indexB];
            items[indexB] = item;

            var priority = priorities[indexA];
            priorities[indexA] = priorities[indexB];
            priorities[indexB] = priorities[priority];
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        private int GetParentIndex (int index)
        {
            return (index - 1) / 2;
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
    
    public sealed class HeapItem<T> : IComparable<HeapItem<T>>
        where T : IComparable<T>
    {
        public readonly T Item;
        public int Index;
            
        public HeapItem (T item, int index = 0)
        {
            Item = item;
            Index = index;
        }

        public int CompareTo (HeapItem<T> other)
        {
            return Item.CompareTo (other.Item);
        }
    }

}
