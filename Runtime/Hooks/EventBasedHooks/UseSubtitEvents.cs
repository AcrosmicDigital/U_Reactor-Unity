using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace U.Reactor
{
    // Base class to handle submit events
    public abstract class UseSubtitEvents<TSelector, TUseSubtitEvents> : MonoBehaviour,
        ICancelHandler,
        ISubmitHandler

        where TUseSubtitEvents : UseSubtitEvents<TSelector, TUseSubtitEvents>
        where TSelector : REbaseSelector

    {

        protected TSelector selector;

        public Action<BaseEventData, TSelector> onCancel; // When a object is canceled (selected and pressed Esc) is called inside the canceled object, one frame, only in cancelable objects
        public Action<BaseEventData, TSelector> onSubmit;  // When a object is submited (selected and pressed enter) is called inside the submited object, one frame, only in submitable objects


        public void OnCancel(BaseEventData eventData)
        {
            onCancel?.Invoke(eventData, selector);
        }

        public void OnSubmit(BaseEventData eventData)
        {
            onSubmit?.Invoke(eventData, selector);
        }



        public class Hook
        {
            public Action<BaseEventData, TSelector> onCancel; // When a object is canceled (selected and pressed Esc) is called inside the canceled object, one frame, only in cancelable objects
            public Action<BaseEventData, TSelector> onSubmit;  // When a object is submited (selected and pressed enter) is called inside the submited object, one frame, only in submitable objects
        }

        public static void AddHook(GameObject gameObject, TSelector selector, Hook hook)
        {
            if (hook == null || gameObject == null || selector == null)
                return;

            var hookRunner = gameObject.AddComponent<TUseSubtitEvents>();
            hookRunner.selector = selector;
            hookRunner.onCancel = hook.onCancel;
            hookRunner.onSubmit = hook.onSubmit;

        }

    }
}
