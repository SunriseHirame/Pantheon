using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Hiramesaurus.Pantheon.Editor
{
    [CustomPropertyDrawer (typeof (FloatRange))]
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
            rect.x -= 16;
            
            var minProp = property.FindPropertyRelative ("Min");
            var maxProp = property.FindPropertyRelative ("Max");
            
            if (minMax != null)
            {
                DrawMinMax (rect, minProp, maxProp);
            }
            else
            {
                var subLabelRect = rect;
                subLabelRect.width = 40;
                EditorGUI.LabelField (subLabelRect, "Min");
                subLabelRect.x += rect.width / 2f;
                EditorGUI.LabelField (subLabelRect, "Max");
            }
        }

        private void DrawMinMax (Rect contentRect, SerializedProperty minProp, SerializedProperty maxProp)
        {
            var subLabelRect = contentRect;
            var fieldRect = contentRect;
            contentRect = PadRectRight (contentRect, 80, 72);

            subLabelRect.width = 40;
            EditorGUI.LabelField (subLabelRect, "Min");
            
            fieldRect.x += 32;
            fieldRect.width = 60;
            var minValue = EditorGUI.FloatField (fieldRect, minProp.floatValue);

            fieldRect.x = contentRect.x + contentRect.width;
            subLabelRect.x = fieldRect.x + fieldRect.width - 16;
            EditorGUI.LabelField (subLabelRect, "Max");
            var maxValue = EditorGUI.FloatField (fieldRect, maxProp.floatValue);

            
            EditorGUI.MinMaxSlider (
                contentRect, ref minValue, ref maxValue, (float) minMax.Min, (float) minMax.Max);

            minProp.floatValue = minValue;
            maxProp.floatValue = maxValue;
        }

        public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
        {
            return minMax == null ? EditorGUIUtility.singleLineHeight : EditorGUIUtility.singleLineHeight * 2f;
        }

        private static Rect PadRectRight (in Rect rect, float amount, float shorten = 0)
        {
            var r = new Rect(rect);
            r.x += amount;
            r.width -= amount + shorten;
            return r;
        }
    }

}
