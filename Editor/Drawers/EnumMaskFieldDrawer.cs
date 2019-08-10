using UnityEngine;
using UnityEditor;

namespace Hirame.Pantheon.Editor
{
    [CustomPropertyDrawer (typeof (MaskFieldAttribute))]
    public class EnumMaskFieldDrawer : PropertyDrawer
    {
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            using (var scope = new EditorGUI.ChangeCheckScope ())
            {
                var value = (uint) (EditorGUI.MaskField (position, label, property.intValue, property.enumNames));
                if (scope.changed)
                    property.intValue = (int) value;
            }
        }
    }
}