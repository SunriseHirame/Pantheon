using UnityEngine;
using UnityEngine.Events;

namespace Hirame.Pantheon
{
    public sealed class TriggerEnterProxy : MonoBehaviour
    {
        [SerializeField] private UventCollider triggerEnter;

        public void AddListener (UnityAction<Collider> listener)
        {
            triggerEnter.AddListener (listener);
        }

        public void RemoveListener (UnityAction<Collider> listener)
        {
            triggerEnter.RemoveListener (listener);
        }
        
        private void OnTriggerEnter (Collider other)
        {
            triggerEnter.Invoke (other);
        }
    }

}
