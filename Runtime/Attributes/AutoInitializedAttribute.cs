using UnityEngine;

namespace Hirame.Pantheon
{
    public class AutoInitializedAttribute : System.Attribute
    {
        public RuntimeInitializeLoadType InitializeLoadType { get; private set; }
        
        public AutoInitializedAttribute (RuntimeInitializeLoadType initializeLoadType)
        {
            InitializeLoadType = initializeLoadType;
        }
    }

}