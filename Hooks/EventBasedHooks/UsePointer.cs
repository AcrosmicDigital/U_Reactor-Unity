using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace U.Reactor
{
    // Base class of a Pointer event handler
    public abstract class UsePointer<TSelector, TUsePointerEvents> : MonoBehaviour,
        IPointerEnterHandler,
        IPointerExitHandler,
        IPointerDownHandler,
        IPointerUpHandler,
        IPointerClickHandler

        where TUsePointerEvents : UsePointer<TSelector, TUsePointerEvents>
        where TSelector : ElementSelector

    {

        protected TSelector selector;

        public Action<PointerEventData, TSelector> onPointerEnter; // Called the frame when the pointer enter the object
        public Action<PointerEventData, TSelector> onPointerExit; // Called the frame when pointer exit
        public Action<PointerEventData, TSelector> onPointerDown; // Called the frame the pointer down
        public Action<PointerEventData, TSelector> onPointerUp; // Called the frame the pointer up
        public Action<PointerEventData, TSelector> onPointerClick; // Called the frame the pointer down and up in the object

        public void OnPointerClick(PointerEventData eventData)
        {
            onPointerClick?.Invoke(eventData, selector);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            onPointerDown?.Invoke(eventData, selector);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            onPointerEnter?.Invoke(eventData, selector);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            onPointerExit?.Invoke(eventData, selector);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            onPointerUp?.Invoke(eventData, selector);
        }



        public class Hook
        {
            public Action<PointerEventData, TSelector> onPointerEnter; // Called the frame when the pointer enter the object
            public Action<PointerEventData, TSelector> onPointerExit; // Called the frame when pointer exit
            public Action<PointerEventData, TSelector> onPointerDown; // Called the frame the pointer down
            public Action<PointerEventData, TSelector> onPointerUp; // Called the frame the pointer up
            public Action<PointerEventData, TSelector> onPointerClick; // Called the frame the pointer down and up in the object
        }

        public static void AddHook(GameObject gameObject, TSelector selector, Hook hook)
        {
            if (hook == null || gameObject == null || selector == null)
                return;

            var hookRunner = gameObject.AddComponent<TUsePointerEvents>();
            hookRunner.selector = selector;
            hookRunner.onPointerEnter = hook.onPointerEnter;
            hookRunner.onPointerExit = hook.onPointerExit;
            hookRunner.onPointerDown = hook.onPointerDown;
            hookRunner.onPointerUp = hook.onPointerUp;
            hookRunner.onPointerClick = hook.onPointerClick;

        }

    }
}
