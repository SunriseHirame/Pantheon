using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Hirame.Pantheon
{
    [System.Serializable]
    public struct IntMinMax
    {
        public int Min;
        public int Max;

        public IntMinMax (int min, int max)
        {
            Min = min;
            Max = max;
        }

        public int GetRandom () => Random.Range (Min, Max);
        
        public Vector2 AsVector2 () => new Vector2 (Min, Max);

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static IntMinMax FromVector2 (in Vector2Int source)
        {
            return new IntMinMax (source.x, source.y);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static IntMinMax Clamped (in IntMinMax minMax, int min, int max)
        {
            return new IntMinMax (math.max (minMax.Min, min), math.min (minMax.Max, max));
        }
    }

}