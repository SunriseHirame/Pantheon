using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Hirame.Pantheon.Editor
{
    [CustomPropertyDrawer (typeof (FloatMinMax))]
    public class FloatRangeDrawer : PropertyDrawer
    {
        private GUIContent minLabel = new GUIContent ("Min");
        private GUIContent maxLabel = new GUIContent ("Max");
        
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            var minMax = fieldInfo.GetCustomAttribute<MinMaxAttribute> ();
            position.height = EditorGUIUtility.singleLineHeight;
            
            var contentRect = EditorGUI.PrefixLabel (position, label);

            var minProp = property.FindPropertyRelative ("Min");
            var maxProp = property.FindPropertyRelative ("Max");
            var sliderRect = contentRect;
            
            EditorGUIUtility.labelWidth = 60;
                
            contentRect.width /= 2f;
            EditorGUI.PropertyField (contentRect, minProp, minLabel);

            contentRect.x += contentRect.width;
            EditorGUI.PropertyField (contentRect, maxProp, maxLabel);
            
            if (minMax != null)
            {
                //sliderRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                //DrawMinMax (ref sliderRect, minMax, minProp, maxProp);
            }
        }

        private void DrawMinMax (ref Rect lineRect, MinMaxAttribute minMax, SerializedProperty minProp, SerializedProperty maxProp)
        {
            var sliderRect = lineRect;

            var minValue = minProp.floatValue;
            var maxValue = maxProp.floatValue;

            //var minValue = EditorGUI.FloatField (minField, minProp.floatValue);
            //var maxValue = EditorGUI.FloatField (maxField, maxProp.floatValue);

            EditorGUI.MinMaxSlider (
                sliderRect, ref minValue, ref maxValue, (float) minMax.Min, (float) minMax.Max);

            minProp.floatValue = minValue;
            maxProp.floatValue = maxValue;
        }
    }
}