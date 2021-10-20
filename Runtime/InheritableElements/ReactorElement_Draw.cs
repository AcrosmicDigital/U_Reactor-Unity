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
        protected abstract Type elementType { get; }  // The type of the element, each element must everride
        protected abstract Func<RectTransformBSetter> PropsRectTransform { get; }
        protected abstract Func<GameObjectBSetter> PropsGameObject { get; }
        protected abstract Func<ReactorIdBSetter> PropsReactorId { get; }




        #region <Components>

        protected RectTransform rectTransformCmp;
        protected ReactorId reactorIdCmp; // Id to find the element

        #endregion </Components>


        #region <Properties>

        protected GameObject virtualChildContainer { get; set; }  // If childs mustbe created in a subobject set this value
        protected GameObject gameObject { get; private set; }
        protected GameObject parent { get; private set; } // The parent GameObject
        protected List<REbase> childsList = new List<REbase>();
        protected bool isDisabled = false;
        public Func<IEnumerable<REbase>> childs = () => new REbase[0];

        #endregion </Properties>


        #region <Hooks>

        internal ElementSelector selector;
        protected ElementSelector parentSelector;
        public IuseState[] useState;

        #endregion </Hooks>


        // Create all the component , call other functions in order
        protected REbase Create(GameObject parent, ElementSelector parentSelector)
        {
            
            // Functions to create parts of the component
            Destroy();
            CreateRoot(parent);
            AddComponents();
            this.parentSelector = parentSelector;
            this.selector = AddSelector();
            selector.SetParent(parentSelector);
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
                    if (child == null)
                        continue;
                    
                    if(virtualChildContainer != null)
                        childsList.Add(child.Create(virtualChildContainer, selector));
                    else
                        childsList.Add(child.Create(gameObject, selector));

                    AfterCreateChild(child.gameObject);
                    
                }
            }
            selector.SetChilds(childsList);

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

                    AfterCreateComponent(); // Run after create function

                    UnityEngine.Object.Destroy(host);
                }
            }
            else
            {
                WaitForDraw(); async void WaitForDraw()
                {
                    await Task.Delay(1000); // Wait for rect values to be calculated
                    AfterCreateComponent(); // Run after create function
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

            // Add Id
            reactorIdCmp.Set(selector);

            return this;
        }

        // Function to subscribe to useState event
        void OnUseStateChange(object sender, EventArgs e)
        {
            Create(parent, parentSelector);
        }


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

            // Create ainstance of gameObject properties
            var propsGo = PropsGameObject();

            // Set the name
            gameObject.name = propsGo.name;

            // Set the parent if exist
            this.parent = parent;
            if (parent != null)
                gameObject.transform.SetParent(parent.transform);

            // Set the childNumber if necesary
            if (siblingIndex >= 0)
                gameObject.transform.SetSiblingIndex(siblingIndex);

            // Set the layer
            gameObject.layer = propsGo.layer;

            // Set the tag
            if (!String.IsNullOrEmpty(propsGo.tag) && !String.IsNullOrWhiteSpace(propsGo.tag))
                gameObject.tag = propsGo.tag;

            // Add rectTransform
            rectTransformCmp = PropsRectTransform().Set(gameObject);
            reactorIdCmp = PropsReactorId().Set(elementType, gameObject);
        }

        protected abstract void AddComponents();

        protected abstract ElementSelector AddSelector();

        protected abstract void AddHooks();

        protected virtual void AfterCreateChild(GameObject child) { }

        protected virtual void AfterCreateComponent() { }

        protected void Destroy()
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

            if (childsList != null)
            {
                foreach (var child in childsList)
                {
                    if (child != null)
                    {
                        child.Destroy();
                    }
                }
            }
            
            if(reactorIdCmp != null)
                UnityEngine.Object.DestroyImmediate(reactorIdCmp);

            if (selector != null)
                selector.Destroy();

        }

    }

}
