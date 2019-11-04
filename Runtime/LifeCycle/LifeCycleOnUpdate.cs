using System;
using UnityEngine;

namespace Hirame.Pantheon
{
    public class LifeCycleOnUpdate : LifeCycleEventBase, IWrapUpUpdate
    {
        [SerializeField, Min (0)] private float spacing = 0.5f;

        private float nextPlayTime;

        private void OnEnable ()
        {
            nextPlayTime = Time.time + delay;
            UpdateLoop.RegisterForUpdate (this);
        }

        private void OnDisable ()
        {
            UpdateLoop.UnregisterForUpdate (this);
        }

        void IWrapUpUpdate.OnWarpUpUpdate ()
        {
            var time = Time.time;

            if (time < nextPlayTime)
                return;
            
            nextPlayTime = time + spacing;
            @event.Invoke ();
        }
    }
}
