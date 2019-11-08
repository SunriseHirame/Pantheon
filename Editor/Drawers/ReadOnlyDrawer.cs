using UnityEditor;
using UnityEngine;

namespace Hirame.Pantheon.Editor
{
    [CustomPropertyDrawer (typeof (ReadOnlyAttribute))]
    public class ReadOnlyDrawer : DecoratorDrawer
    {
        public override void OnGUI (Rect position)
        {
            GUI.enabled = false;
        }

        public override float GetHeight ()
        {
            return 0f;
        }

//        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
//        {
//            var guiEnabled = GUI.enabled;
//            GUI.enabled = false;
//            EditorGUI.PropertyField (position, property, label);
//            GUI.enabled = guiEnabled;
//        }
//
//        public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
//        {
//            return EditorGUI.GetPropertyHeight (property, label, true);
//        }
    }

}