using System.Diagnostics;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Hirame.Pantheon.Editor
{
    [CustomPropertyDrawer (typeof (FloatMinMax))]
    public class FloatRangeDrawer : PropertyDrawer
    {
        private SerializedProperty cachedProperty;
        private MinMaxAttribute minMax;
        
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            if (!property.Equals (cachedProperty))
            {
                cachedProperty = property;
                minMax = fieldInfo.GetCustomAttribute<MinMaxAttribute> ();
            }

            var rect = EditorGUI.PrefixLabel (position, label);
            
            var minProp = property.FindPropertyRelative ("Min");
            var maxProp = property.FindPropertyRelative ("Max");
            
            if (minMax != null)
            {
                DrawMinMax (ref rect, minProp, maxProp);
            }
            else
            {
                var subLabelRect = rect;
                subLabelRect.width = 40;
                subLabelRect.y += 2;
                
                EditorGUI.LabelField (subLabelRect, "Min");
                
                subLabelRect.y += 2;
                subLabelRect.x += rect.width / 2f;
                
                EditorGUI.LabelField (subLabelRect, "Max");
            }
        }

        private void DrawMinMax (ref Rect lineRect, SerializedProperty minProp, SerializedProperty maxProp)
        {
            var minField = lineRect;
            minField.y += 2;
            minField.width = 40;
            
            var maxField = lineRect;
            maxField.y += 2;
            maxField.x += lineRect.width - 40;
            maxField.width = 40;
            
            var sliderRect = lineRect;
            sliderRect.y += 2;
            sliderRect.x += 48;
            sliderRect.width -= 96;
            
            var minValue = EditorGUI.FloatField (minField, minProp.floatValue);
            var maxValue = EditorGUI.FloatField (maxField, maxProp.floatValue);

            EditorGUI.MinMaxSlider (sliderRect, ref minValue, ref maxValue, (float) minMax.Min, (float) minMax.Max);

            minProp.floatValue = minValue;
            maxProp.floatValue = maxValue;
        }

        public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
        {
            return minMax == null ? EditorGUIUtility.singleLineHeight : EditorGUIUtility.singleLineHeight * 2f;
        }
        
    }

}
