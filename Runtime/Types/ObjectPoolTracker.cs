using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hirame.Pantheon
{
    public sealed class ObjectPoolTracker<T> : MonoBehaviour where T : Component
    {
        public GameObjectPool<T> Pool;
        public T TrackedComponent;

        public void SetUp (T trackedComponent, GameObjectPool<T> pool)
        {
            Pool = pool;
            TrackedComponent = trackedComponent;
        }
        
        public void ReturnToPool ()
        {
            if (Pool == null || TrackedComponent == false)
                return;
            
            Pool.AddItem (TrackedComponent);
        } 

        public void ReturnToPool (float delay)
        {
            if (delay > 0)
                StartCoroutine (ReturnToPoolAfterDelay (delay));
            else
                ReturnToPool ();
        }

        internal IEnumerator ReturnToPoolAfterDelay (float delay)
        {
            yield return new WaitForSeconds (delay);
            ReturnToPool ();
        }
    }

}