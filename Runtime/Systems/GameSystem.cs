using System.Collections.Generic;
using UnityEngine;

namespace Hirame.Pantheon.Core
{
    public abstract class GameSystem<T> : GameSystem where T : GameSystem<T>
    {
        public static T Instance { get; private set; }
    }

    public abstract class GameSystem : MonoBehaviour
    {
        private static GameObject gameSystems;

        [RuntimeInitializeOnLoadMethod (RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize ()
        {
            if (gameSystems)
                return;

            gameSystems = new GameObject ("Game Systems", AutoGameSystem.GameSystemTypes.ToArray ());
            gameSystems.hideFlags = HideFlags.DontSave;

            if (Application.isPlaying)
                DontDestroyOnLoad (gameSystems);
            
            foreach (var t in AutoGameSystem.GameSystemTypes)
            {
                Debug.Log (t);
            }
        }

#if UNITY_EDITOR
        //[UnityEditor.InitializeOnLoadMethod]
        private static void EditorInitialize ()
        {
            if (gameSystems)
                DestroyImmediate (gameSystems);
            Initialize ();
        }
#endif
    }
}