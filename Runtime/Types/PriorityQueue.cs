using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Hirame.Pantheon
{
    public sealed class PriorityQueue<T> where T : IComparable<T>
    {
        private readonly List<T> data;

        public int Count => data.Count;

        public PriorityQueue ()
        {
            data = new List<T> ();
        }

        public T Dequeue ()
        {
            var item = data[0];

            var lastIndex = data.Count - 1;
            data[0] = data[lastIndex];
            data.RemoveAt (lastIndex);
            
            SortDown ();
            
            return item;
        }

        public void Enqueue (T item)
        {
            data.Add (item);
            var addIndex = data.Count - 1;
            SortUp (addIndex);
        }

        public void Remove (T item)
        {
            var toRemove = data.IndexOf (item);
            if (toRemove == -1)
                return;

            var lastIndex = data.Count - 1;
            if (toRemove == lastIndex)
            {
                data.RemoveAt (lastIndex);
                return;
            }
            
            data[toRemove] = data[lastIndex];
            data.RemoveAt (lastIndex);
            
            SortUp (toRemove);
            SortDown ();
        }

        public T Peek ()
        {
            return data[0];
        }

        private void SortUp (int index)
        {
            while (index > 0)
            {
                var parentIndex = GetParent (index);

                if (data[index].CompareTo (data[parentIndex]) >= 0)
                    break;

                Swap (index, parentIndex);
                index = parentIndex;
            }
        }

        private void SortDown ()
        {
            var index = 0;
            var lastIndex = data.Count - 1;
            
            while (true)
            {
                var childToCheck = LeftChild (index);
                if (childToCheck > lastIndex)
                    break;

                var rightChild = childToCheck + 1;
                if (rightChild <= childToCheck && data[rightChild].CompareTo (data[childToCheck]) < 0)
                    childToCheck = rightChild;

                if (data[index].CompareTo (data[childToCheck]) <= 0)
                    break;
                
                Swap (index, childToCheck);
                index = childToCheck;
            }
        }

        private void Swap (int a, int b)
        {
            var temp = data[a];
            data[a] = data[b];
            data[b] = temp;
        }
        
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        private static int GetParent (int index)
        {
            return (index - 1) / 2;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        private static int LeftChild (int index)
        {
            return index * 2 + 1;
        }
    }

}
