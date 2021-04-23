using System;
using UnityEngine;

namespace U.Reactor
{
    public abstract class UseCanvasEvents<TSelector, TUseCanvasEvents> : MonoBehaviour

        where TUseCanvasEvents : UseCanvasEvents<TSelector, TUseCanvasEvents>
        where TSelector : ElementSelector

    {

        TSelector selector;

        public Action<TSelector> onCanvasGroupChanged;
        public Action<TSelector> onRectTransformDimensionsChange;


        private void OnCanvasGroupChanged()
        {
            onCanvasGroupChanged?.Invoke(selector);
        }

        private void OnRectTransformDimensionsChange()
        {
            onRectTransformDimensionsChange?.Invoke(selector);
        }



        public class Hook
        {
            public Action<TSelector> onCanvasGroupChanged;
            public Action<TSelector> onRectTransformDimensionsChange;
        }

        public static void AddHook(GameObject gameObject, TSelector selector, Hook hook)
        {
            if (hook == null || gameObject == null || selector == null)
                return;

            var hookRunner = gameObject.AddComponent<TUseCanvasEvents>();
            hookRunner.selector = selector;
            hookRunner.onCanvasGroupChanged = hook.onCanvasGroupChanged;
            hookRunner.onRectTransformDimensionsChange = hook.onRectTransformDimensionsChange;

        }

    }
}
