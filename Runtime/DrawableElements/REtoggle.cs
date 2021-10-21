using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    public class REtoggle : REbase
    {
        public override Type elementType => this.GetType();
        protected override Func<RectTransformBSetter> PropsRectTransform => propsRectTransform;
        protected override Func<GameObjectBSetter> PropsGameObject => propsGameObject;
        protected override Func<ReactorIdBSetter> PropsReactorId => propsReactorId;
        protected override Func<LayoutElementBSetter> PropsLayoutElement => propsLayoutElement;
        public override bool isLayoutElement => true;


        #region Components

        protected Toggle toggleCmp;
        protected Image backImageCmp;
        protected Image checkImageCmp;
        protected Text textCmp;
        protected MultiToggleMember multiToggleMemberCmp;

        #endregion Components


        #region Properties

        // Used when multiToggle is a parent
        public string name;
        public int number;
        public float value;

        #endregion Properties


        #region Setters

        // Base
        public Func<RectTransformSetter> propsRectTransform = () => new RectTransformSetter();
        public Func<GameObjectSetter> propsGameObject = () => new GameObjectSetter();
        public Func<ReactorIdSetter> propsReactorId = () => new ReactorIdSetter();
        // Layout element
        public Func<LayoutElementSetter> propsLayoutElement;

        public Func<ToggleSetter> propsToggle = () => new ToggleSetter();
        public Func<BackImageSetter> propsBackImage = () => new BackImageSetter();
        public Func<CheckImageSetter> propsCheckImage = () => new CheckImageSetter();
        public Func<TextSeter> propsText = () => new TextSeter();
        public Func<MultiToggleMemberSetter> propsMultiToggleMember = () => new MultiToggleMemberSetter();

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
            var checkmarkGO = InstanciateUIObject("Checkmark", backgroundGO);
            var labelGO = InstanciateUIObject("Label", gameObject);


            toggleCmp = propsToggle().Set(gameObject);
            backImageCmp = propsBackImage().Set(backgroundGO);
            checkImageCmp = propsCheckImage().Set(checkmarkGO);
            textCmp = propsText().Set(labelGO);
            multiToggleMemberCmp = propsMultiToggleMember().Set(gameObject);

            toggleCmp.targetGraphic = backImageCmp;
            toggleCmp.graphic = checkImageCmp;



            // backgroundGO rect
            new RectTransformBSetter()
            {
                //pivot = new Vector2(0f, 1f),
                localPosition = new Vector2(40, -40f),
                sizeDelta = new Vector2(80, 80f),
                anchorMin = new Vector2(0f, 1f),
                anchorMax = new Vector2(0f, 1f),
            }.SetOrSearchBySizeDelta(backgroundGO);

            new RectTransformBSetter()
            {
                //pivot = new Vector2(0f, 1f),
                //localPosition = new Vector2(0, 0f),
                sizeDelta = new Vector2(80, 80f),
                //anchorMin = new Vector2(0f, 1f),
                //anchorMax = new Vector2(0f, 1f),
            }.SetOrSearchBySizeDelta(checkmarkGO);

            new RectTransformBSetter()
            {
                anchorMin = new Vector2(0f, 0f),
                anchorMax = new Vector2(1f, 1f),
                offsetMin = new Vector2(90f, 10f),
                offsetMax = new Vector2(0f, -10f),
            }.SetOrSearchByAnchors(labelGO);


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

        protected override REbaseSelector AddSelector() => new Selector(gameObject, reactorIdCmp, rectTransformCmp, toggleCmp, backImageCmp, checkImageCmp, textCmp, multiToggleMemberCmp);

        protected override void AfterCreateComponent()
        {
            propsToggle().SetListeners(toggleCmp, (Selector)selector);
            propsBackImage().SetListeners(backImageCmp, (Selector)selector);
            propsCheckImage().SetListeners(checkImageCmp, (Selector)selector);
            propsText().SetListeners(textCmp, (Selector)selector);
        }

        #endregion Drawers


        #region Subclasses

        public class Selector : REbaseSelector
        {

            public Toggle toggle { get; private set; }
            public Image backImage { get; private set; }
            public Image checkImage { get; private set; }
            public Text text { get; private set; }
            public MultiToggleMember multiToggleMember { get; private set; }


            internal Selector(
                GameObject gameObject,
                ReactorId pieceId,
                RectTransform rectTransform,
                Toggle toggle,
                Image backImage,
                Image checkImage,
                Text text,
                MultiToggleMember multiToggleMember
                ) : base(gameObject, pieceId, rectTransform)
            {
                this.toggle = toggle;
                this.backImage = backImage;
                this.checkImage = checkImage;
                this.text = text;
                this.multiToggleMember = multiToggleMember;
            }

            internal override void Destroy()
            {
                base.Destroy();

                toggle = null;
                backImage = null;
                checkImage = null;
                text = null;
                multiToggleMember = null;
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

        public class MultiToggleMemberSetter : MultiToggleMemberBSetter
        {

        }

        public class ReactorIdSetter : ReactorIdBSetter
        {

        }

        public class GameObjectSetter : GameObjectBSetter
        {
            public override string name { get; set ; } = "Toggle";
        }

        public class RectTransformSetter : RectTransformBSetter
        {
            public override float width { get; set; } = 600;
            public override float height { get; set; } = 80;
        }

        public class ToggleSetter : ToggleBSetter<Selector>
        {

        }

        public class BackImageSetter: ImageBSetter<Selector>
        {

        }

        public class CheckImageSetter : ImageBSetter<Selector>
        {
            public override Color color { get; set; } = Color.gray;
        }

        public class TextSeter : TextBSetter<Selector>
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
