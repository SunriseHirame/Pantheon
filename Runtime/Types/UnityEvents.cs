using UnityEngine;
using UnityEngine.Events;
 
namespace Hiramesaurus.Pantheon
{
    [System.Serializable]
    public sealed class UventCollider : UnityEvent<Collider>
    {
    }
    
    [System.Serializable]
    public sealed class UventCollision : UnityEvent<Collision>
    {
    }

    [System.Serializable]
    public sealed class UventGameObject : UnityEvent<GameObject>
    {
    }
    
    [System.Serializable]
    public sealed class UventBoolean : UnityEvent<bool>
    {
    }
}