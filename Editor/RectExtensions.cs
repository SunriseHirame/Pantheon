using UnityEditor;
using UnityEngine;

namespace Hirame.Pantheon.Editor
{
    public static class RectExtensions
    {
        public static Rect AsLineHigh (this Rect rect)
        {
            rect.height = EditorGUIUtility.singleLineHeight;
            return rect;
        }
        
        public static void NextLine (ref this Rect rect)
        {
            rect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        }

        public static void PreviousLine (ref this Rect rect)
        {
            rect.y -= EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        }
    }
}