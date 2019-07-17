using System;
using UnityEngine;

namespace Hirame.Pantheon
{
    public class LifeCycleOnUpdate : LifeCycleEventBase
    {
        [SerializeField, Min (0)] private float spacing = 0.5f;

        private float nextPlayTime;

        private void OnEnable ()
        {
            nextPlayTime = Time.time + delay;
        }

        // TODO:
        // Replace with a lighter recurrency model
        private void Update ()
        {
            var time = Time.time;

            if (time < nextPlayTime)
                return;
            
            nextPlayTime = time + spacing;
            @event.Invoke ();
        }
    }
}
