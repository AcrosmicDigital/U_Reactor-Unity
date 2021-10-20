﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    public class REprogressBar : REbase
    {
        protected override Type elementType => this.GetType();
        protected override Func<RectTransformBSetter> PropsRectTransform { get => propsRectTransform; }
        protected override Func<GameObjectBSetter> PropsGameObject { get => propsGameObject; }


        #region Components

        protected Slider sliderCmp;
        protected Image backImageCmp;
        protected Image fillImageCmp;

        #endregion Components


        #region Setters

        public Func<RectTransformSetter> propsRectTransform = () => new RectTransformSetter();
        public Func<GameObjectSetter> propsGameObject = () => new GameObjectSetter();

        public Func<SliderSetter> propsSlider = () => new SliderSetter();
        public Func<BackImageSetter> propsBackImage = () => new BackImageSetter();
        public Func<FillImageSetter> propsFillImage = () => new FillImageSetter();

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
            var fillAreaGO = InstanciateObject("FillArea", gameObject);
            var fillGO = InstanciateUIObject("Fill", fillAreaGO);

            // Get RectTs
            var fillGOrectT = fillGO.GetComponent<RectTransform>();

            sliderCmp = propsSlider().Set(gameObject);
            backImageCmp = propsBackImage().Set(backgroundGO);
            fillImageCmp = propsFillImage().Set(fillGO);

            // backgroundGO rect
            new RectTransformBSetter()
            {
                anchorMin = new Vector2(0, 0.25f),
                anchorMax = new Vector2(1, 0.75f),
                sizeDelta = Vector2.zero,
                offsetMin = Vector2.zero,
                offsetMax = Vector2.zero,
            }.SetByAnchors(backgroundGO.GetComponent<RectTransform>());

            new RectTransformBSetter()
            {
                anchorMin = new Vector2(0, 0.25f),
                anchorMax = new Vector2(1, 0.75f),
                sizeDelta = Vector2.zero,
                offsetMin = new Vector2(15, 0f),
                offsetMax = new Vector2(-15F, 0F),
            }.SetByAnchors(fillAreaGO.GetComponent<RectTransform>());

            new RectTransformBSetter()
            {
                sizeDelta = Vector2.zero,
                offsetMin = new Vector2(-14, 0f),
                offsetMax = new Vector2(14F, 0F),
            }.SetByAnchors(fillGOrectT);

            sliderCmp.fillRect = fillGOrectT;

        }

        protected override ElementSelector AddSelector()
        {
            var sel = new Selector(gameObject, elementIdCmp, rectTransformCmp, sliderCmp, backImageCmp, fillImageCmp);

            return sel;
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

        #endregion Drawers


        #region Subclasses

        public class Selector : ElementSelector
        {

            public Slider slider { get; private set; }
            public Image backImage { get; private set; }
            public Image fillImage { get; private set; }


            internal Selector(
                GameObject gameObject,
                ReactorId pieceId,
                RectTransform rectTransform,
                Slider slider,
                Image backImage,
                Image fillImage
                ) : base(gameObject, pieceId, rectTransform)
            {
                this.slider = slider;
                this.backImage = backImage;
                this.fillImage = fillImage;
            }

            internal override void Destroy()
            {
                base.Destroy();

                slider = null;
                backImage = null;
                fillImage = null;
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
            public override string name { get; set; } = "Progress Bar";
        }

        public class RectTransformSetter : RectTransformBSetter
        {
            public override float width { get; set; } = 500;
            public override float height { get; set; } = 40;
        }

        public class SliderSetter : SliderBSetter
        {

        }

        public class BackImageSetter : ImageBSetter
        {
            public override Color color { get; set; } = Color.gray;
        }

        public class FillImageSetter : ImageBSetter
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