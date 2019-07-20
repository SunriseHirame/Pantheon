using Unity.Mathematics;
using UnityEngine;

namespace Hirame.Pantheon
{
    [CreateAssetMenu (menuName = "Hirame/Pantheon/Curve")]
    public sealed class Curve : ScriptableObject
    {
        [SerializeField] private Node[] nodes = new[]
        {
            new Node {Value = 0, Time = 0},
            new Node {Value = 1, Time = 1}
        };
        
        public float Evaluate (float t)
        {
            t = math.clamp (t, 0, 1);
            var _t = 1 - t;
            
            return t;
        }

        [System.Serializable]
        private struct Node
        {
            public float Value;
            public float Time;
        }
    }

}