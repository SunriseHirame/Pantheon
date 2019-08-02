using Unity.Mathematics;
using UnityEngine;

namespace Hirame.Pantheon
{
    public sealed class ObjectPool<T> where T : new()
    {
        private readonly FastStack<T> pool;
        private readonly T proto;
        private readonly bool allowExpansion;
        
        public ObjectPool (T item, int startCapacity, bool allowExpansion)
        {
            proto = item;
            this.allowExpansion = allowExpansion;
            
            pool = new FastStack<T> (startCapacity);
        }

        public void AddItem (T item)
        {
            if (!pool.HasRoom)
            {
                if (!allowExpansion)
                    return;
                
                pool.Resize (pool.Capacity * 2);
            }
            
            pool.Push (item);
        }
        
        public T GetItem ()
        {
            return pool.Count > 0 ? pool.Pop () : default;
        }

        public bool TryGetItem (out T item)
        {
            var hasItems = pool.Count > 0;
            item = hasItems ? pool.Pop () : default;
            
            return hasItems;
        }
        
        public void FillWithItems ()
        {
            
        }
    }
    
    public sealed class GameObjectPool<T> where T : Component
    {
        private readonly FastStack<T> pool;
        private readonly T proto;
        private readonly bool allowExpansion;

        private int trackedObjects;

        public T Proto => proto;
        
        public GameObjectPool (T item, int startCapacity, bool allowExpansion)
        {
            proto = item;
            this.allowExpansion = allowExpansion;
            
            pool = new FastStack<T> (startCapacity);
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
            pool.Push (item);

            if (!deactivate)
                return;
            
            var go = item.gameObject;
            if (go.activeSelf)
                go.SetActive (false);
        }
        
        public T GetItem ()
        {
            return pool.Count > 0 ? pool.Pop () : default;
        }

        public bool TryGetItem (out T item)
        {
            var hasItems = pool.Count > 0;
            item = hasItems ? pool.Pop () : default;
            
            return hasItems;
        }

        public void FillWithItems ()
        {
            FillWithItems (pool.Capacity);
        }

        public void FillWithItems (int maxItemsToAdd)
        {
            maxItemsToAdd = math.clamp (maxItemsToAdd, 0, pool.Capacity - trackedObjects);

            for (var i = 0; i < maxItemsToAdd; i++)
            {
                var item = Object.Instantiate (proto);
                var itemGo = item.gameObject;
                
                itemGo.SetActive (false);
                Object.DontDestroyOnLoad (itemGo);
                
                pool.Push (item);
            }
        }
        
    }

}
