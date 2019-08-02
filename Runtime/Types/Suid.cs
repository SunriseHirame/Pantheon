using System;
using UnityEngine;
using Random = System.Random;

namespace Hirame.Pantheon
{
	[Serializable]
	public struct Suid : IEquatable<Suid>
    {
        private static readonly Random prng = new Random ();
        private static readonly DateTime originDate = new DateTime (637001154690000000);

        [SerializeField, HideInInspector]
        private byte
            b1, b2, b3, b4,
            b5, b6, b7, b8;

        private Suid (
            byte b1, byte b2, byte b3, byte b4,
            byte b5, byte b6, byte b7, byte b8)
        {
            this.b1 = b1;
            this.b2 = b2;
            this.b3 = b3;
            this.b4 = b4;

            this.b5 = b5;
            this.b6 = b6;
            this.b7 = b7;
            this.b8 = b8;
        }
        
        public static Suid CreateNew ()
        {
            var random = (uint) (prng.NextDouble () * uint.MaxValue);
            var time = DateTime.Now.Ticks - originDate.Ticks;

            var r1 = (byte) (random >> 0);
            var r2 = (byte) (random >> 4);
            var r3 = (byte) (random >> 8);
            var r4 = (byte) (random >> 12);
            
            var t1 = (byte) (time >> 0);
            var t2 = (byte) (time >> 4);
            var t3 = (byte) (time >> 8);
            var t4 = (byte) (time >> 12);

            return new Suid (r1, r2, r3, r4, t1, t2, t3, t4);
        }
        
        public bool Equals (Suid other)
        {
            return b1 == other.b1
                   && b2 == other.b2
                   && b3 == other.b3
                   && b4 == other.b4
                   && b5 == other.b5
                   && b6 == other.b6
                   && b7 == other.b7
                   && b8 == other.b8;
        }

        public override string ToString ()
        {
            var r = 0;
            r |= b1 << 0;
            r |= b2 << 4;
            r |= b3 << 8;
            r |= b4 << 12;
            
            var t = 0;
            t |= b5 << 0;
            t |= b6 << 4;
            t |= b7 << 8;
            t |= b8 << 12;
            
            return $"{r.ToString("X5")}-{t.ToString("X5")}";
        }
    }

}
