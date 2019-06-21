using System;

namespace Hiramesaurus.Pantheon
{
    [AttributeUsage (AttributeTargets.Field)]
    public class MinMaxAttribute : System.Attribute
    {
        public double Min { get; private set; }
        public double Max { get; private set; }
        
        public MinMaxAttribute (double min, double max)
        {
            Min = min;
            Max = max;
        }
    }

}