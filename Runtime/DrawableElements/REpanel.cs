using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    public class REpanel : RErenderer
    {
        public override Type elementType => this.GetType();
        protected override Func<RectTransformBSetter> PropsRectTransform => propsRectTransform;
        protected override Func<GameObjectBSetter> PropsGameObject => propsGameObject;
        protected override Func<ReactorIdBSetter> PropsReactorId => propsReactorId;
        protected override Func<CanvasRendererBSetter> PropsCanvasRenderer => propsCanvasRenderer;
        protected override Func<LayoutElementBSetter> PropsLayoutElement => null;
        public override bool isLayoutElement => false;


        #region Components

        protected Image imageCmp;

        #endregion Components


        #region Setters

        // Base
        public Func<RectTransformSetter> propsRectTransform = () => new RectTransformSetter();
        public Func<GameObjectSetter> propsGameObject = () => new GameObjectSetter();
        public Func<ReactorIdSetter> propsReactorId = () => new ReactorIdSetter();
        // Child
        public Func<CanvasRendererSetter> propsCanvasRenderer = () => new CanvasRendererSetter();

        public Func<ImageSetter> propsImage = () => new ImageSetter();

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

            imageCmp = propsImage().Set(gameObject);

        }

        protected override REbaseSelector AddSelector() => new Selector(gameObject, reactorIdCmp, rectTransformCmp, canvasRendererCmp, imageCmp);

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


        public class Selector : RErendererSelector
        {

            public Image image { get; private set; }

            internal Selector(
                GameObject gameObject,
                ReactorId pieceId,
                RectTransform rectTransform,
                CanvasRenderer canvasRenderer,
                Image image
                ) : base(gameObject, pieceId, rectTransform, canvasRenderer)
            {
                this.image = image;
            }


            internal override void Destroy()
            {
                base.Destroy();

                image = null;
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

        public class CanvasRendererSetter : CanvasRendererBSetter
        {

        }

        public class ReactorIdSetter : ReactorIdBSetter
        {

        }

        public class GameObjectSetter : GameObjectBSetter
        {
            public override string name { get; set; } = "Panel";
        }

        public class RectTransformSetter : RectTransformBSetter
        {
            public override float width { get; set; } = 0;
            public override float height { get; set; } = 0;
            public override Vector2 anchorMin { get; set; } = Vector2.zero;
            public override Vector2 anchorMax { get; set; } = Vector2.one;
        }

        public class ImageSetter : ImageBSetter
        {
            public override Color color { get; set; } = new Color(255, 255, 255, .4f);
        }

        #endregion


        #region Static Funcs




        public new static Selector[] Find(string pattern) => Find<Selector>(pattern);

        public new static Selector[] Find() => Find<Selector>();

        public new static Selector FindOne(string pattern) => FindOne<Selector>(pattern);

        #endregion Static Funcs


    }
}