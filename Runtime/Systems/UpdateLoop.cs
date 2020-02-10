using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;
using UnityEngine.Profiling;

namespace Hirame.Pantheon
{
    public static class UpdateLoop
    {
        private static readonly List<IGameSystemUpdate> gameSystemUpdate = new List<IGameSystemUpdate> ();
        private static readonly List<IEarlyUpdate> earlyUpdate = new List<IEarlyUpdate> ();
        private static readonly List<IWrapUpUpdate> warpUpUpdate = new List<IWrapUpUpdate> ();
        private static readonly List<ICameraUpdate> cameraUpdate = new List<ICameraUpdate> ();

        public static void RegisterForUpdate (IUpdate updateable)
        {
            switch (updateable)
            {
                case IGameSystemUpdate gameSystem:
                    gameSystemUpdate.Add (gameSystem);
                    break;
                case IEarlyUpdate early:
                    earlyUpdate.Add (early);
                    break;
                case IWrapUpUpdate wrapUp:
                    warpUpUpdate.Add (wrapUp);
                    break;
                case ICameraUpdate camera:
                    cameraUpdate.Add (camera);
                    break;
            }
        }
        
        public static void UnregisterForUpdate (IUpdate updateable)
        {
            switch (updateable)
            {
                case IGameSystemUpdate gameSystem:
                    gameSystemUpdate.Remove (gameSystem);
                    break;
                case IEarlyUpdate early:
                    earlyUpdate.Remove (early);
                    break;
                case IWrapUpUpdate wrapUp:
                    warpUpUpdate.Remove (wrapUp);
                    break;
                case ICameraUpdate camera:
                    cameraUpdate.Add (camera);
                    break;
            }
        }

        [RuntimeInitializeOnLoadMethod (RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Init ()
        {        
            var updateLoop = PlayerLoop.GetDefaultPlayerLoop ();

            var gameSystemUpdateLoop = new PlayerLoopSystem
            {
                type = typeof (UpdateLoop),
                updateDelegate = GameSystemUpdate
            };
            
            var earlyUpdateLoop = new PlayerLoopSystem
            {
                type = typeof (UpdateLoop),
                updateDelegate = EarlyUpdate
            };
            
            var wrapUpUpdateLoop = new PlayerLoopSystem
            {
                type = typeof (UpdateLoop),
                updateDelegate = WrapUpUpdate
            };

            var cameraUpdateLoop = new PlayerLoopSystem
            {
                type = typeof (UpdateLoop),
                updateDelegate = CameraUpdate
            };
            
            for (var i = 0; i < updateLoop.subSystemList.Length; i++)
            {
                if (updateLoop.subSystemList[i].type != typeof (Update))
                    continue;

                var list = updateLoop.subSystemList.ToList ();
                for (var j = 0; j < list.Count; j++)
                {
                    if (list[j].type == typeof (PreUpdate))
                    {
                        list.Insert (j++, gameSystemUpdateLoop);
                    }
                    else if (list[j].type == typeof (Update))
                    {
                        SetUpdateDelegate (j, Update, list);
                        
                        list.Insert (j++, earlyUpdateLoop);
                        list.Insert (j++, wrapUpUpdateLoop);
                    }
                    else if (list[j].type == typeof (PostLateUpdate))
                    {
                        list.Insert (j++, cameraUpdateLoop);
                    }
                }

                updateLoop.subSystemList = list.ToArray ();
                break;
            }
            
            PlayerLoop.SetPlayerLoop (updateLoop);
        }

        private static void SetUpdateDelegate (int index, PlayerLoopSystem.UpdateFunction callback, List<PlayerLoopSystem> systems)
        {
            var entry = systems[index];
            entry.updateDelegate += callback;
            systems[index] = entry;
        }

        private static void GameSystemUpdate ()
        {
            Profiler.BeginSample ("GameSystemUpdate");
            foreach (var update in gameSystemUpdate)
            {
                update.OnGameSystemUpdate ();
            }
            Profiler.EndSample ();
        }
        
        private static void EarlyUpdate ()
        {
            Profiler.BeginSample ("EarlyUpdate");
            foreach (var update in earlyUpdate)
            {
                update.OnEarlyUpdate ();
            }
            Profiler.EndSample ();
        }

        private static void Update ()
        {
        }
        
        private static void WrapUpUpdate ()
        {
            Profiler.BeginSample ("WrapUpUpdate");
            foreach (var update in warpUpUpdate)
            {
                update.OnWarpUpUpdate ();
            }
            Profiler.EndSample ();
        }
        
        private static void CameraUpdate ()
        {
            Profiler.BeginSample ("CameraUpdate");
            foreach (var update in cameraUpdate)
            {
                update.OnCameraUpdate ();
            }
            Profiler.EndSample ();
        }
        
#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
        private static void EditorInit ()
        {
            UnityEditor.EditorApplication.playModeStateChanged -= OnPlayModeChange;
            UnityEditor.EditorApplication.playModeStateChanged += OnPlayModeChange;

            if (!UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
                Init ();
        }    
        
        private static void OnPlayModeChange (UnityEditor.PlayModeStateChange playModeStateChange)
        {
            if (playModeStateChange == UnityEditor.PlayModeStateChange.ExitingEditMode
                || playModeStateChange == UnityEditor.PlayModeStateChange.EnteredEditMode)
            {
                gameSystemUpdate.Clear ();
                earlyUpdate.Clear ();
                warpUpUpdate.Clear ();
            }
        }
#endif
    }

}