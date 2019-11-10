using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Hirame.Pantheon
{
    [System.Serializable]
    public struct FloatMinMax
    {
        public float Min;
        public float Max;
        
        public static FloatMinMax Default => new FloatMinMax (0, 1);
        
        public FloatMinMax (float min, float max)
        {
            Min = min;
            Max = max;
        }
        
        public float GetRandom () => Random.Range (Min, Max);
        
        public Vector2 AsVector2 () => new Vector2 (Min, Max);

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static FloatMinMax FromVector2 (in Vector2 source)
        {
            return new FloatMinMax(source.x, source.y);
        }
    }
    
    public static class FloatRangeExtensions
    {
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static FloatMinMax Clamped (in this FloatMinMax minMax, float min, float max)
        {
            return new FloatMinMax (math.max (minMax.Min, min), math.min (minMax.Max, max));
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static float Remap (this in FloatMinMax minMax, float t)
        {
            return minMax.Min + (minMax.Max - minMax.Min) * t;
        }
    }

}