using System;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    // Add URelementId, RectTransform, Canvas, CanvasScaler, GraphicRaycaster, CanvasGroup
    public class REcanvas : REbase
    {
        public override Type elementType => this.GetType();
        protected override Func<RectTransformBSetter> PropsRectTransform => propsRectTransform;
        protected override Func<GameObjectBSetter> PropsGameObject => propsGameObject;
        protected override Func<ReactorIdBSetter> PropsReactorId => propsReactorId;
        protected override Func<LayoutElementBSetter> PropsLayoutElement => null;
        public override bool isLayoutElement => false;


        #region Components

        protected Canvas canvasCmp;
        protected CanvasScaler canvasScalerCmp;
        protected GraphicRaycaster graphicRaycasterCmp;
        protected CanvasGroup canvasGroupCmp;

        #endregion Components


        #region Properties

        protected bool isHided = false;
        public bool enabled { get; set; } = true;

        #endregion Properties


        #region Setters

        // Base
        public Func<RectTransformSetter> propsRectTransform = () => new RectTransformSetter();
        public Func<GameObjectSetter> propsGameObject = () => new GameObjectSetter();
        public Func<ReactorIdSetter> propsReactorId = () => new ReactorIdSetter();

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

            propsGameObject().SetDontDestroyOnLoad(gameObject);

            // Set the props
            canvasCmp = propsCanvas().Set(gameObject);
            canvasScalerCmp = propsCanvasScaler().Set(gameObject);
            graphicRaycasterCmp = propsGraphicRaycaster().Set(gameObject);
            canvasGroupCmp = propsCanvasGroup().Set(gameObject);

            // Disable until all childs are rendered
            canvasCmp.enabled = false;

        }

        protected override REbaseSelector AddSelector() => new Selector(gameObject, reactorIdCmp, rectTransformCmp, canvasCmp, canvasScalerCmp, graphicRaycasterCmp, canvasGroupCmp);

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

        protected override void AfterRenderComponent()
        {

            //new RectTransformBSetter
            //{
            //    anchorMin = Vector2.zero,
            //    anchorMax = Vector2.one,
            //    offsetMin = Vector2.zero,
            //    offsetMax = Vector2.zero,
            //}.SetByAnchors(gameObject.GetComponent<RectTransform>());
            // Set again values of the canvas recttransform
            propsRectTransform().SetOrSearchByAnchors(gameObject);

            bool shouldEnable = enabled;

            if (isHided)
                shouldEnable = false;

            if (canvasCmp != null)
                canvasCmp.enabled = shouldEnable;

        }

        #endregion Drawers


        #region Aditional Methods

        public REcanvas Draw()
        {
            return Draw(null);
        }

        public REcanvas Draw(GameObject parent)
        {
            Draw(parent, parentSelector);

            return this;
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
                Draw(parent, parentSelector);
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
                Draw(parent, parentSelector);
        }

        #endregion Aditional Methods


        #region Subclasses


        public class Selector : REbaseSelector
        {

            public Canvas canvas { get; private set; }
            public CanvasScaler canvasScaler { get; private set; }
            public GraphicRaycaster graphicRaycaster { get; private set; }
            public CanvasGroup canvasGroup { get; private set; }


            internal Selector(
                GameObject gameObject,
                HC.ReactorId pieceId,
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

        public class ReactorIdSetter : ReactorIdBSetter
        {

        }

        public class GameObjectSetter : GameObjectBSetter
        {
            public override string name { get; set; } = "Canvas";
        }
        public class RectTransformSetter : RectTransformBSetter
        {
            public override float width { get; set; } = 0;
            public override float height { get; set; } = 0;
            public override Vector2 anchorMin { get; set; } = Vector2.zero;
            public override Vector2 anchorMax { get; set; } = Vector2.one;
        }

        public class CanvasSetter : CanvasBSetter
        {
            public override RenderMode renderMode { get; set; } = RenderMode.ScreenSpaceOverlay;
        }

        public class CanvasScalerSetter : CanvasScalerBSetter
        {
            public override CanvasScaler.ScaleMode uiScaleMode { get; set; } = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            public override Vector3 referenceResolution { get; set; } = new Vector3(1920, 1080);
            public override float matchWidthOrHeight { get; set; } = 0.5f;
        }

        public class GraphicRaycasterSetter : GraphicRaycasterBSetter
        {

        }

        public class CanvasGroupSetter : CanvasGroupBSetter
        {

        }

        #endregion


        #region Static Funcs


        public static RectTransformSetter TableRectTransform(int xPad, int xCell, int yPad, int yCell)
        {
            // Validate values
            if ((xCell < 1) && (xCell > 100)) throw new FormatException("REpanel.TableRectTransform(): xCell(" + xCell + ") must be between 0 and 100");
            if ((yCell < 1) && (yCell > 100)) throw new FormatException("REpanel.TableRectTransform(): yCell(" + yCell + ") must be between 0 and 100");
            if ((xPad < 0) && (xPad > 99)) throw new FormatException("REpanel.TableRectTransform(): xPad(" + xPad + ") must be between 0 and 99");
            if ((yPad < 0) && (xPad > 99)) throw new FormatException("REpanel.TableRectTransform(): yPad(" + yPad + ") must be between 0 and 99");
            if (xCell < xPad) throw new FormatException("REpanel.TableRectTransform(): xCell(" + xCell + ") must be greater than xPad(" + xPad + ")");
            if (yCell < yPad) throw new FormatException("REpanel.TableRectTransform(): yCell(" + yCell + ") must be greater than yPad(" + yPad + ")");

            return new RectTransformSetter
            {
                anchorMin = new Vector2(xPad / 100f, yPad / 100f),
                anchorMax = new Vector2(xCell / 100f, yCell / 100f),
            };
        }


        public static void EraseAll()
        {
            var allCanvas = REcanvas.Find();
            for (int i = 0; i < allCanvas.Length; i++)
            {
                allCanvas[i]?.Erase();
            }

        }

        public static void EraseByPattern(string pattern)
        {
            var allCanvas = REcanvas.Find(pattern);
            for (int i = 0; i < allCanvas.Length; i++)
            {
                allCanvas[i]?.Erase();
            }

        }

        public static Selector CastSelector(REbaseSelector selector)
        {
            try
            {
                return (Selector)selector;
            }
            catch (Exception)
            {
                Debug.Log("REcanvas: Cant cast selector");
                return null;
            }
        }

        public new static Selector[] Find(string pattern) => Find<Selector>(pattern);

        public new static Selector[] Find() => Find<Selector>();

        public new static Selector FindOne(string pattern) => FindOne<Selector>(pattern);


        #endregion Static Funcs


    }
}
