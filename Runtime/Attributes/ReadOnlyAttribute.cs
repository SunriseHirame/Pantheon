using System;
using System.Diagnostics;
using UnityEngine;

namespace Hirame.Pantheon
{
    [AttributeUsage (AttributeTargets.Field)]
    [Conditional ("UNITY_EDITOR")]
    public class ReadOnlyAttribute : PropertyAttribute
    {
        
    }

}
