using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    public class REslider : REbase
    {
        public override Type elementType => this.GetType();
        protected override Func<RectTransformBSetter> PropsRectTransform => propsRectTransform;
        protected override Func<GameObjectBSetter> PropsGameObject => propsGameObject;
        protected override Func<ReactorIdBSetter> PropsReactorId => propsReactorId;
        protected override Func<LayoutElementBSetter> PropsLayoutElement => propsLayoutElement;
        public override bool isLayoutElement => true;


        #region Components

        protected Slider sliderCmp;
        protected Image backImageCmp;
        protected Image fillImageCmp;
        protected Image handleImageCmp;

        #endregion Components


        #region Setters

        // Base
        public Func<RectTransformSetter> propsRectTransform = () => new RectTransformSetter();
        public Func<GameObjectSetter> propsGameObject = () => new GameObjectSetter();
        public Func<ReactorIdSetter> propsReactorId = () => new ReactorIdSetter();
        // Layout element
        public Func<LayoutElementSetter> propsLayoutElement;

        public Func<SliderSetter> propsSlider = () => new SliderSetter();
        public Func<BackImageSetter> propsBackImage = () => new BackImageSetter();
        public Func<FillImageSetter> propsFillImage = () => new FillImageSetter();
        public Func<HandleImageSetter> propsHandleImage = () => new HandleImageSetter();

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
            // Agrega los ubObjetos del Button
            var backgroundGO = InstanciateUIObject("Background", gameObject);
            var fillAreaGO = InstanciateObject("Fill Area", gameObject);
            var fillGO = InstanciateUIObject("Fill", fillAreaGO);
            var handleAreaGO = InstanciateObject("Handle Slide Area", gameObject);
            var handleGO = InstanciateUIObject("Handle", handleAreaGO);


            sliderCmp = propsSlider().Set(gameObject);
            backImageCmp = propsBackImage().Set(backgroundGO);
            fillImageCmp = propsFillImage().Set(fillGO);
            handleImageCmp = propsHandleImage().Set(handleGO);


            // backgroundGO rect
            new RectTransformBSetter()
            {
                anchorMin = new Vector2(0, 0.25f),
                anchorMax = new Vector2(1, 0.75f),
                sizeDelta = Vector2.zero,
                offsetMin = Vector2.zero,
                offsetMax = Vector2.zero,
            }.SetOrSearchByAnchors(backgroundGO);

            new RectTransformBSetter()
            {
                anchorMin = new Vector2(0, 0.25f),
                anchorMax = new Vector2(1, 0.75f),
                sizeDelta = Vector2.zero,
                offsetMin = new Vector2(15, 0f),
                offsetMax = new Vector2(-30F, 0F),
            }.SetOrSearchByAnchors(fillAreaGO);

            new RectTransformBSetter()
            {
                sizeDelta = Vector2.zero,
                offsetMin = new Vector2(-14, 0f),
                offsetMax = new Vector2(14F, 0F),
            }.SetOrSearchByAnchors(fillGO);

            new RectTransformBSetter()
            {
                anchorMin = new Vector2(0, 0f),
                anchorMax = new Vector2(1, 1f),
                sizeDelta = Vector2.zero,
                offsetMin = new Vector2(14, 0f),
                offsetMax = new Vector2(-14F, 0F),
            }.SetOrSearchByAnchors(handleAreaGO);

            new RectTransformBSetter()
            {
                sizeDelta = Vector2.zero,
                offsetMin = new Vector2(-14, 0f),
                offsetMax = new Vector2(17F, 0F),
            }.SetOrSearchByAnchors(handleGO);

            sliderCmp.fillRect = fillGO.GetComponent<RectTransform>();
            sliderCmp.targetGraphic = handleImageCmp;
            sliderCmp.handleRect = handleGO.GetComponent<RectTransform>();

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

        protected override REbaseSelector AddSelector() => new Selector(gameObject, reactorIdCmp, rectTransformCmp, sliderCmp, backImageCmp, fillImageCmp, handleImageCmp);

        protected override void AfterCreateComponent()
        {
            propsSlider().SetListeners(sliderCmp, (Selector)selector);
            propsBackImage().SetListeners(backImageCmp, (Selector)selector);
            propsFillImage().SetListeners(fillImageCmp, (Selector)selector);
            propsHandleImage().SetListeners(handleImageCmp, (Selector)selector);
        }

        #endregion Drawers


        #region Subclasses
        public class Selector : REbaseSelector
        {

            public Slider slider { get; private set; }
            public Image backImage { get; private set; }
            public Image fillImage { get; private set; }
            public Image handleImage { get; private set; }


            internal Selector(
                GameObject gameObject,
                HC.ReactorId pieceId,
                RectTransform rectTransform,
                Slider slider,
                Image backImage,
                Image fillImage,
                Image handleImage
                ) : base(gameObject, pieceId, rectTransform)
            {
                this.slider = slider;
                this.backImage = backImage;
                this.fillImage = fillImage;
                this.handleImage = handleImage;
            }

            internal override void Destroy()
            {
                base.Destroy();

                slider = null;
                backImage = null;
                fillImage = null;
                handleImage = null;
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

        public class LayoutElementSetter : LayoutElementBSetter
        {

        }

        public class ReactorIdSetter : ReactorIdBSetter
        {

        }

        public class GameObjectSetter : GameObjectBSetter
        {
            public override string name { get; set; } = "Slider";
        }

        public class RectTransformSetter : RectTransformBSetter
        {
            public override float width { get; set; } = 500;
            public override float height { get; set; } = 40;
        }

        public class SliderSetter : SliderBSetter<Selector>
        {

        }

        public class BackImageSetter : ImageBSetter<Selector>
        {
            public override Color color { get; set; } = Color.gray;
        }

        public class FillImageSetter : ImageBSetter<Selector>
        {

        }

        public class HandleImageSetter : ImageBSetter<Selector>
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
