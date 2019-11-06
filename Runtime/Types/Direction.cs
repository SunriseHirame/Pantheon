using UnityEngine;

namespace Hirame.Pantheon
{
    public enum AxialDirection
    {
        Left,
        Right,
        Down,
        Up,
        Back,
        Front,
        Invalid
    }

    public static class AxialDirectionExtensions
    {
        private static readonly Vector3[] DirectionVectors =
        {
            new Vector3 (-1, 0, 0),
            new Vector3 (1, 0, 0),
            new Vector3 (0, -1, 0),
            new Vector3 (0, 1, 0),
            new Vector3 (0, 0, -1),
            new Vector3 (0, 0, 1),
            new Vector3 (0, 0, 0),
        };
        
        public static ref readonly Vector3 GetDirectionVector (this AxialDirection axialDirection)
        {
            return ref DirectionVectors[(int) axialDirection];
        }
    }

}