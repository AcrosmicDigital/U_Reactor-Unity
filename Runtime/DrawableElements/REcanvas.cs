using System;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    // Add URelementId, RectTransform, Canvas, CanvasScaler, GraphicRaycaster, CanvasGroup
    public class REcanvas : REbase
    {
        protected override Type elementType => this.GetType();
        protected override Func<RectTransformBSetter> PropsRectTransform { get => propsRectTransform; }
        protected override Func<GameObjectBSetter> PropsGameObject { get => propsGameObject; }


        #region Components

        protected Canvas canvasCmp;
        protected CanvasScaler canvasScalerCmp;
        protected GraphicRaycaster graphicRaycasterCmp;
        protected CanvasGroup canvasGroupCmp;

        #endregion Components


        #region Properties

        protected bool isHided = false;

        #endregion Properties


        #region Setters

        public Func<RectTransformSetter> propsRectTransform = () => new RectTransformSetter();
        public Func<GameObjectSetter> propsGameObject = () => new GameObjectSetter();

        public Func<CanvasSetter> propsCanvas = () => new CanvasSetter();
        public Func<CanvasScalerSetter> propsCanvasScaler = () => new CanvasScalerSetter();
        public Func<GraphicRaycasterSetter> propsGraphicRaycaster = () => new GraphicRaycasterSetter();
        public Func<CanvasGroupSetter> propsCanvasGroup = () => new CanvasGroupSetter();

        #endregion Setters


        #region Hooks

        public UseEffect.Hook[] useEffect;

        public UseApplicationEvents.Hook useApplicationEvents;
        public UseCanvasEvents.Hook useCanvasEvents;
        public UseDrag.Hook useDrag;
        public UseLateUpdate.Hook useLateUpdate;
        public UseObjectEvents.Hook useObjectEvents;
        public UsePointer.Hook usePointer;
        public UseScroll.Hook useScroll;
        public UseSelectEvents.Hook useSelectEvents;
        public UseSubmitEvents.Hook useSubmitEvents;
        public UseUpdate.Hook useUpdate;

        #endregion Hooks


        #region Drawers


        protected override void AddComponents()
        {

            if (rectTransformCmp.parent == null)
                if (PropsGameObject().dontDestroyOnLoad)
                    UnityEngine.Object.DontDestroyOnLoad(gameObject);

            // Set the props
            canvasCmp = propsCanvas().Set(gameObject);
            canvasScalerCmp = propsCanvasScaler().Set(gameObject);
            graphicRaycasterCmp = propsGraphicRaycaster().Set(gameObject);
            canvasGroupCmp = propsCanvasGroup().Set(gameObject);

            // Disable until all childs are rendered
            canvasCmp.enabled = false;

        }

        protected override ElementSelector AddSelector()
        {
            return new Selector(gameObject, elementIdCmp, rectTransformCmp, canvasCmp, canvasScalerCmp, graphicRaycasterCmp, canvasGroupCmp);
        }

        protected override void AddHooks()
        {
            UseEffect.AddHook(gameObject, (Selector)selector, useEffect);
            UseApplicationEvents.AddHook(gameObject, (Selector)selector, useApplicationEvents);
            UseCanvasEvents.AddHook(gameObject, (Selector)selector, useCanvasEvents);
            UseDrag.AddHook(gameObject, (Selector)selector, useDrag);
            UseLateUpdate.AddHook(gameObject, (Selector)selector, useLateUpdate);
            UseObjectEvents.AddHook(gameObject, (Selector)selector, useObjectEvents);
            UsePointer.AddHook(gameObject, (Selector)selector, usePointer);
            UseScroll.AddHook(gameObject, (Selector)selector, useScroll);
            UseSelectEvents.AddHook(gameObject, (Selector)selector, useSelectEvents);
            UseSubmitEvents.AddHook(gameObject, (Selector)selector, useSubmitEvents);
            UseUpdate.AddHook(gameObject, (Selector)selector, useUpdate);
        }

        protected override void AfterCreateComponent()
        {
            bool shouldEnable = propsCanvas().enabled;

            if (isHided)
                shouldEnable = false;

            if (canvasCmp != null)
                canvasCmp.enabled = shouldEnable;
        }

        #endregion Drawers


        #region Aditional Methods

        public REcanvas Draw(GameObject parent = null)
        {
            Create(parent, parentSelector);
            return this;
        }

        public void Erase()
        {
            if (gameObject != null)
                UnityEngine.Object.Destroy(gameObject); // Esta la puse en vez de la de abajo comentada, para evaluar posibles errores
                //UnityEngine.Object.DestroyImmediate(gameObject);
        }

        public void Hide()
        {
            isHided = true;
            if (canvasCmp != null)
            {
                canvasCmp.enabled = false;
            }
        }

        public void Show()
        {
            isHided = false;
            if (canvasCmp != null)
                canvasCmp.enabled = true;
            else
                Create(parent, parentSelector);
        }

        public void Disable()
        {
            isDisabled = true;
            if (gameObject != null)
                gameObject.SetActive(false);
        }

        public void Enable()
        {
            isDisabled = false;
            if (gameObject != null)
                gameObject.SetActive(true);
            else
                Create(parent, parentSelector);
        }

        #endregion Aditional Methods


        #region Subclasses


        public class Selector : ElementSelector
        {

            public Canvas canvas { get; private set; }
            public CanvasScaler canvasScaler { get; private set; }
            public GraphicRaycaster graphicRaycaster { get; private set; }
            public CanvasGroup canvasGroup { get; private set; }

            internal Selector(
                GameObject gameObject,
                ReactorId pieceId,
                RectTransform rectTransform,
                Canvas canvas,
                CanvasScaler canvasScaler,
                GraphicRaycaster graphicRaycaster,
                CanvasGroup canvasGroup
                ) : base(gameObject, pieceId, rectTransform)
            {
                this.canvas = canvas;
                this.canvasScaler = canvasScaler;
                this.graphicRaycaster = graphicRaycaster;
                this.canvasGroup = canvasGroup;
            }


            internal override void Destroy()
            {
                base.Destroy();

                canvas = null;
                canvasScaler = null;
                graphicRaycaster = null;
                canvasGroup = null;
            }
        }


        public class UseEffect : UseEffect<Selector, UseEffect> { }
        public class UseApplicationEvents : UseApplicationEvents<Selector, UseApplicationEvents> { }
        public class UseCanvasEvents : UseCanvasEvents<Selector, UseCanvasEvents> { }
        public class UseDrag : UseDrag<Selector, UseDrag> { }
        public class UseLateUpdate : UseLateUpdate<Selector, UseLateUpdate> { }
        public class UseObjectEvents : UseObjectEvents<Selector, UseObjectEvents> { }
        public class UsePointer : UsePointer<Selector, UsePointer> { }
        public class UseScroll : UseScroll<Selector, UseScroll> { }
        public class UseSelectEvents : UseSelectEvents<Selector, UseSelectEvents> { }
        public class UseSubmitEvents : UseSubtitEvents<Selector, UseSubmitEvents> { }
        public class UseUpdate : UseUpdate<Selector, UseUpdate> { }



        #endregion Subclasses


        #region Subsetters

        public class GameObjectSetter : GameObjectBSetter
        {
            public override string name { get; set; } = "Canvas";
        }
        public class RectTransformSetter : RectTransformBSetter
        {
            public override float width { get; set; } = 300;
            public override float height { get; set; } = 120;
        }

        public class CanvasSetter : CanvasBSetter
        {

        }

        public class CanvasScalerSetter : CanvasScalerBSetter
        {

        }

        public class GraphicRaycasterSetter : GraphicRaycasterBSetter
        {

        }

        public class CanvasGroupSetter : CanvasGroupBSetter
        {

        }

        #endregion


        #region Static Funcs



        public new static Selector[] Find(string pattern) => Find<Selector>(pattern);

        public new static Selector[] Find() => Find<Selector>();

        public new static Selector FindOne(string pattern) => FindOne<Selector>(pattern);


        #endregion Static Funcs


    }
}
