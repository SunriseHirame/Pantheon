using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Hirame.Pantheon
{
    [StructLayout (LayoutKind.Explicit)]
    [Serializable]
    public struct Benum
    {
        [FieldOffset (0)]
        [SerializeField] private int integerValue;
        
        [FieldOffset (0)]
        private Enum enumValue;

        public Benum (Enum value)
        {
            integerValue = 0;
            enumValue = value;
        }
    }

}