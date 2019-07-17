using System;
using System.Collections.Generic;

namespace Hirame.Pantheon
{
    [AttributeUsage (AttributeTargets.Class, AllowMultiple = false, Inherited =  false)]
    public class AutoGameSystem : Attribute
    {
        internal static List<Type> GameSystemTypes = new List<Type> ();
        
        public AutoGameSystem (Type systemType)
        {
            GameSystemTypes.Add (systemType);
        }
    }

}