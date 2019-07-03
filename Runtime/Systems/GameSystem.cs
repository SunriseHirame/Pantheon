using System.Collections.Generic;
using UnityEngine;

namespace Hirame.Pantheon.Core
{
    public abstract class GameSystem<T> : GameSystem where T : GameSystem<T>
    {
        public static T Instance { get; private set; }

        static GameSystem ()
        {
            print (typeof (T));
            gameSystemTypes.Add (typeof (T));
        }
    }

    public abstract class GameSystem : MonoBehaviour
    {
        protected static readonly List<System.Type> gameSystemTypes = new List<System.Type> ();
        private static GameObject gameSystems;

        [RuntimeInitializeOnLoadMethod (RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize ()
        {
            if (gameSystems)
                return;

            gameSystems = new GameObject ("Game Systems", gameSystemTypes.ToArray ());
            gameSystems.hideFlags = HideFlags.HideAndDontSave;
        }

#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
        private static void EditorInitialize ()
        {
            Initialize ();
        }
#endif
    }
}