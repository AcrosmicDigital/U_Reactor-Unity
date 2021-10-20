using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    public class REinputField : REchild
    {
        protected override Type elementType => this.GetType();
        protected override string elementName => "InputField";

        protected override Func<RectTransformSetter> PropsRectTransform { get => propsRectTransform; }


        #region <Components>

        protected InputField inputFieldCmp;
        protected Image backImageCmp;
        protected Text placeholderTextCmp;
        protected Text textCmp;

        #endregion </Components>


        #region <Setters>

        public Func<RectTransformSetter> propsRectTransform = () => new RectTransformSetterButton
        {
            width = 400,
            height = 70,
        };
        public Func<InputFieldSetter> propsInputField = () => new InputFieldSetter { };
        public Func<ImageSetter> propsBackImage = () => new ImageSetter { };
        public Func<TextSetter> propsPlaceholderText = () => new TextSetter
        {
            text = "Enter text...",
            fontStyle = FontStyle.Italic,
            fontSize = 40,
            horizontalOverflow = HorizontalWrapMode.Wrap,
            verticalOverflow = VerticalWrapMode.Truncate,
            fontColor = new Color(0,0,0,.5f),
        };
        public Func<TextSetter> propsText = () => new TextSetter 
        { 
            text = "",
            fontSize = 40,
            richText = false,
            verticalOverflow = VerticalWrapMode.Truncate,
        };

        #endregion </Setters>


        #region <Hooks>

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

        #endregion </Hooks>



        protected override void AddComponents()
        {
            // Add the gameObjects
            var placeholderGO = InstanciateUIObject("Placeholder", gameObject);
            var textGO = InstanciateUIObject("Text", gameObject);


            backImageCmp = propsBackImage().Set(gameObject);
            inputFieldCmp = propsInputField().Set(gameObject);
            placeholderTextCmp = propsPlaceholderText().Set(placeholderGO);
            textCmp = propsText().Set(textGO);


            inputFieldCmp.textComponent = textCmp;
            inputFieldCmp.placeholder = placeholderTextCmp;


            new RectTransformSetter()
            {
                anchorMin = new Vector2(0, 0f),
                anchorMax = new Vector2(1, 1f),
                sizeDelta = Vector2.zero,
                offsetMin = new Vector2(15, 10f),
                offsetMax = new Vector2(-15F, -10F),
            }.SetByAnchors(placeholderGO.GetComponent<RectTransform>());

            new RectTransformSetter()
            {
                anchorMin = new Vector2(0, 0f),
                anchorMax = new Vector2(1, 1f),
                sizeDelta = Vector2.zero,
                offsetMin = new Vector2(15, 10f),
                offsetMax = new Vector2(-15F, -10F),
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

        protected override ElementSelector AddSelector()
        {
            var sel = new Selector(gameObject, elementIdCmp, rectTransformCmp, canvasRendererCmp, inputFieldCmp, backImageCmp, placeholderTextCmp, textCmp);

            return sel;
        }



        public class Selector : ChildElementSelector
        {

            public InputField inputField { get; private set; }
            public Image backImage { get; private set; }
            public Text placeholderText { get; private set; }
            public Text text { get; private set; }


            internal Selector(
                GameObject gameObject,
                ReactorId pieceId,
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




        public new static Selector[] Find(string pattern) => Find<Selector>(pattern);

        public new static Selector[] Find() => Find<Selector>();

        public new static Selector FindOne(string pattern) => FindOne<Selector>(pattern);


    }
}
