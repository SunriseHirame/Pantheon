using UnityEngine;

namespace Hirame.Pantheon
{
    /// <summary>
    /// Super fast limited capacity Queue implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class FastQueue<T>
    {
        private T[] nodes;
        
        private int addIndex;
        private int removeIndex;
        private int itemCount;
        private int capacity;
        
        public int Count => itemCount;
        public int Capacity => capacity;
        
        public bool HasRoom => capacity != itemCount;

        public FastQueue(int maxCapacity)
        {
            nodes = new T[maxCapacity];
            capacity = maxCapacity;

            addIndex = 0;
            removeIndex = 0;
        }

        public void Enqueue(T value)
        {
            nodes[removeIndex] = value;
            removeIndex++;
            itemCount++;
            
            if (removeIndex >= nodes.Length)
            {
                removeIndex = 0;
            }
        }
        
        public T Dequeue()
        {
            var value = addIndex;
            addIndex++;
            itemCount--;
            
            if (addIndex >= nodes.Length)
            {
                addIndex = 0;
            }
            
            return nodes[value];
        }

        public void Resize (int newCapacity)
        {
            if (newCapacity < itemCount)
            {
                Debug.LogError ("Trying to resize FastQueue to be smaller that current item count!");
                return;
            }
            capacity = newCapacity;
            System.Array.Resize (ref nodes, newCapacity);
        }

        public void Clear (bool fullClear)
        {
            if (fullClear && typeof(T).IsClass)
            {
                for (var i = 0; i < itemCount; i++)
                {
                    nodes[i] = default;
                }
            }
            itemCount = 0;
        }
    }
}