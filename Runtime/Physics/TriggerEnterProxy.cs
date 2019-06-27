using UnityEngine;

namespace Hirame.Pantheon
{
    public sealed class TriggerEnterProxy : MonoBehaviour
    {
        public UventCollider TriggerEnter;
        
        private void OnTriggerEnter (Collider other)
        {
            TriggerEnter.Invoke (other);
        }
    }

}
