using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using U.Reactor;
using UnityEngine.EventSystems;
using System;
using System.Linq;

public class Reactor_91FindCases
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
    public IEnumerator Find_ThenErase()
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

        // Draw the component for the first time
        Debug.Log("Drawing");
        reactorComponent.Draw();

        yield return new WaitForSecondsRealtime(1);

        // Look fr the component
        Debug.Log("Finding");
        var textSelector = REtext.FindOne("#ProveText");

        // Check that the compoennt exist
        Assert.IsFalse(textSelector.isDisposed);
        Assert.IsFalse(textSelector.rectTransform == null);
        Assert.IsFalse(textSelector.gameObject == null);
        Assert.IsFalse(textSelector.parent == null);
        Assert.IsFalse(textSelector.childs == null);
        Assert.IsFalse(textSelector.brothers == null);
        Assert.IsFalse(textSelector.canvasRenderer == null);
        Assert.IsTrue(textSelector.textCmp.text == "Hello world");

        // Erase the component
        Debug.Log("Erasing");
        reactorComponent.Erase();

        // Check that the selector references are now null
        Assert.IsTrue(textSelector.isDisposed);
        Assert.IsTrue(textSelector.rectTransform == null);
        Assert.IsTrue(textSelector.gameObject == null);
        Assert.IsTrue(textSelector.parent == null);
        Assert.IsTrue(textSelector.childs == null);
        Assert.IsTrue(textSelector.brothers == null);
        Assert.IsTrue(textSelector.canvasRenderer == null);
        Assert.Throws<NullReferenceException>(() => Debug.Log("V: " + textSelector.rectTransform.rect));

        // Try to find again the component that it must not exist
        Debug.Log("Finding again");
        Assert.IsTrue(REtext.Find("#ProveText").Length == 0);

        yield return new WaitForSecondsRealtime(2);
        reactorComponent.Erase();


    }

    [UnityTest]
    public IEnumerator Find_ThenDraw()
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

        // Draw the component for the first time
        Debug.Log("Drawing");
        reactorComponent.Draw();

        yield return new WaitForSecondsRealtime(1);

        // Look fr the component
        Debug.Log("Finding");
        var textSelector = REtext.FindOne("#ProveText");

        // Check that the compoennt exist
        Assert.IsFalse(textSelector.isDisposed);
        Assert.IsFalse(textSelector.rectTransform == null);
        Assert.IsFalse(textSelector.gameObject == null);
        Assert.IsFalse(textSelector.parent == null);
        Assert.IsFalse(textSelector.childs == null);
        Assert.IsFalse(textSelector.brothers == null);
        Assert.IsFalse(textSelector.canvasRenderer == null);
        Assert.IsTrue(textSelector.textCmp.text == "Hello world");

        // Redraw the component
        Debug.Log("Drawing again");
        reactorComponent.Draw();

        // Check that the selector references are now null
        Assert.IsTrue(textSelector.isDisposed);
        Assert.IsTrue(textSelector.rectTransform == null);
        Assert.IsTrue(textSelector.gameObject == null);
        Assert.IsTrue(textSelector.parent == null);
        Assert.IsTrue(textSelector.childs == null);
        Assert.IsTrue(textSelector.brothers == null);
        Assert.IsTrue(textSelector.canvasRenderer == null);
        Assert.Throws<NullReferenceException>(() => Debug.Log("V: " + textSelector.rectTransform.rect));

        // Try to find again the component that it must exist
        Debug.Log("Finding again");
        textSelector = REtext.FindOne("#ProveText");
        Assert.IsTrue(REtext.Find("#ProveText").Length == 1);

        // Check that the compoennt exist
        Assert.IsFalse(textSelector.isDisposed);
        Assert.IsFalse(textSelector.rectTransform == null);
        Assert.IsFalse(textSelector.gameObject == null);
        Assert.IsFalse(textSelector.parent == null);
        Assert.IsFalse(textSelector.childs == null);
        Assert.IsFalse(textSelector.brothers == null);
        Assert.IsFalse(textSelector.canvasRenderer == null);
        Assert.IsTrue(textSelector.textCmp.text == "Hello world");

        yield return new WaitForSecondsRealtime(2);
        reactorComponent.Erase();

    }

    [UnityTest]
    public IEnumerator Find_ThenChangeAndDraw()
    {

        var letter = "Hello world";

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
                                text = letter,
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

        // Draw the component for the first time
        Debug.Log("Drawing");
        reactorComponent.Draw();

        yield return new WaitForSecondsRealtime(1);

        // Look fr the component
        Debug.Log("Finding");
        var textSelector = REtext.FindOne("#ProveText");

        // Check that the compoennt exist
        Assert.IsFalse(textSelector.isDisposed);
        Assert.IsFalse(textSelector.rectTransform == null);
        Assert.IsFalse(textSelector.gameObject == null);
        Assert.IsFalse(textSelector.parent == null);
        Assert.IsFalse(textSelector.childs == null);
        Assert.IsFalse(textSelector.brothers == null);
        Assert.IsFalse(textSelector.canvasRenderer == null);
        Assert.IsTrue(textSelector.textCmp.text == "Hello world");

        // Redraw the component
        Debug.Log("Changing and drawing again");
        letter = "New text";
        reactorComponent.Draw();

        // Check that the selector references are now null
        Assert.IsTrue(textSelector.isDisposed);
        Assert.IsTrue(textSelector.rectTransform == null);
        Assert.IsTrue(textSelector.gameObject == null);
        Assert.IsTrue(textSelector.parent == null);
        Assert.IsTrue(textSelector.childs == null);
        Assert.IsTrue(textSelector.brothers == null);
        Assert.IsTrue(textSelector.canvasRenderer == null);
        Assert.Throws<NullReferenceException>(() => Debug.Log("V: " + textSelector.rectTransform.rect));

        // Try to find again the component that it must exist
        Debug.Log("Finding again");
        textSelector = REtext.FindOne("#ProveText");
        Assert.IsTrue(REtext.Find("#ProveText").Length == 1);

        // Check that the compoennt exist
        Assert.IsFalse(textSelector.isDisposed);
        Assert.IsFalse(textSelector.rectTransform == null);
        Assert.IsFalse(textSelector.gameObject == null);
        Assert.IsFalse(textSelector.parent == null);
        Assert.IsFalse(textSelector.childs == null);
        Assert.IsFalse(textSelector.brothers == null);
        Assert.IsFalse(textSelector.canvasRenderer == null);
        Assert.IsTrue(textSelector.textCmp.text == "New text");

        yield return new WaitForSecondsRealtime(2);
        reactorComponent.Erase();

    }

    [UnityTest]
    public IEnumerator Find_AndUseState()
    {

        var letterState = new UseState<string>("Hello world");

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
                                text = letterState.value,
                            },
                            useState = new IuseState[]
                            {
                                letterState,
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

        // Draw the component for the first time
        Debug.Log("Drawing");
        reactorComponent.Draw();

        yield return new WaitForSecondsRealtime(1);

        // Look fr the component
        Debug.Log("Finding");
        var textSelector = REtext.FindOne("#ProveText");

        // Check that the compoennt exist
        Assert.IsFalse(textSelector.isDisposed);
        Assert.IsFalse(textSelector.rectTransform == null);
        Assert.IsFalse(textSelector.gameObject == null);
        Assert.IsFalse(textSelector.parent == null);
        Assert.IsFalse(textSelector.childs == null);
        Assert.IsFalse(textSelector.brothers == null);
        Assert.IsFalse(textSelector.canvasRenderer == null);
        Assert.IsTrue(textSelector.textCmp.text == "Hello world");

        // Redraw the component
        Debug.Log("Changing and drawing again");
        letterState.SetState("New text");

        // Check that the selector references are now null
        Debug.Log("Changing and drawing again");
        Assert.IsTrue(textSelector.isDisposed);
        Debug.Log("Changing and drawing again");
        Assert.IsTrue(textSelector.rectTransform == null);
        Debug.Log("Changing and drawing again");
        Assert.IsTrue(textSelector.gameObject == null);
        Debug.Log("Changing and drawing again");
        Assert.IsTrue(textSelector.parent == null);
        Debug.Log("Changing and drawing again");
        Assert.IsTrue(textSelector.childs == null);
        Debug.Log("Changing and drawing again");
        Assert.IsTrue(textSelector.brothers == null);
        Debug.Log("Changing and drawing again");
        Assert.IsTrue(textSelector.canvasRenderer == null);
        Debug.Log("Changing and drawing again");
        Assert.Throws<NullReferenceException>(() => Debug.Log("V: " + textSelector.rectTransform.rect));
        Debug.Log("Changing and drawing again");
        // Try to find again the component that it must exist
        Debug.Log("Finding again");
        textSelector = REtext.FindOne("#ProveText");

        // Check that the compoennt exist
        Assert.IsFalse(textSelector.isDisposed);
        Assert.IsFalse(textSelector.rectTransform == null);
        Assert.IsFalse(textSelector.gameObject == null);
        Assert.IsFalse(textSelector.parent == null);
        Assert.IsFalse(textSelector.childs == null);
        Assert.IsFalse(textSelector.brothers == null);
        Assert.IsFalse(textSelector.canvasRenderer == null);
        Assert.IsTrue(textSelector.textCmp.text == "New text");

        yield return new WaitForSecondsRealtime(2);
        reactorComponent.Erase();

    }

    [UnityTest]
    public IEnumerator Find_AndHideShow()
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

        // Draw the component for the first time
        Debug.Log("Drawing");
        reactorComponent.Draw();

        yield return new WaitForSecondsRealtime(1);

        // Look fr the component
        Debug.Log("Finding");
        var textSelector = REtext.FindOne("#ProveText");

        // Check that the compoennt exist
        Assert.IsFalse(textSelector.isDisposed);
        Assert.IsFalse(textSelector.rectTransform == null);
        Assert.IsFalse(textSelector.gameObject == null);
        Assert.IsFalse(textSelector.parent == null);
        Assert.IsFalse(textSelector.childs == null);
        Assert.IsFalse(textSelector.brothers == null);
        Assert.IsFalse(textSelector.canvasRenderer == null);
        Assert.IsTrue(textSelector.textCmp.text == "Hello world");

        // Hide/Disable the component
        Debug.Log("Hiding");
        reactorComponent.Hide();
        Assert.IsFalse(((REcanvas.Selector)textSelector.root).canvas.enabled);

        // Check that the selector references are not null
        Assert.IsFalse(textSelector.isDisposed);
        Assert.IsFalse(textSelector.rectTransform == null);
        Assert.IsFalse(textSelector.gameObject == null);
        Assert.IsFalse(textSelector.parent == null);
        Assert.IsFalse(textSelector.childs == null);
        Assert.IsFalse(textSelector.brothers == null);
        Assert.IsFalse(textSelector.canvasRenderer == null);
        Assert.IsTrue(textSelector.textCmp.text == "Hello world");

        // Show/Enable the component
        Debug.Log("Showing");
        reactorComponent.Show();
        Assert.IsTrue(((REcanvas.Selector)textSelector.root).canvas.enabled);

        // Check that the selector references are now null
        Assert.IsFalse(textSelector.isDisposed);
        Assert.IsFalse(textSelector.rectTransform == null);
        Assert.IsFalse(textSelector.gameObject == null);
        Assert.IsFalse(textSelector.parent == null);
        Assert.IsFalse(textSelector.childs == null);
        Assert.IsFalse(textSelector.brothers == null);
        Assert.IsFalse(textSelector.canvasRenderer == null);
        Assert.IsTrue(textSelector.textCmp.text == "Hello world");

        yield return new WaitForSecondsRealtime(2);
        reactorComponent.Erase();


    }

    [UnityTest]
    public IEnumerator Find_AndDisableEnable()
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

        // Draw the component for the first time
        Debug.Log("Drawing");
        reactorComponent.Draw();

        yield return new WaitForSecondsRealtime(1);

        // Look fr the component
        Debug.Log("Finding");
        var textSelector = REtext.FindOne("#ProveText");
        Assert.IsTrue(textSelector.gameObject.activeSelf);

        // Check that the compoennt exist
        Assert.IsFalse(textSelector.isDisposed);
        Assert.IsFalse(textSelector.rectTransform == null);
        Assert.IsFalse(textSelector.gameObject == null);
        Assert.IsFalse(textSelector.parent == null);
        Assert.IsFalse(textSelector.childs == null);
        Assert.IsFalse(textSelector.brothers == null);
        Assert.IsFalse(textSelector.canvasRenderer == null);
        Assert.IsTrue(textSelector.textCmp.text == "Hello world");

        // Disable the component
        Debug.Log("Disabling");
        reactorComponent.Disable();
        Assert.IsTrue(textSelector.gameObject.activeSelf);
        Assert.IsFalse(textSelector.gameObject.activeInHierarchy);
        Assert.IsFalse(textSelector.root.gameObject.activeSelf);

        // Check that the selector references are not null
        Assert.IsFalse(textSelector.isDisposed); 
        Assert.IsFalse(textSelector.rectTransform == null);
        Assert.IsFalse(textSelector.gameObject == null);
        Assert.IsFalse(textSelector.parent == null);
        Assert.IsFalse(textSelector.childs == null);
        Assert.IsFalse(textSelector.brothers == null);
        Assert.IsFalse(textSelector.canvasRenderer == null);
        Assert.IsTrue(textSelector.textCmp.text == "Hello world");

        // Show/Enable the component
        Debug.Log("Showing");
        reactorComponent.Enable();
        Assert.IsTrue(textSelector.gameObject.activeSelf);
        Assert.IsTrue(textSelector.gameObject.activeInHierarchy);
        Assert.IsTrue(textSelector.root.gameObject.activeSelf);

        // Check that the selector references are now null
        Assert.IsFalse(textSelector.isDisposed);
        Assert.IsFalse(textSelector.rectTransform == null);
        Assert.IsFalse(textSelector.gameObject == null);
        Assert.IsFalse(textSelector.parent == null);
        Assert.IsFalse(textSelector.childs == null);
        Assert.IsFalse(textSelector.brothers == null);
        Assert.IsFalse(textSelector.canvasRenderer == null);
        Assert.IsTrue(textSelector.textCmp.text == "Hello world");

        yield return new WaitForSecondsRealtime(2);
        reactorComponent.Erase();


    }


    [UnityTest]
    public IEnumerator FindAsChildOfOtherGO_AndDisableEnable()
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

        // Draw the component for the first time
        Debug.Log("Drawing");
        var go = new GameObject("AnyGameobject");
        reactorComponent.Draw(go);

        yield return new WaitForSecondsRealtime(1);

        // Look fr the component
        Debug.Log("Finding");
        var textSelector = REtext.FindOne("#ProveText");
        Assert.IsTrue(textSelector.gameObject.activeSelf);

        // Check that the compoennt exist
        Assert.IsFalse(textSelector.isDisposed);
        Assert.IsFalse(textSelector.rectTransform == null);
        Assert.IsFalse(textSelector.gameObject == null);
        Assert.IsFalse(textSelector.parent == null);
        Assert.IsFalse(textSelector.childs == null);
        Assert.IsFalse(textSelector.brothers == null);
        Assert.IsFalse(textSelector.canvasRenderer == null);
        Assert.IsTrue(textSelector.textCmp.text == "Hello world");

        // Disable the component
        Debug.Log("Disabling");
        go.SetActive(false);
        Assert.IsTrue(textSelector.gameObject.activeSelf);
        Assert.IsFalse(textSelector.gameObject.activeInHierarchy);
        Assert.IsTrue(textSelector.root.gameObject.activeSelf);

        // Check that the selector references are not null
        Assert.IsFalse(textSelector.isDisposed);
        Assert.IsFalse(textSelector.rectTransform == null);
        Assert.IsFalse(textSelector.gameObject == null);
        Assert.IsFalse(textSelector.parent == null);
        Assert.IsFalse(textSelector.childs == null);
        Assert.IsFalse(textSelector.brothers == null);
        Assert.IsFalse(textSelector.canvasRenderer == null);
        Assert.IsTrue(textSelector.textCmp.text == "Hello world");

        // Show/Enable the component
        Debug.Log("Showing");
        go.SetActive(true);
        Assert.IsTrue(textSelector.gameObject.activeSelf);
        Assert.IsTrue(textSelector.gameObject.activeInHierarchy);
        Assert.IsTrue(textSelector.root.gameObject.activeSelf);

        // Check that the selector references are now null
        Assert.IsFalse(textSelector.isDisposed);
        Assert.IsFalse(textSelector.rectTransform == null);
        Assert.IsFalse(textSelector.gameObject == null);
        Assert.IsFalse(textSelector.parent == null);
        Assert.IsFalse(textSelector.childs == null);
        Assert.IsFalse(textSelector.brothers == null);
        Assert.IsFalse(textSelector.canvasRenderer == null);
        Assert.IsTrue(textSelector.textCmp.text == "Hello world");

        yield return new WaitForSecondsRealtime(2);
        reactorComponent.Erase();


    }

}
