using System;
using UnityEngine;

namespace U.Reactor
{
    public abstract class UseLateUpdate<TSelector, TUseObjectEvents> : MonoBehaviour

        where TUseObjectEvents : UseLateUpdate<TSelector, TUseObjectEvents>
        where TSelector : ElementSelector

    {

        TSelector selector;

        public Action<TSelector> onLateUpdate;


        private void LateUpdate()
        {
            onLateUpdate?.Invoke(selector);
        }



        public class Hook
        {
            public Action<TSelector> onLateUpdate;
        }

        public static void AddHook(GameObject gameObject, TSelector selector, Hook hook)
        {
            if (hook == null || gameObject == null || selector == null)
                return;

            var hookRunner = gameObject.AddComponent<TUseObjectEvents>();

            hookRunner.selector = selector;
            hookRunner.onLateUpdate = hook.onLateUpdate;

        }

    }
}
