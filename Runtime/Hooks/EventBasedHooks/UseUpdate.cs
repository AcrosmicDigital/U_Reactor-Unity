using System;
using UnityEngine;

namespace U.Reactor
{
    public abstract class UseUpdate<TSelector, TUseObjectEvents> : MonoBehaviour

        where TUseObjectEvents : UseUpdate<TSelector, TUseObjectEvents>
        where TSelector : REbaseSelector

    {

        TSelector selector;

        public Action<TSelector> onUpdate;


        private void Update()
        {
            onUpdate?.Invoke(selector);
        }


        public class Hook
        {
            public Action<TSelector> onUpdate;
        }

        public static void AddHook(GameObject gameObject, TSelector selector, Hook hook)
        {
            if (hook == null || gameObject == null || selector == null)
                return;

            var hookRunner = gameObject.AddComponent<TUseObjectEvents>();

            hookRunner.selector = selector;
            hookRunner.onUpdate = hook.onUpdate;

        }

    }
}

