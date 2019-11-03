using UnityEditor;

namespace Hirame.Pantheon.Editor
{
    [CustomEditor (typeof (LifeCycleEventBase), true)]
    public class LifeCycleEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI ()
        {
            serializedObject.Update ();
            
            using (var scope = new EditorGUI.ChangeCheckScope ())
            {
                DrawPropertiesExcluding (serializedObject, "m_Script", "event");

                EditorGUILayout.Space ();
                EditorGUILayout.PropertyField (serializedObject.FindProperty ("event"));

                if (scope.changed)
                    serializedObject.ApplyModifiedProperties ();
            }
        }
    }
}
