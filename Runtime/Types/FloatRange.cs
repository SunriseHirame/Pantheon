using System.Runtime.CompilerServices;
//using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Hiramesaurus.Pantheon
{
    [System.Serializable]
    public struct FloatRange
    {
        public float Min;
        public float Max;

        public float GetRandom () => Random.Range (Min, Max);
        
        public Vector2 AsVector2 () => new Vector2 (Min, Max);

        public FloatRange (float min, float max)
        {
            Min = min;
            Max = max;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static FloatRange FromVector2 (in Vector2 source)
        {
            return new FloatRange(source.x, source.y);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static FloatRange Clamped (FloatRange range, float min, float max)
        {
            return new FloatRange();
            //return new FloatRange (math.max (range.Min, min), math.min (range.Max, max));
        }
    }

}