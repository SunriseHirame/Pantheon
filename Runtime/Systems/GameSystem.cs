using System;
using System.Linq;
using UnityEngine;

namespace Hirame.Pantheon.Core
{
    public abstract class GameSystem<T> : GameSystem where T : GameSystem<T>
    {
        public static T Instance { get; private set; }
    }

    public abstract class GameSystem : MonoBehaviour
    {
        protected static GameObject gameSystems;

#if UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod (RuntimeInitializeLoadType.BeforeSceneLoad)]
#else
        [RuntimeInitializeOnLoadMethod (RuntimeInitializeLoadType.BeforeSplashScreen)]
#endif
        private static void Initialize ()
        {
            if (gameSystems)
                return;

            var listOfBs = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies ()
                from assemblyType in domainAssembly.GetTypes ()
                where typeof (GameSystem).IsAssignableFrom (assemblyType) && !assemblyType.IsAbstract
                select assemblyType).ToArray ();

            gameSystems = new GameObject ("Game Systems", listOfBs);
            gameSystems.hideFlags = HideFlags.DontSave;

            if (Application.isPlaying)
                DontDestroyOnLoad (gameSystems);
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