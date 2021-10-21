using System;
using UnityEngine;

namespace U.Reactor
{
    public abstract class UseApplicationEvents<TSelector, TUseApplicationEvents> : MonoBehaviour

        where TUseApplicationEvents : UseApplicationEvents<TSelector, TUseApplicationEvents>
        where TSelector : REbaseSelector

    {

        TSelector selector;

        public Action<bool, TSelector> onApplicationFocus;
        public Action<bool, TSelector> onApplicationPause;
        public Action<TSelector> onApplicationQuit;


        private void OnApplicationFocus(bool focus)
        {
            onApplicationFocus?.Invoke(focus, selector);
        }

        private void OnApplicationPause(bool pause)
        {
            onApplicationPause?.Invoke(pause, selector);
        }

        private void OnApplicationQuit()
        {
            onApplicationQuit?.Invoke(selector);
        }



        public class Hook
        {
            public Action<bool, TSelector> onApplicationFocus;
            public Action<bool, TSelector> onApplicationPause;
            public Action<TSelector> onApplicationQuit;
        }

        public static void AddHook(GameObject gameObject, TSelector selector, Hook hook)
        {
            if (hook == null || gameObject == null || selector == null)
                return;

            var hookRunner = gameObject.AddComponent<TUseApplicationEvents>();

            hookRunner.selector = selector;

            hookRunner.onApplicationFocus = hook.onApplicationFocus;
            hookRunner.onApplicationPause = hook.onApplicationPause;
            hookRunner.onApplicationQuit = hook.onApplicationQuit;

        }

    }
}
