using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    public class REverticalForm : ReactorChildElement
    {
        protected override string elementType => "Vertical Form";

        protected override Func<RectTransformSetter> PropsRectTransform { get => propsRectTransform; }



        #region <Components>

        protected Image backImageCmp;
        protected ScrollRect scrollRectCmp;
        protected RectMask2D rectMask2Cmp;
        protected VerticalLayoutGroup verticalLayoutCmp;
        protected ContentSizeFitter contentSizeCmp;

        protected Scrollbar vScrollbarCmp;
        protected Image vScrollbarImageCmp;
        protected Image vScrollbarHandleImageCmp;

        protected Scrollbar hScrollbarCmp;
        protected Image hScrollbarImageCmp;
        protected Image hScrollbarHandleImageCmp;


        #endregion </Components>


        #region <Setters>

        public Func<RectTransformSetter> propsRectTransform = () => new RectTransformSetterPanel();

        public Func<ImageSetter> propsBackImage = () => new ImageSetter 
        {
            color = new Color(255, 255, 255, .4f),
        };
        public Func<ScrollRectSetter> propsScrollRect = () => new ScrollRectSetter 
        { 
            horizontal = false,
        };
        public Func<RectMask2DSetter> propsRectMask2D = () => new RectMask2DSetter { };
        public Func<VerticalLayoutGroupSetter> propsVerticalLayoutGroup = () => new VerticalLayoutGroupSetter 
        {
            spacing = 10f,
        };
        public Func<ContentSizeFilterSetter> propsContentSizeFilter = () => new ContentSizeFilterSetter
        {
            horizontalFit = ContentSizeFitter.FitMode.MinSize,
            verticalFit = ContentSizeFitter.FitMode.MinSize,
        };

        public Func<ImageSetter> propsVScrollbarImage = () => new ImageSetter { };
        public Func<ScrollbarSetter> propsVScrollbar = () => new ScrollbarSetter { };
        public Func<ImageSetter> propsVScrollbarHandleImageCmp = () => new ImageSetter { };

        public Func<ImageSetter> propsHScrollbarImage = () => new ImageSetter { };
        public Func<ScrollbarSetter> propsHScrollbar = () => new ScrollbarSetter { };
        public Func<ImageSetter> propsHScrollbarHandleImageCmp = () => new ImageSetter { };

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
            var viewportGO = InstanciateUIObject("Viewport", gameObject);
            var containerGO = InstanciateUIObject("Container", viewportGO);
            var vScrollbarGO = InstanciateScrollbar("Vertical Scrollbar", gameObject, out vScrollbarCmp, out vScrollbarImageCmp, out vScrollbarHandleImageCmp);
            var hScrollbarGO = InstanciateScrollbar("Horizontal Scrollbar", gameObject, out hScrollbarCmp, out hScrollbarImageCmp, out hScrollbarHandleImageCmp);

            // Set virtual parent
            virtualParent = containerGO;

            // Add Components
            backImageCmp = propsBackImage().Set(gameObject);
            scrollRectCmp = propsScrollRect().Set(gameObject);
            rectMask2Cmp = propsRectMask2D().Set(viewportGO);
            verticalLayoutCmp = propsVerticalLayoutGroup().Set(containerGO);
            contentSizeCmp = propsContentSizeFilter().Set(containerGO);

            // Relations
            scrollRectCmp.content = containerGO.GetComponent<RectTransform>();
            scrollRectCmp.viewport = viewportGO.GetComponent<RectTransform>();
            scrollRectCmp.horizontalScrollbar = hScrollbarCmp;
            scrollRectCmp.verticalScrollbar = vScrollbarCmp;


            new RectTransformSetter()
            {
                anchorMin = new Vector2(0, 0f),
                anchorMax = new Vector2(1, 1f),
                offsetMin = new Vector2(0, 0f),
                offsetMax = new Vector2(0, 0f),
            }.SetByAnchors(viewportGO.GetComponent<RectTransform>());

            new RectTransformSetter()
            {
                pivot = new Vector2(0f, 1f),
                localPosition = new Vector2(0, 0f),
                sizeDelta = new Vector2(0, 0f),
                anchorMin = new Vector2(0, 0f),
                anchorMax = new Vector2(1, 1f),
            }.SetBySizeDelta(containerGO);

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
            var sel = new Selector(gameObject, elementIdCmp, rectTransformCmp, canvasRendererCmp, backImageCmp, scrollRectCmp, rectMask2Cmp, verticalLayoutCmp, contentSizeCmp,
                vScrollbarImageCmp, vScrollbarCmp, vScrollbarHandleImageCmp, hScrollbarImageCmp, hScrollbarCmp, hScrollbarHandleImageCmp);

            return sel;
        }

        public class Selector : ChildElementSelector
        {

            public Image backImage { get; private set; }
            public ScrollRect scrollRect { get; private set; }
            public RectMask2D rectMask2 { get; private set; }
            public VerticalLayoutGroup verticalLayout { get; private set; }
            public ContentSizeFitter contentSize { get; private set; }

            public Image vScrollbarImage { get; private set; }
            public Scrollbar vScrollbar { get; private set; }
            public Image vScrollbarHandleImage { get; private set; }

            public Image hScrollbarImage { get; private set; }
            public Scrollbar hScrollbar { get; private set; }
            public Image hScrollbarHandleImage { get; private set; }


            internal Selector(
                GameObject gameObject,
                ElementId pieceId,
                RectTransform rectTransform,
                CanvasRenderer canvasRenderer,

                Image backImage,
                ScrollRect scrollRect,
                RectMask2D rectMask2,
                VerticalLayoutGroup verticalLayout,
                ContentSizeFitter contentSize,

                Image vScrollbarImage,
                Scrollbar vScrollbar,
                Image vScrollbarHandleImage,

                Image hScrollbarImage,
                Scrollbar hScrollbar,
                Image hScrollbarHandleImage

                ) : base(gameObject, pieceId, rectTransform, canvasRenderer)
            {
                this.backImage = backImage;
                this.scrollRect = scrollRect;
                this.rectMask2 = rectMask2;
                this.verticalLayout = verticalLayout;
                this.contentSize = contentSize;

                this.vScrollbarImage = vScrollbarImage;
                this.vScrollbar = vScrollbar;
                this.vScrollbarHandleImage = vScrollbarHandleImage;

                this.hScrollbarImage = hScrollbarImage;
                this.hScrollbar = hScrollbar;
                this.hScrollbarHandleImage = hScrollbarHandleImage;
            }

            internal override void Destroy()
            {
                base.Destroy();

                this.backImage = null;
                this.scrollRect = null;
                this.rectMask2 = null;
                this.verticalLayout = null;
                this.contentSize = null;

                this.vScrollbarImage = null;
                this.vScrollbar = null;
                this.vScrollbarHandleImage = null;

                this.hScrollbarImage = null;
                this.hScrollbar = null;
                this.hScrollbarHandleImage = null;

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

