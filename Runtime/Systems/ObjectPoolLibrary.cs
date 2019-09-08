using System.Collections.Generic;
using UnityEngine;

namespace Hirame.Pantheon
{
    public static class ObjectPoolLibrary
    {
        private static readonly Dictionary<object, IObjectPool> sharedObjectPools = new Dictionary<object, IObjectPool> ();
        
        public static GameObjectPool<T> GetOrCreateGameObjectPool<T> (T protoObject) where T : Component
        {
            if (!sharedObjectPools.TryGetValue (protoObject, out var pool))
            {
                pool = new GameObjectPool<T> (protoObject, 10, false, true);
                sharedObjectPools.Add (protoObject, pool);
            }
            return (GameObjectPool<T>) pool;
        }
        
        public static ObjectPool<T> GetOrCreateObjectPool<T> (T protoObject) where T : class, new ()
        {
            if (!sharedObjectPools.TryGetValue (protoObject, out var pool))
            {
                pool = new ObjectPool<T> (protoObject, 10, true);
                sharedObjectPools.Add (protoObject, pool);
            }
            return (ObjectPool<T>) pool;
        }
    }

}