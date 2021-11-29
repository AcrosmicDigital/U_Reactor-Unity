using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace U.Reactor
{
    public class UseEffect<TSelector, TUseEffect> : MonoBehaviour

        where TUseEffect : UseEffect<TSelector, TUseEffect>
        where TSelector : REbaseSelector

    {

        protected TSelector selector;
        protected HooksConfig.TimeMode timeMode; // Time between functions calls
        protected float duration; // Time between functions calls
        protected int iterationsCount; // If will be destroyed after first execution, 0 or less for loop
        protected Action<float, TSelector> deltaFunction;


        // Private
        private float time = 0;
        private int completedIterations = 0;



        protected void Update()
        {

            // Return if time is 0 or negative
            if (duration <= 0)
            {
                Debug.LogError("Duration cant be 0 or less");
                Destroy(this);
                return;
            }

            if(iterationsCount > 0)
            {
                if (completedIterations >= iterationsCount)
                {
                    Destroy(this);
                    return;
                }
            }
            

            // Delta runner
            if (time >= duration)
            {
                completedIterations++;
                time -= duration;

                // Execute the delegates
                try
                {
                    deltaFunction?.Invoke(duration, selector);
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                    Destroy(this);
                    return;
                }
            }

            if( timeMode == HooksConfig.TimeMode.DeltaTime)
                time += Time.deltaTime;
            else
                time += Time.unscaledDeltaTime;

            return;

        }






        public class Hook
        {
            public float duration = 1f;
            public HooksConfig.TimeMode timeMode = HooksConfig.TimeMode.DeltaUnscaledTime;
            public Action<float, TSelector> deltaFunction;
            public int iterationsCount = -1;
        }


        public static void AddHook(GameObject gameObject, TSelector selector, Hook[] effects)
        {
            if (effects == null || gameObject == null || selector == null)
                return;

            foreach (var effect in effects)
            {
                if (effect == null)
                    continue;

                var useEffect = gameObject.AddComponent<TUseEffect>();
                useEffect.selector = selector;
                useEffect.duration = effect.duration;
                useEffect.timeMode = effect.timeMode;
                useEffect.deltaFunction = effect.deltaFunction;
                useEffect.iterationsCount = effect.iterationsCount;
            }
        }

    }

    public class HooksConfig
    {
        public enum TimeMode
        {
            DeltaTime,
            DeltaUnscaledTime,
        }
    }

}
