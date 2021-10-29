using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    public class REmultiToggle : REbase
    {
        public override Type elementType => this.GetType();
        protected override Func<RectTransformBSetter> PropsRectTransform => propsRectTransform;
        protected override Func<GameObjectBSetter> PropsGameObject => propsGameObject;
        protected override Func<ReactorIdBSetter> PropsReactorId => propsReactorId;
        protected override Func<LayoutElementBSetter> PropsLayoutElement => propsLayoutElement;
        public override bool isLayoutElement => false;


        #region Components

        protected HC.MultiToggle multiToggleCmp;

        #endregion Components


        #region Setters

        // Base
        public Func<RectTransformSetter> propsRectTransform = () => new RectTransformSetter { };
        public Func<GameObjectSetter> propsGameObject = () => new GameObjectSetter();
        public Func<ReactorIdSetter> propsReactorId = () => new ReactorIdSetter();
        // Layout element
        public Func<LayoutElementSetter> propsLayoutElement;

        public Func<MultiToggleSetter> propsMultiToggle = () => new MultiToggleSetter();

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
            multiToggleCmp = propsMultiToggle().Set(gameObject);
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

        protected override REbaseSelector AddSelector() => new Selector(gameObject, reactorIdCmp, rectTransformCmp, multiToggleCmp);

        protected override void AfterCreateComponent()
        {
            List<REtoggle.Selector> toggleList = new List<REtoggle.Selector>();

            // Search al toggles in childs
            SearchToggles(selector);
            void SearchToggles(REbaseSelector selector)
            {
                //Debug.Log("Childs: " + selector.childs.Length);
                for (int i = 0; i < selector.childs.Length; i++)
                {
                    //Debug.Log("Type: " + selector.childs[i].elementId.elementType);
                    // Add all togges to List
                    try
                    {
                        toggleList.Add((REtoggle.Selector)selector.childs[i]);
                    }
                    catch (Exception)
                    {
                    }

                    SearchToggles(selector.childs[i]);
                }
            }

            var toggleDefs = toggleList.Select(t => new HC.MultiToggle.ToggleDef { toggle = t.toggle, label = t.text, name = t.multiToggleMember.toggleName, number = t.multiToggleMember.toggleNumber, value = t.multiToggleMember.toggleValue, }).ToArray();

            multiToggleCmp.toggleDefs = toggleDefs;

            // Subscrbie events
            propsMultiToggle().SetListeners(multiToggleCmp, (Selector)selector);
        }

        #endregion Drawers


        #region Subclasses

        public class Selector : REbaseSelector
        {

            public HC.MultiToggle multiToggle { get; private set; }


            internal Selector(
                GameObject gameObject,
                HC.ReactorId pieceId,
                RectTransform rectTransform,
                HC.MultiToggle multiToggle
                ) : base(gameObject, pieceId, rectTransform)
            {
                this.multiToggle = multiToggle;
            }

            internal override void Destroy()
            {
                base.Destroy();

                multiToggle = null;
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
            public override string name { get; set; } = "Multi Toggle";
        }

        public class RectTransformSetter : RectTransformBSetter
        {
            public override float width { get; set; } = 0;
            public override float height { get; set; } = 0;
            public override Vector2 anchorMin { get; set; } = Vector2.zero;
            public override Vector2 anchorMax { get; set; } = Vector2.one;
        }

        public class MultiToggleSetter : MultiToggleBSetter<Selector>
        {

        }

        #endregion


        #region Static Funcs


        public static RectTransformSetter TableRectTransform(int xPad, int xCell, int yPad, int yCell)
        {
            // Validate values
            if ((xCell < 1) && (xCell > 100)) throw new FormatException("REpanel.TableRectTransform(): xCell(" + xCell + ") must be between 0 and 100");
            if ((yCell < 1) && (yCell > 100)) throw new FormatException("REpanel.TableRectTransform(): yCell(" + yCell + ") must be between 0 and 100");
            if ((xPad < 0) && (xPad > 99)) throw new FormatException("REpanel.TableRectTransform(): xPad(" + xPad + ") must be between 0 and 99");
            if ((yPad < 0) && (xPad > 99)) throw new FormatException("REpanel.TableRectTransform(): yPad(" + yPad + ") must be between 0 and 99");
            if (xCell < xPad) throw new FormatException("REpanel.TableRectTransform(): xCell(" + xCell + ") must be greater than xPad(" + xPad + ")");
            if (yCell < yPad) throw new FormatException("REpanel.TableRectTransform(): yCell(" + yCell + ") must be greater than yPad(" + yPad + ")");

            return new RectTransformSetter
            {
                anchorMin = new Vector2(xPad / 100f, yPad / 100f),
                anchorMax = new Vector2(xCell / 100f, yCell / 100f),
            };
        }



        public new static Selector[] Find(string pattern) => Find<Selector>(pattern);

        public new static Selector[] Find() => Find<Selector>();

        public new static Selector FindOne(string pattern) => FindOne<Selector>(pattern);


        #endregion Static Funcs


    }
}
