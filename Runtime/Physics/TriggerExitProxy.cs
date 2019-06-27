using UnityEngine;

namespace Hirame.Pantheon
{
    public sealed class TriggerExitProxy : MonoBehaviour
    {
        public UventCollider TriggerExit;
        
        private void OnTriggerEnter (Collider other)
        {
            TriggerExit.Invoke (other);
        }
    }

}
