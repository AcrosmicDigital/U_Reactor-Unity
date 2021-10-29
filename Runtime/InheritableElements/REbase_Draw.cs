using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    // Add URelementId, RectTransform
    public abstract partial class REbase
    {
        public abstract Type elementType { get; }  // The type of the element, each element must everride
        protected abstract Func<RectTransformBSetter> PropsRectTransform { get; }
        protected abstract Func<GameObjectBSetter> PropsGameObject { get; }
        protected abstract Func<ReactorIdBSetter> PropsReactorId { get; }
        protected abstract Func<LayoutElementBSetter> PropsLayoutElement { get; }
        public abstract bool isLayoutElement { get; }


        #region Components

        protected RectTransform rectTransformCmp;
        protected HC.ReactorId reactorIdCmp;

        #endregion Components


        #region Setters

        public Func<IEnumerable<REbase>> childs = () => new REbase[0];

        #endregion


        #region Properties

        protected GameObject virtualChildContainer { get; set; }  // If childs mustbe created in a subobject set this value
        protected GameObject gameObject { get; private set; }
        protected GameObject parent { get; private set; } // The parent GameObject
        protected List<REbase> childsList = new List<REbase>();
        protected bool isDisabled = false;
        internal REbaseSelector selector;
        protected REbaseSelector parentSelector;

        #endregion Properties


        #region Hooks

        public IuseState[] useState;
        public UseTrigger.Hook[] useTrigger;
        public IuseAddChilds[] useAddChilds;

        #endregion Hooks


        #region Drawers

        /// <summary>
        /// Create a new gameobject with all properties and comonents, and destroy the old one
        /// </summary>
        /// <param name="parent">Parent gameobject</param>
        protected virtual void CreateRoot(GameObject parent)
        {

            // Check if the gameObject exist and delete it to draw again, but save childNumber
            var siblingIndex = -1;
            if (gameObject != null)
            {
                isDisabled = !gameObject.activeSelf;
                if (gameObject.transform.parent != null)
                    siblingIndex = gameObject.transform.GetSiblingIndex();
                UnityEngine.Object.DestroyImmediate(gameObject);
            }

            // Crate the gameObject
            if (gameObject == null)
                gameObject = new GameObject();

            // Set the name, layer and tag
            PropsGameObject().SetNameLayerAndTag(gameObject);

            // Set the parent if exist
            this.parent = parent;
            if (parent != null)
                gameObject.transform.SetParent(parent.transform);

            // Set the childNumber if necesary
            if (siblingIndex >= 0)
            {
                try
                {
                    gameObject.transform.SetSiblingIndex(siblingIndex);
                }
                catch (Exception)
                {
                }
            }

            // Add rectTransform and components
            var propsRectT = PropsRectTransform();
            rectTransformCmp = propsRectT.SetOrSearchByWidthAndHeight(gameObject);
            SetRealSize(propsRectT);
            reactorIdCmp = PropsReactorId().Set(gameObject);
            reactorIdCmp.SetElementType(elementType);
        }


        // Create all the component , call other functions in order
        protected REbase Draw(GameObject parent, REbaseSelector parentSelector)
        {
            
            // Functions to create parts of the component
            Erase();
            CreateRoot(parent);
            AddComponents();
            this.parentSelector = parentSelector;
            this.selector = AddSelector();
            selector.SetParent(parentSelector);
            selector.layoutElementSetter = PropsLayoutElement;
            selector.isLayoutElement = isLayoutElement;
            AddHooks();

            // Create childs
            childsList.Clear();

            IEnumerable<REbase> newChilds = null;
            try { newChilds = childs(); }
            catch (ArgumentNullException) { newChilds = null; }
            catch (NullReferenceException) { newChilds = null; }

            if (newChilds != null)
            {
                foreach (var child in newChilds)
                {
                    CreateChild(child);
                }
            }
            selector.SetChilds(childsList);

            AfterCreateComponent();

            //// Wait for ui to update values
            if (Application.isPlaying)
            {
                var host = new GameObject();
                UnityEngine.Object.DontDestroyOnLoad(host);
                host.AddComponent<NoBehaviour>().StartCoroutine(WaitForDraw()); IEnumerator WaitForDraw()
                {
                    yield return new WaitForEndOfFrame();  // Wait for rect values to be calculated
                    
                    bool shouldEnable = PropsGameObject().active;

                    if (isDisabled)
                        shouldEnable = false;

                    if (gameObject != null)
                        gameObject.SetActive(shouldEnable);

                    AfterRenderComponent(); // Run after create function

                    UnityEngine.Object.Destroy(host);
                }
            }
            else
            {
                WaitForDraw(); async void WaitForDraw()
                {
                    await Task.Delay(1000); // Wait for rect values to be calculated
                    AfterRenderComponent(); // Run after create function
                    gameObject.SetActive(PropsGameObject().active); // Enable or disable
                }
            }

            // Subscribe to useState events
            if (useState != null)
            {
                foreach (var state in useState)
                {
                    if (state == null)
                        continue;

                    state.OnStateChange -= OnUseStateChange;
                    state.OnStateChange += OnUseStateChange;

                }
            }

            // Subscribe to useAddChilds events
            if (useAddChilds != null)
            {
                foreach (var h in useAddChilds)
                {
                    if (h == null)
                        continue;

                    h.OnChildAdded -= OnUseChildAdded;
                    h.OnChildAdded += OnUseChildAdded;

                }
            }

            // Subscribe to useTrigger events
            if (useTrigger != null)
            {
                foreach (var h in useTrigger.Select(p => p.hook))
                {
                    if (h == null)
                        continue;

                    h.OnTrigger -= OnTrigger;
                    h.OnTrigger += OnTrigger;

                }
            }

            // Add Id
            selector.SetErase(Erase);
            selector.SetEraseChilds(EraseChilds);
            reactorIdCmp.SetSelector(selector);

            return this;
        }

        private void OnTrigger(object sender, OnTriggerEventArgs e)
        {
            // Search the action trigger
            var action = useTrigger.Where(t => t.hook.id == e.id).Select(t => t.OnTrigger).FirstOrDefault();

            // Invoke the action
            try
            {
                action?.Invoke(selector);
            }
            catch (Exception ex)
            {
                Debug.LogError("UReactor.REbase.OnTrigger(): Error while invoking action, " + ex);
            }

        }


        private void OnUseChildAdded(object sender, OnChildAddedEventArgs e)
        {
            try
            {
                CreateChild(e.child);
            }
            catch (Exception ex)
            {
                Debug.LogError("UReactor.REbase.OnUseChildAdded(): Error while adding child, " + ex);
            }
        }

        // Function to subscribe to useState event
        private void OnUseStateChange(object sender, EventArgs e)
        {
            Draw(parent, parentSelector);
        }

        // Function to subscribe to useAddChilds event
        private void CreateChild(REbase child)
        {
            if (child == null) return;

            // Only if create child condition is true
            if (!CrateChildCondition(child)) return;

            if (virtualChildContainer != null)
                childsList.Add(child.Draw(virtualChildContainer, selector));
            else
                childsList.Add(child.Draw(gameObject, selector));

            AfterCreateChild(child.selector);
        }

        

        protected abstract void AddComponents();

        protected abstract REbaseSelector AddSelector();

        protected abstract void AddHooks();

        protected virtual bool CrateChildCondition(REbase child) => true;

        protected virtual void AfterCreateChild(REbaseSelector child) { }

        protected virtual void AfterCreateComponent() { }

        protected virtual void AfterRenderComponent() { }

        public virtual void Erase()
        {
            // Unsubscribe to useState events
            if (useState != null)
            {
                foreach (var state in useState)
                {
                    if (state == null)
                        continue;

                    state.OnStateChange -= OnUseStateChange;

                }
            }

            // Unsubscribe to useAddChilds events
            if (useAddChilds != null)
            {
                foreach (var h in useAddChilds)
                {
                    if (h == null)
                        continue;

                    h.OnChildAdded -= OnUseChildAdded;

                }
            }

            // Destroy each child
            if (childsList != null)
            {
                foreach (var child in childsList)
                {
                    if (child != null)
                    {
                        child.Erase();
                    }
                }
            }
            
            // Destroy all the components and selectors
            if(reactorIdCmp != null)
                UnityEngine.Object.DestroyImmediate(reactorIdCmp);

            // Destroy the selector
            if (selector != null)
                selector.Destroy();

            // Remove this from parent child list
            if (parentSelector != null) if(!parentSelector.isDisposed) parentSelector.RemoveChild(this);

            // Destroy gameobject
            if (gameObject != null)
                UnityEngine.Object.Destroy(gameObject); // Esta la puse en vez de la de abajo comentada, para evaluar posibles errores
                                                        //UnityEngine.Object.DestroyImmediate(gameObject);

        }

        public virtual void EraseChilds()
        {
            foreach (var child in childsList)
            {
                child.Erase();
            }
        }

        #endregion Drawers


    }

}
