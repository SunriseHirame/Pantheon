using UnityEngine;
using UnityEngine.Events;

namespace Hirame.Pantheon
{
    public sealed class TriggerExitProxy : MonoBehaviour
    {
        [SerializeField] private UventCollider triggerExit;
        
        public void AddListener (UnityAction<Collider> listener)
        {
            triggerExit.AddListener (listener);
        }

        public void RemoveListener (UnityAction<Collider> listener)
        {
            triggerExit.RemoveListener (listener);
        }
        
        private void OnTriggerEnter (Collider other)
        {
            triggerExit.Invoke (other);
        }
    }

}
