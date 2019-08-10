using UnityEngine;

namespace Hirame.Pantheon
{
    public class AssetPopUpAttribute : PropertyAttribute
    {
        public System.Type Type { get; private set; }
        
        public AssetPopUpAttribute (System.Type type)
        {
            Type = type;
        }

    }

}