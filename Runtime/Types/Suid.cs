using System;
using System.Runtime.InteropServices;
using UnityEngine;
using Random = System.Random;

namespace Hirame.Pantheon
{
	[Serializable, StructLayout (LayoutKind.Explicit)]
	public struct Suid : IEquatable<Suid>
    {
        private static readonly Random prng = new Random ();
        private static readonly DateTime originDate = new DateTime (637001154690000000);

        [HideInInspector, FieldOffset (0)]
        [SerializeField] private long fullValue;
        
        [FieldOffset (sizeof (byte) * 0)]
        private readonly byte b0;
        
        [FieldOffset (sizeof (byte) * 1)]
        private readonly byte b1;
        
        [FieldOffset (sizeof (byte) * 2)]
        private readonly byte b2;
        
        [FieldOffset (sizeof (byte) * 3)]
        private readonly byte b3;
        
        [FieldOffset (sizeof (byte) * 4)]
        private readonly byte b4;
        
        [FieldOffset (sizeof (byte) * 5)]
        private readonly byte b5;
        
        [FieldOffset (sizeof (byte) * 6)]
        private readonly byte b6;
        
        [FieldOffset (sizeof (byte) * 7)]
        private readonly byte b7;
           

        private Suid (
            byte b0, byte b1, byte b2, byte b3,
            byte b4, byte b5, byte b6, byte b7)
        {
            fullValue = 0;
            
            this.b0 = b0;
            this.b1 = b1;
            this.b2 = b2;
            this.b3 = b3;

            this.b4 = b4;
            this.b5 = b5;
            this.b6 = b6;
            this.b7 = b7;
        }
        
        public bool IsValid ()
        {
            return fullValue != 0;
        }
        
        public static Suid CreateNew ()
        {
            var random = (uint) (prng.NextDouble () * uint.MaxValue);
            var time = DateTime.Now.Ticks - originDate.Ticks;

            var r1 = (byte) (random);
            var r2 = (byte) (random >> 4);
            var r3 = (byte) (random >> 8);
            var r4 = (byte) (random >> 12);
            
            var t1 = (byte) (time);
            var t2 = (byte) (time >> 4);
            var t3 = (byte) (time >> 8);
            var t4 = (byte) (time >> 12);

            return new Suid (r1, r2, r3, r4, t1, t2, t3, t4);
        }
        
        public bool Equals (Suid other)
        {
            return fullValue == other.fullValue;
        }

        public override string ToString ()
        {
            var r = 0;
            r |= b0;
            r |= b1 << 4;
            r |= b2 << 8;
            r |= b3 << 12;
            
            var t = 0;
            t |= b4;
            t |= b5 << 4;
            t |= b6 << 8;
            t |= b7 << 12;
            
            return $"{r.ToString("X5")}-{t.ToString("X5")}";
        }
    }

}
