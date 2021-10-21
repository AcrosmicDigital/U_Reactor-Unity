using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace U.Reactor
{
    // Base class to handle scroll events
    public abstract class UseScroll<TSelector, TUseScrollEvents> : MonoBehaviour,
        IScrollHandler

        where TUseScrollEvents : UseScroll<TSelector, TUseScrollEvents>
        where TSelector : REbaseSelector

    {
        
        protected TSelector selector;

        public Action<PointerEventData, TSelector> onScroll; // Called each frame is scrolling inside the object


        public void OnScroll(PointerEventData eventData)
        {
            onScroll?.Invoke(eventData, selector);
        }



        public class Hook
        {
            public Action<PointerEventData, TSelector> onScroll; // Called each frame is scrolling inside the object
        }

        public static void AddHook(GameObject gameObject, TSelector selector, Hook hook)
        {
            if (hook == null || gameObject == null || selector == null)
                return;

            var hookRunner = gameObject.AddComponent<TUseScrollEvents>();
            hookRunner.selector = selector;
            hookRunner.onScroll = hook.onScroll;

        }

    }
}
