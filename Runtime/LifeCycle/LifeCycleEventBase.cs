using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

namespace Hirame.Pantheon
{
    public abstract class LifeCycleEventBase : MonoBehaviour
    {
        [SerializeField, Min (0)] protected float delay;
        [SerializeField] protected UnityEvent @event;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        protected void RaiseEvent ()
        {
            if (delay > 0)
                StartCoroutine (RaiseEventDelayed ());
            else
                @event.Invoke ();
        }

        private IEnumerator RaiseEventDelayed ()
        {
            yield return new WaitForSeconds (delay);
            @event.Invoke ();
        }
    }

}