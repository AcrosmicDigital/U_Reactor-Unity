using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace U.Reactor
{
    // Base class to hanble events of selectable objects
    public abstract class UseSelectEvents<TSelector, TUseSelectEvents> : MonoBehaviour,
        ISelectHandler,
        IDeselectHandler,
        IMoveHandler,
        IUpdateSelectedHandler

        where TUseSelectEvents : UseSelectEvents<TSelector, TUseSelectEvents>
        where TSelector : ElementSelector

    {

        protected TSelector selector;

        public Action<BaseEventData, TSelector> onDeselect; // When a object is selected is called inside the selected object, one frame, only in selectable objects
        public Action<BaseEventData, TSelector> onMove;  // When a object is deselected is called inside the deselected object, one frame, only in selectable objects
        public Action<BaseEventData, TSelector> onSelect;   // When selectionis moved with keyboard or move kays (selected one object and press lef key to move to other object) is called inside the first object, one frame, only in selectable objects
        public Action<BaseEventData, TSelector> onUpdateSelected;  // When a object is selected is called inside the selected object, each frame, only in selectable objects


        public void OnDeselect(BaseEventData eventData)
        {
            onDeselect?.Invoke(eventData, selector);
        }

        public void OnMove(AxisEventData eventData)
        {
            onMove?.Invoke(eventData, selector);
        }

        public void OnSelect(BaseEventData eventData)
        {
            onSelect?.Invoke(eventData, selector);
        }

        public void OnUpdateSelected(BaseEventData eventData)
        {
            onUpdateSelected?.Invoke(eventData, selector);
        }



        public class Hook
        {
            public Action<BaseEventData, TSelector> onDeselect; // When a object is selected is called inside the selected object, one frame, only in selectable objects
            public Action<BaseEventData, TSelector> onMove;  // When a object is deselected is called inside the deselected object, one frame, only in selectable objects
            public Action<BaseEventData, TSelector> onSelect;   // When selectionis moved with keyboard or move kays (selected one object and press lef key to move to other object) is called inside the first object, one frame, only in selectable objects
            public Action<BaseEventData, TSelector> onUpdateSelected;  // When a object is selected is called inside the selected object, each frame, only in selectable objects
        }

        public static void AddHook(GameObject gameObject, TSelector selector, Hook hook)
        {
            if (hook == null || gameObject == null || selector == null)
                return;

            var hookRunner = gameObject.AddComponent<TUseSelectEvents>();
            hookRunner.selector = selector;
            hookRunner.onDeselect = hook.onDeselect;
            hookRunner.onMove = hook.onMove;
            hookRunner.onSelect = hook.onSelect;
            hookRunner.onUpdateSelected = hook.onUpdateSelected;

        }

    }
}
