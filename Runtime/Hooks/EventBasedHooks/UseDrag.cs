using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace U.Reactor
{
    // Base class to handle drags
    public abstract class UseDrag<TSelector, TUseDragEvents> : MonoBehaviour,
        IBeginDragHandler,
        IDragHandler,
        IEndDragHandler,
        IInitializePotentialDragHandler,
        IDropHandler

        where TUseDragEvents : UseDrag<TSelector, TUseDragEvents>
        where TSelector : ElementSelector

    {

        protected TSelector selector;

        public Action<PointerEventData, TSelector> onBeginDrag; // Called the frame when a drap start
        public Action<PointerEventData, TSelector> onDrag;  // Called each frame that drag continue
        public Action<PointerEventData, TSelector> onEndDrag;  // Called the frame thar drag end
        public Action<PointerEventData, TSelector> onPotentialDrag;  // Called the frame that the mouse is clicked on the object, if a drag start onBeginDrag is called after
        public Action<PointerEventData, TSelector> onDrop;  // Called the frame when the object is droped over other object, only if raycast enabled for other object and is detected.


        public void OnBeginDrag(PointerEventData eventData)
        {
            onBeginDrag?.Invoke(eventData, selector);
        }

        public void OnDrag(PointerEventData eventData)
        {
            onDrag?.Invoke(eventData, selector);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            onEndDrag?.Invoke(eventData, selector);
        }

        public void OnInitializePotentialDrag(PointerEventData eventData)
        {
            onPotentialDrag?.Invoke(eventData, selector);
        }

        public void OnDrop(PointerEventData eventData)
        {
            onDrop?.Invoke(eventData, selector);
        }



        public class Hook
        {
            public Action<PointerEventData, TSelector> onBeginDrag; // Called the frame when a drap start
            public Action<PointerEventData, TSelector> onDrag;  // Called each frame that drag continue
            public Action<PointerEventData, TSelector> onEndDrag;  // Called the frame thar drag end
            public Action<PointerEventData, TSelector> onPotentialDrag;  // Called the frame that the mouse is clicked on the object, if a drag start onBeginDrag is called after
            public Action<PointerEventData, TSelector> onDrop;  // Called the frame when the object is droped over other object, only if raycast enabled for other object and is detected.
        }

        public static void AddHook(GameObject gameObject, TSelector selector, Hook hook)
        {
            if (hook == null || gameObject == null || selector == null)
                return;

            var hookRunner = gameObject.AddComponent<TUseDragEvents>();
            hookRunner.selector = selector;
            hookRunner.onBeginDrag = hook.onBeginDrag;
            hookRunner.onDrag = hook.onDrag;
            hookRunner.onEndDrag = hook.onEndDrag;
            hookRunner.onPotentialDrag = hook.onPotentialDrag;
            hookRunner.onDrop = hook.onDrop;

        }

    }
}
