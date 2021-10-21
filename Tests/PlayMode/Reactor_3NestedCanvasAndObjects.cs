using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using U.Reactor;
using UnityEngine.EventSystems;
using System;
using System.Linq;

public class Reactor_3NestedCanvasAndObjects
{


    GameObject mainCamera = null;
    GameObject eventSystem = null;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        mainCamera = new GameObject("Main Camera");
        var camera = mainCamera.AddComponent<Camera>();
        camera.backgroundColor = Color.grey;
        camera.orthographic = true;
        camera.depth = -1;
        mainCamera.AddComponent<AudioListener>();
        eventSystem = new GameObject("EventSystem");
        eventSystem.AddComponent<EventSystem>();
        eventSystem.AddComponent<StandaloneInputModule>();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        UnityEngine.Object.Destroy(mainCamera);
        UnityEngine.Object.Destroy(eventSystem);
    }




    [UnityTest]
    public IEnumerator DrawInOtherObject()
    {
        // Create a reactor component 
        REcanvas MainReactorComponent()
        {

            return new REcanvas
            {

                childs = () => new REbase[] {
                        new REtext {
                            propsReactorId = () => new REtext.ReactorIdSetter{
                                id = "ProveText"
                            },
                            propsText = () => new REtext.TextSetter {
                                text = "Hello world",
                            },
                            childs = () => new REbase[]
                            {
                                new REimage(),
                                new REimage(),
                            }
                        },
                        new REtext(),
                        new REtext(),
                        new REtext(),
                    },

            };

        }
        var reactorComponent = MainReactorComponent();

        yield return new WaitForSecondsRealtime(1);

        // Draw the component for the first time but in other object
        Debug.Log("Drawing");
        reactorComponent.Draw(new GameObject("AnyObject"));

        yield return new WaitForSecondsRealtime(1);

        // Look fr the component
        var textSelector = REtext.Find("#ProveText")[0];

        // The transform root must be the other object
        Assert.IsTrue(REtext.Find("#ProveText").Length == 1);
        Assert.IsTrue(textSelector.rectTransform.root.gameObject.name == "AnyObject");

        // If component is redrawed the same parent must be in same object, but must be finded again
        reactorComponent.Draw(new GameObject("OtherObject"));
        textSelector = REtext.Find("#ProveText")[0];

        // The transform root must be the other object
        Assert.IsTrue(REtext.Find("#ProveText").Length == 1);
        Assert.IsTrue(textSelector.rectTransform.root.gameObject.name == "OtherObject");

        // If component is redrawed in null
        reactorComponent.Draw();
        textSelector = REtext.Find("#ProveText")[0];

        // The transform root must be the canvas
        Assert.IsTrue(REtext.Find("#ProveText").Length == 1);
        Assert.IsTrue(textSelector.rectTransform.root.gameObject.name == "Canvas-Unamed");


        reactorComponent.Erase();

    }

    [UnityTest]
    public IEnumerator DrawInOtherObject_ButObjectDestroyed()
    {
        // Create a reactor component 
        REcanvas MainReactorComponent()
        {

            return new REcanvas
            {

                childs = () => new REbase[] {
                        new REtext {
                            propsReactorId = () => new REtext.ReactorIdSetter{
                                id = "ProveText"
                            },
                            propsText = () => new REtext.TextSetter {
                                text = "Hello world",
                            },
                            childs = () => new REbase[]
                            {
                                new REimage(),
                                new REimage(),
                            }
                        },
                        new REtext(),
                        new REtext(),
                        new REtext(),
                    },

            };


        }
        var reactorComponent = MainReactorComponent();

        yield return new WaitForSecondsRealtime(1);

        // Draw the component for the first time but in other object
        Debug.Log("Drawing");
        reactorComponent.Draw(new GameObject("AnyObject"));

        yield return new WaitForSecondsRealtime(1);

        // Look fr the component
        Debug.Log("Finding");
        var textSelector = REtext.Find("#ProveText")[0];

        // The transform root must be the other object
        Assert.IsTrue(REtext.Find("#ProveText").Length == 1);
        Assert.IsTrue(textSelector.rectTransform.root.gameObject.name == "AnyObject");

        // If component is redrawed the same parent must be in same object, but must be finded again
        Debug.Log("Drawing in other object");
        var go = new GameObject("OtherObject");
        reactorComponent.Draw(go);
        textSelector = REtext.Find("#ProveText")[0];

        // The transform root must be the other object
        Debug.Log("Finding again");
        Assert.IsTrue(REtext.Find("#ProveText").Length == 1);
        Assert.IsTrue(textSelector.rectTransform.root.gameObject.name == "OtherObject");

        // If object destroyed
        Debug.Log("Destroying object");
        UnityEngine.Object.DestroyImmediate(go);

        // Reactor component will be destroyed too
        Assert.IsTrue(REtext.Find("#ProveText").Length == 0);

        // Check that the selector references are now null
        Debug.Log("Checking selectoris destroyed");
        Assert.IsTrue(textSelector.isDisposed);
        Assert.IsTrue(textSelector.rectTransform == null);
        Assert.IsTrue(textSelector.gameObject == null);
        Assert.IsTrue(textSelector.parent == null);
        Assert.IsTrue(textSelector.childs == null);
        Assert.IsTrue(textSelector.brothersSelector == null);
        Assert.IsTrue(textSelector.canvasRenderer == null);
        Assert.Throws<NullReferenceException>(() => Debug.Log("V: " + textSelector.rectTransform.rect));

        reactorComponent.Erase();
    }

    [UnityTest]
    public IEnumerator DrawInOtherObject_Selector_Root()
    {
        // Create a reactor component 
        REcanvas MainReactorComponent()
        {

            return new REcanvas
            {

                childs = () => new REbase[] {
                        new REtext {
                            propsReactorId = () => new REtext.ReactorIdSetter{
                                id = "ProveText"
                            },
                            propsText = () => new REtext.TextSetter {
                                text = "Hello world",
                            },
                            childs = () => new REbase[]
                            {
                                new REimage(),
                                new REimage(),
                            }
                        },
                        new REtext(),
                        new REtext(),
                        new REtext(),
                    },

            };

        }
        var reactorComponent = MainReactorComponent();

        yield return new WaitForSecondsRealtime(1);

        // Draw the component for the first time but in other object
        Debug.Log("Drawing");
        reactorComponent.Draw(new GameObject("AnyObject"));

        yield return new WaitForSecondsRealtime(1);

        // Look fr the component
        var textSelector = REtext.Find("#ProveText")[0];

        // The transform root must be the other object
        Assert.IsTrue(REtext.Find("#ProveText").Length == 1);
        Assert.IsTrue(textSelector.rectTransform.root.gameObject.name == "AnyObject");

        // But the selector root must be the canvas, but the canvas parent must be the canvas too
        Assert.IsTrue(textSelector.rootCanvasSelector.gameObject.name == "Canvas-Unamed");
        Assert.IsTrue(textSelector.parent.gameObject.name == "Canvas-Unamed");
        Assert.IsTrue(textSelector.parent.parent == null);
        var canvasSelector = (REcanvas.Selector)textSelector.rootCanvasSelector;


        // If component is redrawed in null
        reactorComponent.Draw();
        textSelector = REtext.Find("#ProveText")[0];

        // The transform root must be the canvas
        Assert.IsTrue(REtext.Find("#ProveText").Length == 1);
        Assert.IsTrue(textSelector.rectTransform.root.gameObject.name == "Canvas-Unamed");

        // And the selector root must be the canvas too
        Assert.IsTrue(textSelector.rootCanvasSelector.gameObject.name == "Canvas-Unamed");
        Assert.IsTrue(textSelector.parent.gameObject.name == "Canvas-Unamed");
        Assert.IsTrue(textSelector.parent.parent == null);
        canvasSelector = (REcanvas.Selector)textSelector.rootCanvasSelector;

        reactorComponent.Erase();
    }

    [UnityTest]
    public IEnumerator DrawInOtherObjectNestedCanvas_Selector_Root()
    {
        // Create a reactor component 
        REcanvas MainReactorComponent()
        {

            return new REcanvas
            {
                propsGameObject = () => new REcanvas.GameObjectSetter
                {
                    name = "RootCanvas",
                },
                childs = () => new REbase[] {
                        new REcanvas
                        {
                            childs = () => new REbase[] {
                                new REtext {
                                    propsReactorId = () => new REtext.ReactorIdSetter{
                                        id = "ProveText"
                                    },
                                    propsText = () => new REtext.TextSetter {
                                        text = "Hello world",
                                    },
                                    childs = () => new REbase[]
                                    {
                                        new REimage(),
                                        new REimage(),
                                    }
                                },
                                new REtext(),
                                new REtext(),
                                new REtext(),
                            },
                        }
                    },

            };

        }
        var reactorComponent = MainReactorComponent();

        yield return new WaitForSecondsRealtime(1);

        // Draw the component for the first time but in other object
        Debug.Log("Drawing");
        reactorComponent.Draw(new GameObject("AnyObject"));

        yield return new WaitForSecondsRealtime(1);

        // Look fr the component
        Debug.Log("Finding");
        var textSelector = REtext.Find("#ProveText")[0];

        // The transform root must be the other object
        Assert.IsTrue(REtext.Find("#ProveText").Length == 1);
        Assert.IsTrue(textSelector.rectTransform.root.gameObject.name == "AnyObject");

        // But the selector root must be the canvas, but the canvas parent must be the canvas too
        Debug.Log("Check root");
        Assert.IsTrue(textSelector.rootCanvasSelector.gameObject.name == "RootCanvas");
        Assert.IsTrue(textSelector.rootCanvasSelector.rootCanvasSelector.gameObject.name == "RootCanvas");
        Assert.IsTrue(textSelector.rootCanvasSelector.rootCanvasSelector.rootCanvasSelector.gameObject.name == "RootCanvas");
        Assert.IsTrue(textSelector.parent.gameObject.name == "Canvas-Unamed");
        Assert.IsTrue(textSelector.parent.parent.parent == null);
        var canvasSelector = (REcanvas.Selector)textSelector.rootCanvasSelector;


        // If component is redrawed in null
        Debug.Log("Drawing in null");
        reactorComponent.Draw();
        textSelector = REtext.Find("#ProveText")[0];

        // The transform root must be the canvas
        Debug.Log("Checking root");
        Assert.IsTrue(REtext.Find("#ProveText").Length == 1);
        Debug.Log("Checking root");
        Assert.IsTrue(textSelector.rectTransform.root.gameObject.name == "RootCanvas");
        Debug.Log("Checking root");
        // And the selector root must be the canvas too
        Assert.IsTrue(textSelector.rootCanvasSelector.gameObject.name == "RootCanvas"); 
        Assert.IsTrue(textSelector.parent.gameObject.name == "Canvas-Unamed"); 
        Assert.IsTrue(textSelector.parent.parent.parent == null); 
        canvasSelector = (REcanvas.Selector)textSelector.rootCanvasSelector; 

        reactorComponent.Erase();
    }


    [UnityTest]
    public IEnumerator DrawInOtherObjectNestedCanvas_Selector_ParentCanvas()
    {
        // Create a reactor component 
        REcanvas MainReactorComponent()
        {

            return new REcanvas
            {
                propsGameObject = () => new REcanvas.GameObjectSetter
                {
                    name = "RootCanvas",
                },
                childs = () => new REbase[] {
                        new REcanvas
                        {
                            childs = () => new REbase[] {
                                new REpanel
                                {
                                    childs = () => new REbase[] {
                                        new REtext {
                                            propsReactorId = () => new REtext.ReactorIdSetter{
                                                id = "ProveText"
                                            },
                                            propsText = () => new REtext.TextSetter {
                                                text = "Hello world",
                                            },
                                            childs = () => new REbase[]
                                            {
                                                new REimage(),
                                                new REimage(),
                                            }
                                        },
                                        new REtext(),
                                        new REtext(),
                                        new REtext(),
                                    },
                                }
                            },
                        }
                    },

            };

        }
        var reactorComponent = MainReactorComponent();

        yield return new WaitForSecondsRealtime(1);

        // Draw the component for the first time but in other object
        Debug.Log("Drawing");
        reactorComponent.Draw(new GameObject("AnyObject"));

        yield return new WaitForSecondsRealtime(1);

        // Look fr the component
        Debug.Log("Finding");
        var textSelector = REtext.Find("#ProveText")[0];

        // The transform root must be the other object
        Assert.IsTrue(REtext.Find("#ProveText").Length == 1);
        Assert.IsTrue(textSelector.rectTransform.root.gameObject.name == "AnyObject");

        // But the selector parentcanvas must be the first canvas
        Debug.Log("Check parentCanvas");
        Assert.IsTrue(textSelector.parentCanvasSelector.gameObject.name == "Canvas-Unamed");
        Assert.IsTrue(textSelector.parentCanvasSelector.parentCanvasSelector.gameObject.name == "RootCanvas");
        Assert.IsTrue(textSelector.parentCanvasSelector.parentCanvasSelector.parentCanvasSelector.gameObject.name == "RootCanvas");
        var canvasSelector = (REcanvas.Selector)textSelector.parentCanvasSelector;
        canvasSelector = (REcanvas.Selector)textSelector.parentCanvasSelector.parentCanvasSelector;
        canvasSelector = (REcanvas.Selector)textSelector.parentCanvasSelector.parentCanvasSelector.parentCanvasSelector;


        // If component is redrawed in null
        Debug.Log("Drawing in null");
        reactorComponent.Draw();
        textSelector = REtext.Find("#ProveText")[0];

        // But the selector parentcanvas must be the first canvas
        Debug.Log("Check parentCanvas");
        Assert.IsTrue(textSelector.parentCanvasSelector.gameObject.name == "Canvas-Unamed");
        Assert.IsTrue(textSelector.parentCanvasSelector.parentCanvasSelector.gameObject.name == "RootCanvas");
        Assert.IsTrue(textSelector.parentCanvasSelector.parentCanvasSelector.parentCanvasSelector.gameObject.name == "RootCanvas");
        canvasSelector = (REcanvas.Selector)textSelector.parentCanvasSelector;
        canvasSelector = (REcanvas.Selector)textSelector.parentCanvasSelector.parentCanvasSelector;
        canvasSelector = (REcanvas.Selector)textSelector.parentCanvasSelector.parentCanvasSelector.parentCanvasSelector;

        reactorComponent.Erase();
    }


}