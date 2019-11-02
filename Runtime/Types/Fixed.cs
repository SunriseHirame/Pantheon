using System.Runtime.InteropServices;
using UnityEngine;

namespace Hirame.Pantheon
{
    [System.Serializable, StructLayout (LayoutKind.Explicit)]
    public struct Fixed
    {
        [FieldOffset (0)]
        [SerializeField]
        private long rawValue;

        [FieldOffset (0)]
        private int left;

        [FieldOffset (sizeof (int))]
        private int right;

        public int AsInt => left;

        public Fixed (int value)
        {
            rawValue = 0;
            right = 0;
            left = value;
        }

        public static Fixed operator + (Fixed a, Fixed b)
        {
            a.rawValue += b.rawValue;
            return a;
        }

        public static Fixed operator - (Fixed a, Fixed b)
        {
            a.rawValue -= b.rawValue;
            return a;
        }

        public static Fixed operator * (Fixed a, in Fixed b)
        {
            a.rawValue *= b.rawValue;
            a.rawValue >>= 16;
            return a;
        }

        public static Fixed operator / (Fixed a, in Fixed b)
        {
            a.rawValue <<= 16;
            a.rawValue /= b.rawValue;
            return a;
        }

        public override string ToString ()
        {
            return $"{left.ToString ()}.{right.ToString ()}";
        }
    }
}