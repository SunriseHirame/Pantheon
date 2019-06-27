using System;

namespace Hirame.Pantheon
{
    [AttributeUsage (AttributeTargets.Field)]
    public class MinMaxAttribute : System.Attribute
    {
        public double Min { get; }
        public double Max { get; }
        
        public MinMaxAttribute (double min, double max)
        {
            Min = min;
            Max = max;
        }
    }

}