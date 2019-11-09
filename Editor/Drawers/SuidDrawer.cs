using UnityEditor;
using UnityEngine;

namespace Hirame.Pantheon.Editor
{
    [CustomPropertyDrawer (typeof (Suid))]
    public class SuidDrawer : PropertyDrawer
    {
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            var suidValue = property.FindPropertyRelative ("fullValue");
            
            position = EditorGUI.PrefixLabel (position, label);
            GUI.Label (position, suidValue.longValue.ToString());
        }
    }

}