﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    public class REinputField : RErenderer
    {
        public override Type elementType => this.GetType();
        protected override Func<RectTransformBSetter> PropsRectTransform => propsRectTransform;
        protected override Func<GameObjectBSetter> PropsGameObject => propsGameObject;
        protected override Func<IdBSetter> PropsId => propsId;
        protected override Func<CanvasRendererBSetter> PropsCanvasRenderer => propsCanvasRenderer;
        protected override Func<LayoutElementBSetter> PropsLayoutElement => propsLayoutElement;
        public override bool isLayoutElement => true;


        #region Components

        protected InputField inputFieldCmp;
        protected Image backImageCmp;
        protected Text placeholderTextCmp;
        protected Text textCmp;

        #endregion Components


        #region Setters

        // Base
        public Func<RectTransformSetter> propsRectTransform = () => new RectTransformSetter();
        public Func<GameObjectSetter> propsGameObject = () => new GameObjectSetter();
        public Func<IdSetter> propsId = () => new IdSetter();
        // Child
        public Func<CanvasRendererSetter> propsCanvasRenderer = () => new CanvasRendererSetter();
        // Layout element
        public Func<LayoutElementSetter> propsLayoutElement;

        public Func<InputFieldSetter> propsInputField = () => new InputFieldSetter { };
        public Func<BackImageSetter> propsBackImage = () => new BackImageSetter { };
        public Func<PlaceholderTextSetter> propsPlaceholderText = () => new PlaceholderTextSetter();
        public Func<TextSetter> propsText = () => new TextSetter();

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
            // Add the gameObjects
            var placeholderGO = InstanciateUIObject("Placeholder", gameObject);
            var textGO = InstanciateUIObject("Text", gameObject);


            backImageCmp = propsBackImage().Set(gameObject);
            inputFieldCmp = propsInputField().Set(gameObject);
            placeholderTextCmp = propsPlaceholderText().Set(placeholderGO);
            textCmp = propsText().Set(textGO);


            inputFieldCmp.targetGraphic = backImageCmp;
            inputFieldCmp.textComponent = textCmp;
            inputFieldCmp.placeholder = placeholderTextCmp;


            // Obtain percentage size
            SetReferenceSize(new RectTransformSetter());

            new RectTransformBSetter()
            {
                anchorMin = new Vector2(0, 0f),
                anchorMax = new Vector2(1, 1f),
                sizeDelta = Vector2.zero,
                offsetMin = GetPercentageSize(15, 10),  // 15,10
                offsetMax = GetPercentageSize(-15, -10)  // -15,-10
            }.SetByAnchors(placeholderGO.GetComponent<RectTransform>());

            new RectTransformBSetter()
            {
                anchorMin = new Vector2(0, 0f),
                anchorMax = new Vector2(1, 1f),
                sizeDelta = Vector2.zero,
                offsetMin = GetPercentageSize(15, 10),  // 15,10
                offsetMax = GetPercentageSize(-15, -10),  //-15,-10
            }.SetByAnchors(textGO.GetComponent<RectTransform>());

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

        protected override REbaseSelector AddSelector() => new Selector(gameObject, idCmp, rectTransformCmp, canvasRendererCmp, inputFieldCmp, backImageCmp, placeholderTextCmp, textCmp);

        protected override void AfterCreateComponent()
        {
            propsInputField().SetListeners(inputFieldCmp, (Selector)selector);
        }

        #endregion Drawers


        #region Subclasses


        public class Selector : RErendererSelector
        {

            public InputField inputField { get; private set; }
            public Image backImage { get; private set; }
            public Text placeholderText { get; private set; }
            public Text text { get; private set; }


            internal Selector(
                GameObject gameObject,
                HC.ReactorId pieceId,
                RectTransform rectTransform,
                CanvasRenderer canvasRenderer,
                // Extra
                InputField inputField,
                Image backImage,
                Text placeholderText,
                Text text
                ) : base(gameObject, pieceId, rectTransform, canvasRenderer)
            {
                this.inputField = inputField;
                this.backImage = backImage;
                this.placeholderText = placeholderText;
                this.text = text;
            }

            internal override void Destroy()
            {
                base.Destroy();

                inputField = null;
                backImage = null;
                placeholderText = null;
                text = null;
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

        public class CanvasRendererSetter : CanvasRendererBSetter
        {

        }

        public class IdSetter : IdBSetter
        {

        }

        public class GameObjectSetter : GameObjectBSetter
        {
            public override string name { get; set; } = "Input Field";
        }

        public class RectTransformSetter : RectTransformBSetter
        {
            public override float width { get; set; } = 370;
            public override float height { get; set; } = 70;
        }

        public class InputFieldSetter : InputFieldBSetter<Selector>
        {
            public override int caretWidth { get; set; } = 3;
        }

        public class BackImageSetter : ImageBSetter<Selector>
        {

        }

        public class PlaceholderTextSetter : TextBSetter
        {
            public override string text { get; set; } = "Enter text...";
            public override FontStyle fontStyle { get; set; } = FontStyle.Italic;
            public override int fontSize { get; set; } = 34;
            public override HorizontalWrapMode horizontalOverflow { get; set; } = HorizontalWrapMode.Wrap;
            public override VerticalWrapMode verticalOverflow { get; set; } = VerticalWrapMode.Truncate;
            public override Color fontColor { get; set; } = new Color(0, 0, 0, .5f);
            public override TextAnchor alignment { get; set; } = TextAnchor.MiddleLeft;
        }

        public class TextSetter : TextBSetter
        {
            public override string text { get; set; } = "";
            public override int fontSize { get; set; } = 34;
            public override bool richText { get; set; } = false;
            public override VerticalWrapMode verticalOverflow { get; set; } = VerticalWrapMode.Truncate;
            public override TextAnchor alignment { get; set; } = TextAnchor.MiddleLeft;
            public override Color fontColor { get; set; } = new Color(0.1960f, 0.1960f, 0.1960f, 1);
        }

        #endregion


        #region Static Funcs

        public static Selector CastSelector(REbaseSelector selector)
        {
            try
            {
                return (Selector)selector;
            }
            catch (Exception)
            {
                Debug.Log("REinputField: Cant cast selector");
                return null;
            }
        }

        public new static Selector[] Find(string pattern) => Find<Selector>(pattern);

        public new static Selector[] Find() => Find<Selector>();

        public new static Selector FindOne(string pattern) => FindOne<Selector>(pattern);


        #endregion Static Funcs


    }
}
