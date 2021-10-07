using System;
using UnityEngine;

namespace U.Reactor
{
    public abstract class UseObjectEvents<TSelector, TUseObjectEvents> : MonoBehaviour

        where TUseObjectEvents : UseObjectEvents<TSelector, TUseObjectEvents>
        where TSelector : ElementSelector

    {

        TSelector selector;

        public Action<TSelector> onStart;
        public Action<TSelector> onAwake;
        public Action<TSelector> onEnable;
        public Action<TSelector> onDisable;
        public Action<TSelector> onDestroy;


        private void Start()
        {
            try
            {
                onAwake?.Invoke(selector);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

            try
            {
                onEnable?.Invoke(selector);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

            try
            {
                onStart?.Invoke(selector);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            
        }

        private void OnEnable()
        {
            onEnable?.Invoke(selector);
        }

        private void OnDisable()
        {
            onDisable?.Invoke(selector);
        }

        private void OnDestroy()
        {
            onDestroy?.Invoke(selector);
        }



        public class Hook
        {
            public Action<TSelector> onStart;
            public Action<TSelector> onAwake;
            public Action<TSelector> onEnable;
            public Action<TSelector> onDisable;
            public Action<TSelector> onDestroy;
        }

        public static void AddHook(GameObject gameObject, TSelector selector, Hook hook)
        {
            if (hook == null || gameObject == null || selector == null)
                return;

            var hookRunner = gameObject.AddComponent<TUseObjectEvents>();

            hookRunner.selector = selector;
            hookRunner.onAwake = hook.onAwake;
            hookRunner.onStart = hook.onStart;
            hookRunner.onEnable = hook.onEnable;
            hookRunner.onDisable = hook.onDisable;
            hookRunner.onDestroy = hook.onDestroy;

        }

    }
}
