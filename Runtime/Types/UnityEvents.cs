using UnityEngine;
using UnityEngine.Events;
 
namespace Hirame.Pantheon
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
    public sealed class UventBool : UnityEvent<bool>
    {
    }
    
    [System.Serializable]
    public sealed class UventInt : UnityEvent<int>
    {
    }
    
    [System.Serializable]
    public sealed class UventFloat : UnityEvent<float>
    {
    }
    
}