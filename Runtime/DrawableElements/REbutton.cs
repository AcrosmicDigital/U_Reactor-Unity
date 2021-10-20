using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    public class REbutton : REchild
    {
        protected override Type elementType => this.GetType();
        protected override Func<RectTransformBSetter> PropsRectTransform { get => propsRectTransform; }
        protected override Func<GameObjectBSetter> PropsGameObject { get => propsGameObject; }


        #region Components

        protected RectTransform rectTransform;
        protected Button buttonCmp;
        protected Image imageCmp;
        protected Text textCmp;

        #endregion Components


        #region Setters

        public Func<RectTransformSetter> propsRectTransform = () => new RectTransformSetter();
        public Func<GameObjectSetter> propsGameObject = () => new GameObjectSetter();

        public Func<TextSetter> propsText = () => new TextSetter();
        public Func<ImageSetter> propsImage = () => new ImageSetter();
        public Func<ButtonSetter> propsButton = () => new ButtonSetter();

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
            // Agrega los componentes del Button
            var textGO = InstanciateUIObject("Text", gameObject);

            imageCmp = propsImage().Set(gameObject);
            buttonCmp = gameObject.AddComponent<Button>(); // Add here, becouse need the selector and is not set yet
            textCmp = propsText().Set(textGO);

            // Text rectT
            new RectTransformBSetter()
            {
                anchorMin = Vector2.zero,
                anchorMax = Vector2.one,
                sizeDelta = Vector2.zero,
                offsetMin = Vector2.zero,
                offsetMax = Vector2.zero,
            }.SetByAnchors(textGO.GetComponent<RectTransform>());

        }

        protected override ElementSelector AddSelector()
        {
            var sel = new Selector(gameObject, elementIdCmp, rectTransformCmp, canvasRendererCmp, textCmp, buttonCmp, imageCmp);

            propsButton().Set(buttonCmp, sel);

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


        public class Selector : ChildElementSelector
        {

            public Button button { get; private set; }
            public Image image { get; private set; }
            public Text textCmp { get; private set; }


            internal Selector(
                GameObject gameObject,
                ReactorId pieceId,
                RectTransform rectTransform,
                CanvasRenderer canvasRenderer,
                Text textCmp, Button button,
                Image image
                ) : base(gameObject, pieceId, rectTransform, canvasRenderer)
            {
                this.button = button;
                this.image = image;
                this.textCmp = textCmp;
            }

            internal override void Destroy()
            {
                base.Destroy();

                button = null;
                image = null;
                textCmp = null;
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
            public override string name { get; set; } = "Button";
        }
        public class RectTransformSetter : RectTransformBSetter
        {
            public override float width { get; set; } = 300;
            public override float height { get; set; } = 120;
        }

        public class ButtonSetter : ButtonBSetter
        {

        }

        public class ImageSetter : ImageBSetter
        {

        }

        public class TextSetter : TextBSetter
        {
            public override int fontSize { get; set; } = 60;
            public override TextAnchor alignment { get; set; } = TextAnchor.MiddleCenter;
        }

        #endregion


        #region Static Funcs



        public new static Selector[] Find(string pattern) => Find<Selector>(pattern);

        public new static Selector[] Find() => Find<Selector>();

        public new static Selector FindOne(string pattern) => FindOne<Selector>(pattern);

        #endregion Static Funcs


    }
}
