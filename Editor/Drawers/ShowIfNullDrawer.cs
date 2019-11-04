using UnityEditor;
using UnityEngine;

namespace Hirame.Pantheon.Editor
{
    [CustomPropertyDrawer (typeof (ShowIfNull))]
    public class ShowIfNullDrawer : PropertyDrawer
    {
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.objectReferenceValue != null)
                return;

            EditorGUI.PropertyField (position, property, label, true);
        }

        public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
        {
            return property.objectReferenceValue == null ? base.GetPropertyHeight (property, label) : 0f;
        }
    }

}
