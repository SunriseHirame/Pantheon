using System;
using System.Diagnostics;

namespace Hirame.Pantheon
{
    [AttributeUsage (AttributeTargets.Field), Conditional ("UNITY_EDITOR")] 
    public class MinMaxAttribute : Attribute
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