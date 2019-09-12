using System.Linq;
using UnityEngine;

namespace Hirame.Pantheon
{
    [System.Serializable]
    public sealed class WeightedGameObjectArray : WeightedArray<GameObject> { }
    
    [System.Serializable]
    public sealed class WeightedScriptableObjectArray : WeightedArray<ScriptableObject> { }

    
    public class WeightedArray<T> : WeightedArray
    {
        [SerializeField] private T[] items;

        public T this [int index]
        {
            get => items[index];
            set => items[index] = value;
        }

        public T GetRandom ()
        {
            return items[0];
        }
    }

    public abstract class WeightedArray
    {
        [Range (0, 1)]
        [SerializeField] protected float[] weights;
        [SerializeField] protected bool[] locked;

        public void SetWeight (int index, float weight)
        {
            if (locked[index])
                return;
            
            weights[index] = weight;
        }

        public float GetWeight (int index)
        {
            return weights[index];
        }
        
        internal void UpdateWeights ()
        {
            if (weights == null || weights.Length == 0)
                return;
            
            var totalWeight = weights.Sum ();

            if (totalWeight <= 0)
            {
                EqualizeWeights ();
                return;
            }
            
            for (var i = 0; i < weights.Length; i++)
            {
                weights[i] /= totalWeight;
            }
        }
        
        public void EqualizeWeights ()
        {
            var numLocked = 0;
            var lockedWeight = 0f;
            
            for (var i = 0; i < locked.Length; i++)
            {
                var l = locked[i];
                if (!l)
                    continue;
                
                numLocked++;
                lockedWeight += weights[i];
            }

            if (numLocked >= weights.Length)
                return;
            
            var w = (1f - lockedWeight) / (weights.Length - numLocked);

            for (var i = 0; i < weights.Length; i++)
            {
                if (locked[i])
                    continue;
                
                weights[i] = w;
            }
        }
    }
}