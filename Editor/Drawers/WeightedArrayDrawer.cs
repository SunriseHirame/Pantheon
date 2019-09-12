using UnityEditor;
using UnityEngine;

namespace Hirame.Pantheon.Editor
{
    [CustomPropertyDrawer (typeof (WeightedArray), true)]
    public class WeightedArrayDrawer : PropertyDrawer
    {
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            var itemsProp = property.FindPropertyRelative ("items");
            var weightsProp = property.FindPropertyRelative ("weights");
            var lockedProp = property.FindPropertyRelative ("locked");

            var labelRect = position;
            labelRect.height = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing * 2f;
            
            var lineRect = position;
            lineRect.height = EditorGUIUtility.singleLineHeight;
            lineRect.y += EditorGUIUtility.standardVerticalSpacing;

            var addRect = labelRect;
            addRect.x = position.width - 84;
            addRect.width = 40;
            
            var clearRect = labelRect;
            clearRect.x = position.width - 42;
            clearRect.width = 54;
            
            GUI.Box (position, GUIContent.none);
            
            GUI.Box (labelRect, GUIContent.none);
            GUI.Label (labelRect, label);

            using (var changeScope = new EditorGUI.ChangeCheckScope ())
            {
                if (GUI.Button (addRect, "+"))
                {
                    AddItem (itemsProp, weightsProp, lockedProp);
                }
            
                if (GUI.Button (clearRect, "Clear"))
                {
                    ClearItems (itemsProp, weightsProp, lockedProp);
                }

                lineRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing * 2f;
                DrawRows (lineRect, itemsProp, weightsProp, lockedProp);

                if (changeScope.changed)
                {
                    property.serializedObject.ApplyModifiedProperties ();

                    var target = fieldInfo.GetValue (property.serializedObject.targetObject) as WeightedArray;
                    target?.UpdateWeights ();

                    property.serializedObject.Update ();
                }
            }
        }

        private static void DrawRows (
            Rect lineRect,
            SerializedProperty items,
            SerializedProperty weights,
            SerializedProperty locked)
        {
            var width = (lineRect.width - 42) / 2f - 18f;
            
            var itemRect = lineRect;
            itemRect.x += 2f;
            itemRect.width = width;

            var weightRect = lineRect;
            weightRect.x = width + 24f;
            weightRect.width = width;

            var lockRect = lineRect;
            lockRect.x += weightRect.x + weightRect.width + 2f;
            lockRect.width = 40;
            
            var lineOffset = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            var isHeader = true;
            
            while (items.NextVisible (true)
                   && weights.NextVisible (true)
                   && locked.NextVisible (true))
            {
                if (isHeader)
                {
                    EditorGUI.LabelField (itemRect, "Item");
                    EditorGUI.LabelField (weightRect, "Weight");
                    EditorGUI.LabelField (lockRect, "Lock");

                    isHeader = false;
                }
                else
                {
                    EditorGUI.PropertyField (itemRect, items, GUIContent.none);
                    EditorGUI.PropertyField (weightRect, weights, GUIContent.none);
                    EditorGUI.PropertyField (lockRect, locked, GUIContent.none);
                }

                lockRect.y += lineOffset;
                itemRect.y += lineOffset;
                weightRect.y += lineOffset;
            }
        }

        private static void AddItem (SerializedProperty items, SerializedProperty weights, SerializedProperty locked)
        {
            var insertPlace = items.arraySize;
            
            items.InsertArrayElementAtIndex (insertPlace);
            weights.InsertArrayElementAtIndex (insertPlace);
            locked.InsertArrayElementAtIndex (insertPlace);
            
            weights.GetArrayElementAtIndex (insertPlace).floatValue = 1f / (insertPlace + 1);
        }
        
        private static void ClearItems (SerializedProperty items, SerializedProperty weights, SerializedProperty locked)
        {
            items.arraySize = 0;
            weights.arraySize = 0;
            locked.arraySize = 0;
        }

        public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
        {
            var itemsProp = property.FindPropertyRelative ("items");
            var propHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            
            return (itemsProp.arraySize + 2) * propHeight + EditorGUIUtility.standardVerticalSpacing * 2f;
        }
    }
}