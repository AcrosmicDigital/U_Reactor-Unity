using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using U.Reactor;
using UnityEngine.EventSystems;
using System;
using System.Linq;

public class Reactor_51Hooks_UseState
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
    public IEnumerator Hooks_UseState_Basics()
    {

        var wordState = new UseState<string>("Hola a todos !!");

        // A Component
        REcanvas MainReactorComponent()
        {

            return new REcanvas
            {

                childs = () => new REbase[] {
                        new REtext {
                            propsElementId = () => new ElementIdSetter{
                                id = "ProveText"
                            },
                            propsText = () => new TextSetter {
                                text = wordState.value,
                            },
                            useState = new IuseState[] {
                                wordState,
                            },
                        },
                    },

            };

        }

        var routerProve = MainReactorComponent();

        yield return new WaitForSecondsRealtime(1);

        routerProve.Draw();
        Debug.Log("Check");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "Hola a todos !!");

        yield return new WaitForSecondsRealtime(1);

        wordState.SetState("NewWord");
        Debug.Log("Check");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "NewWord");
        wordState.SetState("OtherWord");
        Debug.Log("Check");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "OtherWord");
        wordState.PrevState();
        Debug.Log("Check");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "NewWord");
        wordState.SetState("OtherWord");
        wordState.SetState("Word");
        wordState.SetState("LastWord");
        wordState.SetState("FinalWord");
        Debug.Log("Check");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "FinalWord");
        wordState.PrevState(1);
        Debug.Log("Check");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "LastWord");
        wordState.PrevState(2);
        Debug.Log("Check");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "OtherWord");
        wordState.PrevState(10);
        Debug.Log("Check");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "Hola a todos !!");
        wordState.PrevState();
        Debug.Log("Check");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "Hola a todos !!");
        Debug.Log("Check");

        yield return new WaitForSecondsRealtime(2);
        routerProve.Erase();


    }

    [UnityTest]
    public IEnumerator Hooks_UseState_WrongValues()
    {

        var wordState = new UseState<string>("Hola a todos !!");

        // A Component
        REcanvas MainReactorComponent()
        {

            return new REcanvas
            {

                childs = () => new REbase[] {
                        new REtext {
                            propsElementId = () => new ElementIdSetter{
                                id = "ProveText"
                            },
                            propsText = () => new TextSetter {
                                text = wordState.value,
                            },
                            useState = new IuseState[] {
                                wordState,
                            },
                        },
                    },

            };

        }

        var routerProve = MainReactorComponent();

        yield return new WaitForSecondsRealtime(1);

        routerProve.Draw();
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "Hola a todos !!");

        yield return new WaitForSecondsRealtime(1);

        wordState.SetState("NewWord");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "NewWord");
        wordState.SetState("OtherWord");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "OtherWord");
        wordState.PrevState(0);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "OtherWord");
        wordState.SetState("OtherWord");
        wordState.SetState("Word");
        wordState.SetState("LastWord");
        wordState.SetState("FinalWord");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "FinalWord");
        wordState.PrevState(-1);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "FinalWord");
        wordState.PrevState(-2);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "FinalWord");
        wordState.PrevState(-10);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "FinalWord");
        wordState.PrevState(-2);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "FinalWord");

        yield return new WaitForSecondsRealtime(2);
        routerProve.Erase();

    }

    [UnityTest]
    public IEnumerator Hooks_UseState_InHidedElements()
    {

        var wordState = new UseState<string>("Hola a todos !!");

        // A Component
        REcanvas MainReactorComponent()
        {

            return new REcanvas
            {

                childs = () => new REbase[] {
                        new REtext {
                            propsElementId = () => new ElementIdSetter{
                                id = "ProveText"
                            },
                            propsText = () => new TextSetter {
                                text = wordState.value,
                            },
                            useState = new IuseState[] {
                                wordState,
                            },
                        },
                    },

            };

        }
        var routerProve = MainReactorComponent();

        yield return new WaitForSecondsRealtime(1);

        // Drawing the component
        Debug.Log("Drawing");
        routerProve.Draw();
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "Hola a todos !!");

        yield return new WaitForSecondsRealtime(1);

        // Hide the element
        routerProve.Hide();
        Assert.IsFalse(REbase.FindOne("#ProveText").root.canvas.enabled);

        // Change the states
        wordState.SetState("NewWord");
        Debug.Log("Check");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "NewWord");
        Assert.IsFalse(REbase.FindOne("#ProveText").root.canvas.enabled);
        wordState.SetState("OtherWord");
        Debug.Log("Check");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "OtherWord");
        Assert.IsFalse(REbase.FindOne("#ProveText").root.canvas.enabled);
        wordState.PrevState();
        Debug.Log("Check");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "NewWord");
        Assert.IsFalse(REbase.FindOne("#ProveText").root.canvas.enabled);
        wordState.SetState("OtherWord");
        wordState.SetState("Word");
        wordState.SetState("LastWord");
        wordState.SetState("FinalWord");
        Debug.Log("Check");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "FinalWord");
        Assert.IsFalse(REbase.FindOne("#ProveText").root.canvas.enabled);
        wordState.PrevState(1);
        Debug.Log("Check");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "LastWord");
        Assert.IsFalse(REbase.FindOne("#ProveText").root.canvas.enabled);
        wordState.PrevState(2);
        Debug.Log("Check");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "OtherWord");
        Assert.IsFalse(REbase.FindOne("#ProveText").root.canvas.enabled);
        wordState.PrevState(10);
        Debug.Log("Check");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "Hola a todos !!");
        Assert.IsFalse(REbase.FindOne("#ProveText").root.canvas.enabled);
        wordState.PrevState();
        Debug.Log("Check");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "Hola a todos !!");
        Assert.IsFalse(REbase.FindOne("#ProveText").root.canvas.enabled);
        Debug.Log("Check");

        // Enable and check
        routerProve.Show();
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "Hola a todos !!");
        Assert.IsTrue(REbase.FindOne("#ProveText").root.canvas.enabled);
        Debug.Log("Check");


        yield return new WaitForSecondsRealtime(2);
        routerProve.Erase();

    }

    [UnityTest]
    public IEnumerator Hooks_UseState_InDisabledElements()
    {

        var wordState = new UseState<string>("Hola a todos !!");

        // A Component
        REcanvas MainReactorComponent()
        {

            return new REcanvas
            {

                childs = () => new REbase[] {
                        new REtext {
                            propsElementId = () => new ElementIdSetter{
                                id = "ProveText"
                            },
                            propsText = () => new TextSetter {
                                text = wordState.value,
                            },
                            useState = new IuseState[] {
                                wordState,
                            },
                        },
                    },

            };

        }
        var routerProve = MainReactorComponent();

        yield return new WaitForSecondsRealtime(1);

        // Drawing the component
        Debug.Log("Drawing");
        routerProve.Draw();
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "Hola a todos !!");

        yield return new WaitForSecondsRealtime(1);

        // Hide the element
        routerProve.Disable();
        Assert.IsFalse(REbase.FindOne("#ProveText").gameObject.activeInHierarchy);

        // Change the states
        wordState.SetState("NewWord");
        Debug.Log("Check");
        Assert.IsFalse(REbase.FindOne("#ProveText").gameObject.activeInHierarchy);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "NewWord");
        wordState.SetState("OtherWord");
        Debug.Log("Check");
        Assert.IsFalse(REbase.FindOne("#ProveText").gameObject.activeInHierarchy);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "OtherWord");
        wordState.PrevState();
        Debug.Log("Check");
        Assert.IsFalse(REbase.FindOne("#ProveText").gameObject.activeInHierarchy);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "NewWord");
        wordState.SetState("OtherWord");
        wordState.SetState("Word");
        wordState.SetState("LastWord");
        wordState.SetState("FinalWord");
        Debug.Log("Check");
        Assert.IsFalse(REbase.FindOne("#ProveText").gameObject.activeInHierarchy);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "FinalWord");
        wordState.PrevState(1);
        Debug.Log("Check");
        Assert.IsFalse(REbase.FindOne("#ProveText").gameObject.activeInHierarchy);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "LastWord");
        wordState.PrevState(2);
        Debug.Log("Check");
        Assert.IsFalse(REbase.FindOne("#ProveText").gameObject.activeInHierarchy);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "OtherWord");
        wordState.PrevState(10);
        Debug.Log("Check");
        Assert.IsFalse(REbase.FindOne("#ProveText").gameObject.activeInHierarchy);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "Hola a todos !!");
        wordState.PrevState();
        Debug.Log("Check");
        Assert.IsFalse(REbase.FindOne("#ProveText").gameObject.activeInHierarchy);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "Hola a todos !!");
        Debug.Log("Check");

        // Enable and check
        routerProve.Enable();
        Assert.IsTrue(REbase.FindOne("#ProveText").gameObject.activeInHierarchy);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "Hola a todos !!");
        Debug.Log("Check");


        yield return new WaitForSecondsRealtime(2);
        routerProve.Erase();

    }

    [UnityTest]
    public IEnumerator Hooks_UseState_InHidedCanvas()
    {

        var alphaState = new UseState<float>(0f);

        // A Component
        REcanvas MainReactorComponent()
        {

            return new REcanvas
            {
                propsElementId = () => new ElementIdSetter
                {
                    id = "ProveCanvas"
                },
                propsCanvasGroup = () => new CanvasGroupSetter
                {
                    alpha = alphaState.value,
                },
                useState = new IuseState[]
                {
                    alphaState,
                },
                childs = () => new REbase[] {
                        new REtext (),
                    },

            };

        }
        var routerProve = MainReactorComponent();

        yield return new WaitForSecondsRealtime(1);

        // Drawing the component
        Debug.Log("Drawing");
        routerProve.Draw();
        Assert.IsTrue(REcanvas.FindOne("#ProveCanvas").canvasGroup.alpha == 0f);

        yield return new WaitForSecondsRealtime(1);

        // Hide the element
        routerProve.Hide();
        Assert.IsFalse(REbase.FindOne("#ProveCanvas").root.canvas.enabled);

        // Change the states
        alphaState.SetState(.33f);
        Assert.IsTrue(REcanvas.FindOne("#ProveCanvas").canvasGroup.alpha == .33f);
        Assert.IsFalse(REcanvas.FindOne("#ProveCanvas").root.canvas.enabled);
        alphaState.SetState(.66f);
        Assert.IsTrue(REcanvas.FindOne("#ProveCanvas").canvasGroup.alpha == .66f);
        Assert.IsFalse(REcanvas.FindOne("#ProveCanvas").root.canvas.enabled);
        alphaState.PrevState();
        Assert.IsTrue(REcanvas.FindOne("#ProveCanvas").canvasGroup.alpha == .33f);
        Assert.IsFalse(REcanvas.FindOne("#ProveCanvas").root.canvas.enabled);

        // Enable and check
        routerProve.Show();
        Assert.IsTrue(REcanvas.FindOne("#ProveCanvas").canvasGroup.alpha == .33f);
        Assert.IsTrue(REcanvas.FindOne("#ProveCanvas").root.canvas.enabled);


        yield return new WaitForSecondsRealtime(2);
        routerProve.Erase();

    }

    [UnityTest]
    public IEnumerator Hooks_UseState_InDisabledCanvas()
    {

        var alphaState = new UseState<float>(0f);

        // A Component
        REcanvas MainReactorComponent()
        {

            return new REcanvas
            {
                propsElementId = () => new ElementIdSetter
                {
                    id = "ProveCanvas"
                },
                propsCanvasGroup = () => new CanvasGroupSetter
                {
                    alpha = alphaState.value,
                },
                useState = new IuseState[]
                {
                    alphaState,
                },
                childs = () => new REbase[] {
                        new REtext (),
                    },

            };

        }
        var routerProve = MainReactorComponent();

        yield return new WaitForSecondsRealtime(1);

        // Drawing the component
        Debug.Log("Drawing");
        routerProve.Draw();
        Assert.IsTrue(REcanvas.FindOne("#ProveCanvas").canvasGroup.alpha == 0f);

        yield return new WaitForSecondsRealtime(1);

        // Hide the element
        routerProve.Disable();
        Assert.IsFalse(REbase.FindOne("#ProveCanvas").gameObject.activeSelf);

        // Change the states
        alphaState.SetState(.33f);
        Assert.IsTrue(REcanvas.FindOne("#ProveCanvas").canvasGroup.alpha == .33f);

        // Will disable after one frame, because need to be active to calculate all the rect transform parameters
        yield return new WaitForSecondsRealtime(.3f);
        Assert.IsFalse(REbase.FindOne("#ProveCanvas").gameObject.activeSelf);

        alphaState.SetState(.66f);
        // Will disable after one frame, because need to be active to calculate all the rect transform parameters
        yield return new WaitForSecondsRealtime(.3f);
        Assert.IsTrue(REcanvas.FindOne("#ProveCanvas").canvasGroup.alpha == .66f);
        Assert.IsFalse(REbase.FindOne("#ProveCanvas").gameObject.activeSelf);

        alphaState.PrevState();

        // Will disable after one frame, because need to be active to calculate all the rect transform parameters
        yield return new WaitForSecondsRealtime(.3f);
        Assert.IsTrue(REcanvas.FindOne("#ProveCanvas").canvasGroup.alpha == .33f);
        Assert.IsFalse(REbase.FindOne("#ProveCanvas").gameObject.activeSelf);


        // Enable and check
        routerProve.Enable();
        // Will disable after one frame, because need to be active to calculate all the rect transform parameters
        yield return new WaitForSecondsRealtime(.3f);
        Assert.IsTrue(REcanvas.FindOne("#ProveCanvas").canvasGroup.alpha == .33f);
        Assert.IsTrue(REbase.FindOne("#ProveCanvas").gameObject.activeSelf);


        yield return new WaitForSecondsRealtime(2);
        routerProve.Erase();

    }


    [UnityTest]
    public IEnumerator Hooks_UseState_ErasedComponent()
    {

        var wordState = new UseState<string>("Hola a todos !!");

        // A Component
        REcanvas MainReactorComponent()
        {

            return new REcanvas
            {

                childs = () => new REbase[] {
                        new REtext {
                            propsElementId = () => new ElementIdSetter{
                                id = "ProveText"
                            },
                            propsText = () => new TextSetter {
                                text = wordState.value,
                            },
                            useState = new IuseState[] {
                                wordState,
                            },
                        },
                    },

            };

        }

        var routerProve = MainReactorComponent();

        yield return new WaitForSecondsRealtime(1);

        // Draw the component
        routerProve.Draw();
        Debug.Log("Check");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "Hola a todos !!");

        yield return new WaitForSecondsRealtime(1);

        // Erase the component
        routerProve.Erase();
        wordState.SetState("NewWord");
        wordState.SetState("OtherWord");
        wordState.PrevState();
        wordState.SetState("OtherWord");
        wordState.SetState("Word");
        wordState.SetState("LastWord");
        wordState.SetState("FinalWord");

        // Draw the component again
        routerProve.Draw();
        Debug.Log("Check");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "FinalWord");

        yield return new WaitForSecondsRealtime(2);
        routerProve.Erase();


    }

    [UnityTest]
    public IEnumerator Hooks_UseState_VariableNumberOfStates()
    {

        var wordState = new UseState<string>("Hola a todos !!");
        var fontSizeState = new UseState<int>(20);

        // A Component
        REcanvas MainReactorComponent()
        {

            return new REcanvas
            {

                childs = () => new REbase[] {
                        new REtext {
                            propsElementId = () => new ElementIdSetter{
                                id = "ProveText"
                            },
                            propsText = () => new TextSetter {
                                text = wordState.value,
                                fontSize = fontSizeState.value,
                            },
                            useState = new IuseState[] {
                                wordState,
                                fontSizeState,
                            },
                        },
                    },

            };

        }

        var routerProve = MainReactorComponent();

        yield return new WaitForSecondsRealtime(1);

        // Draw the component
        routerProve.Draw();
        Debug.Log("Check");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "Hola a todos !!");

        yield return new WaitForSecondsRealtime(1);

        // Erase the component
        routerProve.Erase();
        wordState.SetState("NewWord");
        wordState.SetState("OtherWord");
        fontSizeState.SetState(23);
        wordState.PrevState();
        wordState.SetState("OtherWord");
        fontSizeState.SetState(44);
        wordState.SetState("Word");
        wordState.SetState("LastWord");
        fontSizeState.SetState(39);
        wordState.SetState("FinalWord");
        fontSizeState.SetState(34);

        // Draw the component again
        routerProve.Draw();
        Debug.Log("Check");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "FinalWord");

        yield return new WaitForSecondsRealtime(2);
        routerProve.Erase();


    }


    [UnityTest]
    public IEnumerator Hooks_UseStateUseEffect()
    {

        var wordState = new UseState<string>("Hola a todos !!");

        // A Component
        REcanvas MainReactorComponent()
        {

            return new REcanvas
            {

                childs = () => new REbase[] {
                        new REtext {
                            propsElementId = () => new ElementIdSetter
                            {
                                id = "Textmain",
                            },
                            propsText = () => new TextSetter {
                                text = wordState.value,
                            },
                            useState = new IuseState[] {
                                wordState,
                            },
                            useEffect = new REtext.UseEffect.Hook[]{
                                new REtext.UseEffect.Hook {
                                    duration = 3,
                                    deltaFunction = (s) => wordState.SetState("A new word"),
                                }
                            }
                        },
                    },


            };

        }

        var routerProve = MainReactorComponent();

        routerProve.Draw();

        // Waint until useeffect act
        yield return new WaitForSecondsRealtime(4);

        // Find the component
        Assert.IsTrue(REtext.FindOne("#Textmain").textCmp.text == "A new word");

        yield return null;
    }




    [UnityTest]
    public IEnumerator Hooks_UseState_GenerateChildWithUseState_ManualTriggerToChange()
    {

        var listState = new UseState<List<string>>(new List<string>());

        // A Component
        REcanvas MainReactorComponent()
        {

            return new REcanvas
            {
                propsElementId = () => new ElementIdSetter
                {
                    id = "MainCanvas",
                },
                useState = new IuseState[]
                {
                    listState,
                },
                childs = () => listState.value.Select(c =>
                    new REtext
                    {
                        propsElementId = () => new ElementIdSetter
                        {
                            id = c + "Num",
                        },
                        propsText = () => new TextSetter
                        {
                            text = c,
                        }
                    }
                ),

            };

        }

        var routerProve = MainReactorComponent();

        yield return new WaitForSecondsRealtime(1);

        // Draw the component
        Debug.Log("Drawing");
        routerProve.Draw();

        // First check
        Debug.Log("Childs: " + REbase.FindOne("#MainCanvas").childs.Length);
        Assert.IsTrue(REbase.FindOne("#MainCanvas").childs.Length == 0);







        // usestate the component adding elements to the list manually
        Debug.Log("UseState: " + listState.value.Count());
        listState.value.Add("Dog");
        listState.value.Add("Cat");

        // Check, childs are 0, because the useState will not trigger if .SetState is not used
        Debug.Log("Childs: " + REbase.FindOne("#MainCanvas").childs.Length);
        Assert.IsTrue(REbase.FindOne("#MainCanvas").childs.Length == 0);



        // But can be triggered manually
        listState.SetState();
        // Check, childs are 2, because the useState will not trigger if .SetState is not used
        Debug.Log("Childs: " + REbase.FindOne("#MainCanvas").childs.Length);
        Assert.IsTrue(REbase.FindOne("#MainCanvas").childs.Length == 2);



        yield return new WaitForSecondsRealtime(2);
        routerProve.Erase();



    }


    [UnityTest]
    public IEnumerator Hooks_UseState_GenerateChildWithUseState()
    {

        var listState = new UseState<List<string>>(new List<string>());

        // A Component
        REcanvas MainReactorComponent()
        {

            return new REcanvas
            {
                propsElementId = () => new ElementIdSetter
                {
                    id = "MainCanvas",
                },
                useState = new IuseState[]
                {
                    listState,
                },
                childs = () => listState.value.Select(c =>
                    new REtext
                    {
                        propsElementId = () => new ElementIdSetter
                        {
                            id = c + "Num",
                        },
                        propsText = () => new TextSetter
                        {
                            text = c,
                        }
                    }
                ),

            };

        }

        var routerProve = MainReactorComponent();

        yield return new WaitForSecondsRealtime(1);

        // Draw the component
        Debug.Log("Drawing");
        routerProve.Draw();

        // First check
        Debug.Log("Childs: " + REbase.FindOne("#MainCanvas").childs.Length);
        Assert.IsTrue(REbase.FindOne("#MainCanvas").childs.Length == 0);



        // Simulate the fetch
        yield return new WaitForSecondsRealtime(1);
        var fetchData = new List<string>
        {
            "Six",
            "Seven",
            "Eight",
            "Nine",
        };




        // Draw again the component
        Debug.Log("UseState: " + listState.value.Count());
        listState.SetState(fetchData);

        // Check
        Debug.Log("Childs: " + REbase.FindOne("#MainCanvas").childs.Length);
        Assert.IsTrue(REbase.FindOne("#MainCanvas").childs.Length == 4);

        Debug.Log("Childs");
        foreach (var word in listState.value)
        {
            //Debug.Log("Child: " + "#" + word + "Num" + " == " + word);
            //Debug.Log("Child: " + REtext.FindOne("#" + word + "Num").textCmp.text + " == " + word);
            Assert.IsTrue(REtext.FindOne("#" + word + "Num").textCmp.text == word);
        }





        yield return new WaitForSecondsRealtime(2);
        routerProve.Erase();



    }


}
