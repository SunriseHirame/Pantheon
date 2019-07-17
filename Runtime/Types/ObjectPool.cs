using System.Collections;
using UnityEngine;

namespace Hirame.Pantheon
{
    public sealed class ObjectPool<T> where T : new()
    {
        private readonly FastQueue<T> pool;
        private readonly T proto;
        private readonly bool allowExpansion;
        
        public ObjectPool (T item, int startCapacity, bool allowExpansion)
        {
            proto = item;
            this.allowExpansion = allowExpansion;
            
            pool = new FastQueue<T> (startCapacity);
        }

        public void AddItem (T item)
        {
            if (!pool.HasRoom)
            {
                if (!allowExpansion)
                    return;
                
                pool.Resize (pool.Capacity * 2);
            }
            
            pool.Enqueue (item);
        }
        
        public T GetItem ()
        {
            return pool.Count > 0 ? pool.Dequeue () : default;
        }

        public bool TryGetItem (out T item)
        {
            var hasItems = pool.Count > 0;
            item = hasItems ? pool.Dequeue () : default;
            
            return hasItems;
        }
        
        public void FillWithItems ()
        {
            
        }
    }
    
    public sealed class GameObjectPool<T> where T : Component
    {
        private readonly FastQueue<T> pool;
        private readonly T proto;
        private readonly bool allowExpansion;

        private int trackedObjects;

        public T Proto => proto;
        
        public GameObjectPool (T item, int startCapacity, bool allowExpansion)
        {
            proto = item;
            this.allowExpansion = allowExpansion;
            
            pool = new FastQueue<T> (startCapacity);
        }

        public void AddItem (T item, bool deactivate = true)
        {
            if (!pool.HasRoom)
            {
                if (!allowExpansion)
                    return;
                
                pool.Resize (pool.Capacity * 2);
            }

            trackedObjects++;
            pool.Enqueue (item);

            if (!deactivate)
                return;
            
            var go = item.gameObject;
            if (go.activeSelf)
                go.SetActive (false);
        }
        
        public T GetItem ()
        {
            return pool.Count > 0 ? pool.Dequeue () : default;
        }

        public bool TryGetItem (out T item)
        {
            var hasItems = pool.Count > 0;
            item = hasItems ? pool.Dequeue () : default;
            
            return hasItems;
        }

        public void FillWithItems ()
        {
            FillWithItems (pool.Capacity - trackedObjects);
        }

        public void FillWithItems (int maxItemsToAdd)
        {
            
        }
        
    }

}
