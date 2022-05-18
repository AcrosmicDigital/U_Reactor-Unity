using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    public class REdropdown : RErenderer
    {
        public override Type elementType => this.GetType();
        protected override Func<RectTransformBSetter> PropsRectTransform => propsRectTransform;
        protected override Func<GameObjectBSetter> PropsGameObject => propsGameObject;
        protected override Func<IdBSetter> PropsId => propsId;
        protected override Func<CanvasRendererBSetter> PropsCanvasRenderer => propsCanvasRenderer;
        protected override Func<LayoutElementBSetter> PropsLayoutElement => propsLayoutElement;
        public override bool isLayoutElement => true;


        #region Components

        protected Dropdown dropdownCmp;
        protected Image dropdownImageCmp;
        protected Text labelTextCmp;
        protected Image arrowImageCmp;
        protected Image templateImageCmp;
        protected ScrollRect scrollRectCmp;
        protected Image viewportImageCmp;
        protected Mask maskCmp;
        protected Toggle toggleCmp;
        protected Image itemBackImageCmp;
        protected Image itemCheckImageCmp;
        protected Text itemLabelTextCmp;
        protected Image scrollbarImageCmp;
        protected Scrollbar scrollbarCmp;
        protected Image scrollbarHandleImageCmp;

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

        public Func<DropdownSetter> propsDropdown = () => new DropdownSetter();
        public Func<BackImageSetter> propsDropdownImage = () => new BackImageSetter();
        public Func<LabelTextSetter> propsLabelText = () => new LabelTextSetter();
        public Func<ArrowImageSetter> propsArrowImage = () => new ArrowImageSetter();
        public Func<DownBackImageSetter> propsTemplateImage = () => new DownBackImageSetter();
        public Func<ScrollRectSetter> propsScrollRect = () => new ScrollRectSetter();
        public Func<ViewportImageSetter> propsViewportImage = () => new ViewportImageSetter();
        public Func<MaskSetter> propsMask = () => new MaskSetter();
        public Func<ToggleSetter> propsToggle = () => new ToggleSetter();
        public Func<ItemBackImageSetter> propsItemBackImage = () => new ItemBackImageSetter();
        public Func<ItemCheckImageSetter> propsItemCheckImage = () => new ItemCheckImageSetter();
        public Func<ItemTextSetter> propsItemLabelText = () => new ItemTextSetter();
        public Func<ScrollbarBackImageSetter> propsScrollbarImage = () => new ScrollbarBackImageSetter();
        public Func<ScrollbarSetter> propsScrollbar = () => new ScrollbarSetter();
        public Func<ScrollbarHandleImageSetter> propsScrollbarHandleImageCmp = () => new ScrollbarHandleImageSetter();

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
            var labelGO = InstanciateUIObject("Label", gameObject);
            var arrowGO = InstanciateUIObject("Arrow", gameObject);
            var templateGO = InstanciateUIObject("Template", gameObject);
                var viewportGO = InstanciateObject("Viewport", templateGO);
                    var contentGO = InstanciateObject("Content", viewportGO);
                        var itemGO = InstanciateObject("Item", contentGO);
                            var itemBackgroundGO = InstanciateUIObject("Item Background", itemGO);
                            var itemCheckmarkGO = InstanciateUIObject("Item Checkmark", itemGO);
                            var itemLabelGO = InstanciateUIObject("Item Label", itemGO);
                var scrollbarGO = InstanciateScrollbar("Scrollbar", templateGO, out scrollbarCmp, out scrollbarImageCmp, out scrollbarHandleImageCmp);


            // Disable template GO
            templateGO.SetActive(false);

            dropdownImageCmp = propsDropdownImage().Set(gameObject);
            dropdownCmp = propsDropdown().Set(gameObject);
            labelTextCmp = propsLabelText().Set(labelGO);
            arrowImageCmp = propsArrowImage().Set(arrowGO);
            templateImageCmp = propsTemplateImage().Set(templateGO);
            scrollRectCmp = propsScrollRect().Set(templateGO);
            viewportImageCmp = propsViewportImage().Set(viewportGO);
            maskCmp = propsMask().Set(viewportGO);
            toggleCmp = propsToggle().Set(itemGO);
            itemBackImageCmp = propsItemBackImage().Set(itemBackgroundGO);
            itemCheckImageCmp = propsItemCheckImage().Set(itemCheckmarkGO);
            itemLabelTextCmp = propsItemLabelText().Set(itemLabelGO);

            propsScrollbarImage().SetAllExceptType(scrollbarImageCmp);
            var scrollbar = propsScrollbar();
            scrollbar.Set(scrollbarCmp);
            propsScrollbarHandleImageCmp().SetAllExceptType(scrollbarHandleImageCmp);

            // Obtain percentage size
            SetReferenceSize(new RectTransformSetter());

            new RectTransformBSetter()
            {
                anchorMin = new Vector2(0, 0f),
                anchorMax = new Vector2(1, 1f),
                sizeDelta = Vector2.zero,
                offsetMin = GetPercentageSize(15,8),   // 15,8
                offsetMax = GetPercentageSize(-70, -8),  // -70, -8
            }.SetByAnchors(labelGO.GetComponent<RectTransform>());

            new RectTransformBSetter()
            {
                // pivot = new Vector2(0.5f, 1),
                localPosition = GetPercentageSize(-30, 0),  //-30, 0
                anchorMin = new Vector2(1, 0.5f),
                anchorMax = new Vector2(1, 0.5f),
                sizeDelta = GetPercentageSize(44, 44),  // 44,44
            }.SetBySizeDelta(arrowGO.GetComponent<RectTransform>());

            new RectTransformBSetter()
            {
                pivot = new Vector2(0.5f, 1),
                localPosition = new Vector2(0, -10f),  // 0,-10
                anchorMin = new Vector2(0, 0f),
                anchorMax = new Vector2(1, 0f),
                sizeDelta = new Vector2(0, 300f)  // 0,300
            }.SetBySizeDelta(templateGO.GetComponent<RectTransform>());

            new RectTransformBSetter()
            {
                pivot = new Vector2(0f, 1),
                localPosition = new Vector2(0, -150f),  // 0,-150
                anchorMin = new Vector2(0, 0f),
                anchorMax = new Vector2(1, 1f),
                sizeDelta = GetPercentageSize(-20, 0),  // -20,0
            }.SetBySizeDelta(viewportGO.GetComponent<RectTransform>());

            new RectTransformBSetter()
            {
                pivot = new Vector2(1f, 1f),
                localPosition = new Vector2(0, -150f),  // 0,-150
                anchorMin = new Vector2(1, 0f),
                anchorMax = new Vector2(1, 1f),
                sizeDelta = GetPercentageSize(scrollbar.height, 0),  // 20,0
            }.SetBySizeDelta(scrollbarGO.GetComponent<RectTransform>());

            new RectTransformBSetter()
            {
                pivot = new Vector2(.5f, 1f),
                localPosition = new Vector2(GetPercentageSize(175, -150).x, -150f), //GetPercentageSize(175, -150),  // 175,-150
                anchorMin = new Vector2(0, 1f),
                anchorMax = new Vector2(1, 1f),
                sizeDelta = GetPercentageSize(0, 60)  // 0,60
            }.SetBySizeDelta(contentGO.GetComponent<RectTransform>());

            new RectTransformBSetter()
            {
                pivot = new Vector2(.5f, .5f),
                localPosition = GetPercentageSize(0, -30),  // 0,-30
                anchorMin = new Vector2(0, .5f),
                anchorMax = new Vector2(1, .5f),
                sizeDelta = GetPercentageSize(0, 44),  // 0,44
            }.SetBySizeDelta(itemGO.GetComponent<RectTransform>());

            new RectTransformBSetter()
            {
                anchorMin = new Vector2(0, 0f),
                anchorMax = new Vector2(1, 1f),
                offsetMin = new Vector2(0, 0f),
                offsetMax = new Vector2(0f, 0F),
            }.SetByAnchors(itemBackgroundGO.GetComponent<RectTransform>());

            new RectTransformBSetter()
            {
                localPosition = GetPercentageSize(25, 0),  // 25,0
                anchorMin = new Vector2(0, .5f),
                anchorMax = new Vector2(0, .5f),
                sizeDelta = GetPercentageSize(35, 35),  // 35,35
            }.SetBySizeDelta(itemCheckmarkGO.GetComponent<RectTransform>());

            new RectTransformBSetter()
            {
                localPosition = GetPercentageSize(25, 0),  // 25,0
                anchorMin = new Vector2(0, 0f),
                anchorMax = new Vector2(1, 1f),
                sizeDelta = GetPercentageSize(-50, 0),  // -50,0
            }.SetBySizeDelta(itemLabelGO.GetComponent<RectTransform>());



            dropdownCmp.targetGraphic = dropdownImageCmp;
            dropdownCmp.template = templateGO.GetComponent<RectTransform>();
            dropdownCmp.captionText = labelTextCmp;
            dropdownCmp.itemText = itemLabelTextCmp;


            scrollRectCmp.content = contentGO.GetComponent<RectTransform>();
            scrollRectCmp.viewport = viewportGO.GetComponent<RectTransform>();
            scrollRectCmp.verticalScrollbar = scrollbarCmp;


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

        protected override REbaseSelector AddSelector() => new Selector(gameObject, idCmp, rectTransformCmp, canvasRendererCmp, dropdownCmp, dropdownImageCmp, labelTextCmp, arrowImageCmp, templateImageCmp,
                scrollRectCmp, viewportImageCmp, maskCmp, toggleCmp, itemBackImageCmp, itemCheckImageCmp, itemLabelTextCmp, scrollbarImageCmp, scrollbarCmp, scrollbarHandleImageCmp);

        protected override void AfterCreateComponent()
        {
            propsDropdown().SetListeners(dropdownCmp, (Selector)selector);
            propsScrollRect().SetListeners(scrollRectCmp, (Selector)selector);
            propsToggle().SetListeners(toggleCmp, (Selector)selector);
            propsScrollbar().SetListeners(scrollbarCmp, (Selector)selector);
        }

        #endregion Drawers


        #region Subclasses


        public class Selector : RErendererSelector
        {

            public Dropdown dropdown { get; private set; }
            public Image dropdownImage { get; private set; }
            public Text labelText { get; private set; }
            public Image arrowImage { get; private set; }
            public Image templateImage { get; private set; }
            public ScrollRect scrollRect { get; private set; }
            public Image viewportImage { get; private set; }
            public Mask mask { get; private set; }
            public Toggle toggle { get; private set; }
            public Image itemBackImage { get; private set; }
            public Image itemCheckImage { get; private set; }
            public Text itemLabelText { get; private set; }
            public Image scrollbarImage { get; private set; }
            public Scrollbar scrollbar { get; private set; }
            public Image scrollbarHandleImage { get; private set; }


            internal Selector(
                GameObject gameObject,
                HC.ReactorId pieceId,
                RectTransform rectTransform,
                CanvasRenderer canvasRenderer,
                // Extra
                Dropdown dropdown,
                Image dropdownImage,
                Text labelText,
                Image arrowImage,
                Image templateImage,
                ScrollRect scrollRect,
                Image viewportImage,
                Mask mask,
                Toggle toggle,
                Image itemBackImage,
                Image itemCheckImage,
                Text itemLabelText,
                Image scrollbarImage,
                Scrollbar scrollbar,
                Image scrollbarHandleImage
                ) : base(gameObject, pieceId, rectTransform, canvasRenderer)
            {
                this.dropdown = dropdown;
                this.dropdownImage = dropdownImage;
                this.labelText = labelText;
                this.arrowImage = arrowImage;
                this.templateImage = templateImage;
                this.scrollRect = scrollRect;
                this.viewportImage = viewportImage;
                this.mask = mask;
                this.toggle = toggle;
                this.itemBackImage = itemBackImage;
                this.itemCheckImage = itemCheckImage;
                this.itemLabelText = itemLabelText;
                this.scrollbarImage = scrollbarImage;
                this.scrollbar = scrollbar;
                this.scrollbarHandleImage = scrollbarHandleImage;
            }

            internal override void Destroy()
            {
                base.Destroy();

                dropdown = null;
                dropdownImage = null;
                labelText = null;
                arrowImage = null;
                templateImage = null;
                scrollRect = null;
                viewportImage = null;
                mask = null;
                toggle = null;
                itemBackImage = null;
                itemCheckImage = null;
                itemLabelText = null;
                scrollbarImage = null;
                scrollbar = null;
                scrollbarHandleImage = null;
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
            public override string name { get; set; } = "Dropdown";
        }
        public class RectTransformSetter : RectTransformBSetter
        {
            public override float width { get; set; } = 370;
            public override float height { get; set; } = 60;
        }

        public class DropdownSetter : DropdownBSetter<Selector>
        {

        }

        public class BackImageSetter : ImageBSetter<Selector>
        {

        }

        public class LabelTextSetter : TextBSetter
        {
            public override string text { get; set; } = "Option A";
            public override int fontSize { get; set; } = 34;
            public override HorizontalWrapMode horizontalOverflow { get; set; } = HorizontalWrapMode.Wrap;
            public override VerticalWrapMode verticalOverflow { get; set; } = VerticalWrapMode.Truncate;
            public override Color fontColor { get; set; } = new Color(.2f, .2f, .2f, 1f);
            public override TextAnchor alignment { get; set; } = TextAnchor.MiddleLeft;
        }

        public class ArrowImageSetter : ImageBSetter<Selector>
        {
            public override Color color { get; set; } = Color.gray;
        }

        public class DownBackImageSetter : ImageBSetter<Selector>
        {

        }

        public class ScrollRectSetter : ScrollRectBSetter<Selector>
        {
            public override bool horizontal { get; set; } = false;
            public override ScrollRect.MovementType movementType { get; set; } = ScrollRect.MovementType.Clamped;
            public override ScrollRect.ScrollbarVisibility verticalScrollbarVisibility { get; set; } = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
            public override float verticalScrollbarSpacing { get; set; } = -3;
        }

        public class ViewportImageSetter : ImageBSetter<Selector>
        {

        }

        public class MaskSetter : MaskBSetter
        {
            public override bool showMaskGraphic { get; set; } = false;
        }

        public class ToggleSetter : ToggleBSetter<Selector>
        {

        }

        public class ItemBackImageSetter : ImageBSetter<Selector>
        {

        }

        public class ItemCheckImageSetter : ImageBSetter<Selector>
        {
            public override Color color { get; set; } = Color.gray;
        }

        public class ItemTextSetter : TextBSetter
        {
            public override string text { get; set; } = "Option A";
            public override int fontSize { get; set; } = 34;
            public override HorizontalWrapMode horizontalOverflow { get; set; } = HorizontalWrapMode.Wrap;
            public override VerticalWrapMode verticalOverflow { get; set; } = VerticalWrapMode.Truncate;
            public override Color fontColor { get; set; } = new Color(.2f, .2f, .2f, 1f);
            public override TextAnchor alignment { get; set; } = TextAnchor.MiddleLeft;
        }

        public class ScrollbarBackImageSetter : ImageBSetter<Selector>
        {
            public override Color color { get; set; } = Color.gray;
        }

        public class ScrollbarSetter : ScrollbarBSetter<Selector>
        {
            public override Scrollbar.Direction direction { get; set; } = Scrollbar.Direction.BottomToTop;
        }

        public class ScrollbarHandleImageSetter : ImageBSetter<Selector>
        {
            public override Color color { get; set; } = new Color(0.3f, 0.3f, 0.3f, 1f);
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
                Debug.Log("REdropdown: Cant cast selector");
                return null;
            }
        }


        public new static Selector[] Find(string pattern) => Find<Selector>(pattern);

        public new static Selector[] Find() => Find<Selector>();

        public new static Selector FindOne(string pattern) => FindOne<Selector>(pattern);

        #endregion Static Funcs


    }
}
