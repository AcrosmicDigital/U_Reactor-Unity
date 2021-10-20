using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    public class REdropdown : REchild
    {
        protected override Type elementType => this.GetType();
        protected override string elementName => "Dropdown";
        protected override Func<RectTransformBSetter> PropsRectTransform { get => propsRectTransform; }


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

        public Func<RectTransformSetter> propsRectTransform = () => new RectTransformSetter();
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
            propsScrollbar().Set(scrollbarCmp);
            propsScrollbarHandleImageCmp().SetAllExceptType(scrollbarHandleImageCmp);


            new RectTransformBSetter()
            {
                anchorMin = new Vector2(0, 0f),
                anchorMax = new Vector2(1, 1f),
                sizeDelta = Vector2.zero,
                offsetMin = new Vector2(15, 15f),
                offsetMax = new Vector2(-70f, -15F),
            }.SetByAnchors(labelGO.GetComponent<RectTransform>());

            new RectTransformBSetter()
            {
                // pivot = new Vector2(0.5f, 1),
                localPosition = new Vector2(-40f, 0),
                anchorMin = new Vector2(1, 0.5f),
                anchorMax = new Vector2(1, 0.5f),
                sizeDelta = new Vector2(60, 60f),
            }.SetBySizeDelta(arrowGO.GetComponent<RectTransform>());

            new RectTransformBSetter()
            {
                pivot = new Vector2(0.5f, 1),
                localPosition = new Vector2(0f, -10f),
                anchorMin = new Vector2(0, 0f),
                anchorMax = new Vector2(1, 0f),
                sizeDelta = new Vector2(0, 300f),
            }.SetBySizeDelta(templateGO.GetComponent<RectTransform>());

            new RectTransformBSetter()
            {
                pivot = new Vector2(0f, 1),
                localPosition = new Vector2(0f, -150f),
                anchorMin = new Vector2(0, 0f),
                anchorMax = new Vector2(1, 1f),
                sizeDelta = new Vector2(-20, 0),
            }.SetBySizeDelta(viewportGO.GetComponent<RectTransform>());

            new RectTransformBSetter()
            {
                pivot = new Vector2(1f, 1f),
                localPosition = new Vector2(0f, -150f),
                anchorMin = new Vector2(1, 0f),
                anchorMax = new Vector2(1, 1f),
                sizeDelta = new Vector2(20, 0),
            }.SetBySizeDelta(scrollbarGO.GetComponent<RectTransform>());

            new RectTransformBSetter()
            {
                pivot = new Vector2(.5f, 1f),
                localPosition = new Vector2(190f, -150f),
                anchorMin = new Vector2(0, 1f),
                anchorMax = new Vector2(1, 1f),
                sizeDelta = new Vector2(0f, 80f),
            }.SetBySizeDelta(contentGO.GetComponent<RectTransform>());

            new RectTransformBSetter()
            {
                pivot = new Vector2(.5f, .5f),
                localPosition = new Vector2(0f, -40f),
                anchorMin = new Vector2(0, .5f),
                anchorMax = new Vector2(1, .5f),
                sizeDelta = new Vector2(0f, 50f),
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
                localPosition = new Vector2(25f, 0f),
                anchorMin = new Vector2(0, .5f),
                anchorMax = new Vector2(0, .5f),
                sizeDelta = new Vector2(50f, 50f),
            }.SetBySizeDelta(itemCheckmarkGO.GetComponent<RectTransform>());

            new RectTransformBSetter()
            {
                localPosition = new Vector2(25f, 0f),
                anchorMin = new Vector2(0, 0f),
                anchorMax = new Vector2(1, 1f),
                sizeDelta = new Vector2(-50f, 0f),
            }.SetBySizeDelta(itemLabelGO.GetComponent<RectTransform>());



            dropdownCmp.template = templateGO.GetComponent<RectTransform>();
            dropdownCmp.captionText = labelTextCmp;
            dropdownCmp.itemText = itemLabelTextCmp;


            scrollRectCmp.content = contentGO.GetComponent<RectTransform>();
            scrollRectCmp.viewport = viewportGO.GetComponent<RectTransform>();
            scrollRectCmp.verticalScrollbar = scrollbarCmp;



            dropdownCmp.options = new List<Dropdown.OptionData> 
            {
                new Dropdown.OptionData
                {
                    text = "Item1",
                },
                new Dropdown.OptionData
                {
                    text = "Item2",
                },
                new Dropdown.OptionData
                {
                    text = "Item3",
                },
                new Dropdown.OptionData
                {
                    text = "Item4",
                },
                new Dropdown.OptionData
                {
                    text = "Item5",
                },
                new Dropdown.OptionData
                {
                    text = "Item6",
                },
                new Dropdown.OptionData
                {
                    text = "Item7",
                },
                new Dropdown.OptionData
                {
                    text = "Item8",
                },
            };
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
            var sel = new Selector(gameObject, elementIdCmp, rectTransformCmp, canvasRendererCmp, dropdownCmp, dropdownImageCmp, labelTextCmp, arrowImageCmp, templateImageCmp,
                scrollRectCmp, viewportImageCmp, maskCmp, toggleCmp, itemBackImageCmp, itemCheckImageCmp, itemLabelTextCmp, scrollbarImageCmp, scrollbarCmp, scrollbarHandleImageCmp);

            return sel;
        }

        #endregion Drawers


        #region Subclasses


        public class Selector : ChildElementSelector
        {

            protected Dropdown dropdown { get; private set; }
            protected Image dropdownImage { get; private set; }
            protected Text labelText { get; private set; }
            protected Image arrowImage { get; private set; }
            protected Image templateImage { get; private set; }
            protected ScrollRect scrollRect { get; private set; }
            protected Image viewportImage { get; private set; }
            protected Mask mask { get; private set; }
            protected Toggle toggle { get; private set; }
            protected Image itemBackImage { get; private set; }
            protected Image itemCheckImage { get; private set; }
            protected Text itemLabelText { get; private set; }
            protected Image scrollbarImage { get; private set; }
            protected Scrollbar scrollbar { get; private set; }
            protected Image scrollbarHandleImage { get; private set; }


            internal Selector(
                GameObject gameObject,
                ReactorId pieceId,
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

        public class RectTransformSetter : RectTransformBSetter
        {
            public override float width { get; set; } = 400;
            public override float height { get; set; } = 70;
        }

        public class DropdownSetter : DropdownBSetter
        {

        }

        public class BackImageSetter : ImageBSetter
        {

        }

        public class LabelTextSetter : TextBSetter
        {
            public override string text { get; set; } = "Option A";
            public override int fontSize { get; set; } = 35;
            public override HorizontalWrapMode horizontalOverflow { get; set; } = HorizontalWrapMode.Wrap;
            public override VerticalWrapMode verticalOverflow { get; set; } = VerticalWrapMode.Truncate;
            public override Color fontColor { get; set; } = new Color(.2f, .2f, .2f, 1f);
        }

        public class ArrowImageSetter : ImageBSetter
        {
            public override Color color { get; set; } = Color.gray;
        }

        public class DownBackImageSetter : ImageBSetter
        {

        }

        public class ScrollRectSetter : ScrollRectBSetter
        {
            public override bool horizontal { get; set; } = false;
            public override ScrollRect.MovementType movementType { get; set; } = ScrollRect.MovementType.Clamped;
            public override ScrollRect.ScrollbarVisibility verticalScrollbarVisibility { get; set; } = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
            public override float verticalScrollbarSpacing { get; set; } = -3;
        }

        public class ViewportImageSetter : ImageBSetter
        {

        }

        public class MaskSetter : MaskBSetter
        {
            public override bool showMaskGraphic { get; set; } = false;
        }

        public class ToggleSetter : ToggleBSetter
        {

        }

        public class ItemBackImageSetter : ImageBSetter
        {

        }

        public class ItemCheckImageSetter : ImageBSetter
        {
            public override Color color { get; set; } = Color.gray;
        }

        public class ItemTextSetter : TextBSetter
        {
            public override string text { get; set; } = "Option A";
            public override int fontSize { get; set; } = 35;
            public override HorizontalWrapMode horizontalOverflow { get; set; } = HorizontalWrapMode.Wrap;
            public override VerticalWrapMode verticalOverflow { get; set; } = VerticalWrapMode.Truncate;
            public override Color fontColor { get; set; } = new Color(.2f, .2f, .2f, 1f);
        }

        public class ScrollbarBackImageSetter : ImageBSetter
        {
            public override Color color { get; set; } = Color.gray;
        }

        public class ScrollbarSetter : ScrollbarBSetter
        {
            public override Scrollbar.Direction direction { get; set; } = Scrollbar.Direction.BottomToTop;
        }

        public class ScrollbarHandleImageSetter : ImageBSetter
        {
            public override Color color { get; set; } = new Color(0.3f, 0.3f, 0.3f, 1f);
        }

        #endregion


        #region Static Funcs



        public new static Selector[] Find(string pattern) => Find<Selector>(pattern);

        public new static Selector[] Find() => Find<Selector>();

        public new static Selector FindOne(string pattern) => FindOne<Selector>(pattern);

        #endregion Static Funcs


    }
}
