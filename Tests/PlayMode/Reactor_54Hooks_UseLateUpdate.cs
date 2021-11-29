using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using U.Reactor;
using UnityEngine.EventSystems;
using System;
using System.Linq;
using System.Text.RegularExpressions;

public class Reactor_54Hooks_UseLateUpdate
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

    [TearDown]
    public void TearDown()
    {
        Time.timeScale = 1;
    }


    // Unscaled and scaled time
    // Diferent modes


    [UnityTest]
    public IEnumerator Hooks_UseLateUpdate_10Basics()
    {

        var counter = 0;

        // A Component
        REcanvas MainReactorComponent()
        {

            return new REcanvas
            {

                childs = () => new REbase[] {
                        new REtext {
                            propsId = () => new REtext.IdSetter{
                                id = "ProveText"
                            },
                            propsText = () => new REtext.TextSetter {
                                text = "Hello World!",
                            },
                            useLateUpdate = new REtext.UseLateUpdate.Hook
                            {
                                onLateUpdate = (s) =>
                                {
                                    // Increment the value each frame
                                    if(counter < 10)
                                        counter++;
                                },
                            }
                        },
                    },

            };

        }

        var routerProve = MainReactorComponent();
        yield return new WaitForSecondsRealtime(1);

        // Draw the component
        Debug.Log("Drawing");
        routerProve.Draw();

        // First check and hook must be runner 
        yield return new WaitForSecondsRealtime(1.0f);
        Assert.IsTrue(counter == 10);





        yield return new WaitForSecondsRealtime(2);
        routerProve.Erase();


    }

    [UnityTest]
    public IEnumerator Hooks_UseLateUpdate_11TimeMode()
    {

        var counter = 0;

        // A Component
        REcanvas MainReactorComponent()
        {

            return new REcanvas
            {

                childs = () => new REbase[] {
                        new REtext {
                            propsId = () => new REtext.IdSetter{
                                id = "ProveText"
                            },
                            propsText = () => new REtext.TextSetter {
                                text = "Hello World!",
                            },
                            useLateUpdate = new REtext.UseLateUpdate.Hook
                            {
                                onLateUpdate = (s) =>
                                {
                                    // Increment the value each frame
                                    if(counter < 10)
                                        counter++;
                                },
                            }
                        },
                    },

            };

        }



        var routerProve = MainReactorComponent();
        yield return new WaitForSecondsRealtime(1);


        // Time dont affect
        Time.timeScale = 1f;


        // Draw the component
        Debug.Log("Drawing");
        routerProve.Draw();




        // First check and hook must be runner 
        yield return new WaitForSecondsRealtime(1.0f);
        Assert.IsTrue(counter == 10);





        yield return new WaitForSecondsRealtime(2);
        routerProve.Erase();


    }



    [UnityTest]
    public IEnumerator Hooks_UseLateUpdate_60ThrowErrorInDeltaFunction()
    {

        var counter = 0;

        // A Component
        REcanvas MainReactorComponent()
        {

            return new REcanvas
            {

                childs = () => new REbase[] {
                        new REtext {
                            propsId = () => new REtext.IdSetter{
                                id = "ProveText"
                            },
                            propsText = () => new REtext.TextSetter {
                                text = "Hello World!",
                            },
                            useLateUpdate = new REtext.UseLateUpdate.Hook
                            {
                                onLateUpdate = (s) =>
                                {
                                    // Increment the value each frame
                                    if(counter < 10)
                                        counter++;
                                    throw new Exception("Expected exception");
                                },
                            }
                        },
                    },

            };

        }

        var routerProve = MainReactorComponent();
        yield return new WaitForSecondsRealtime(1);

        // Draw the component
        Debug.Log("Drawing");
        routerProve.Draw();




        // Many error messages will be printed
        LogAssert.ignoreFailingMessages = true;



        // The function will run in each frame
        yield return new WaitForSecondsRealtime(1.0f);
        Assert.IsTrue(counter == 10);




        yield return new WaitForSecondsRealtime(2);
        routerProve.Erase();


    }






    [UnityTest]
    public IEnumerator Hooks_UseLateUpdate_30InHidedElements_RunNormally()
    {


        var counter = 0;

        // A Component
        REcanvas MainReactorComponent()
        {

            return new REcanvas
            {

                childs = () => new REbase[] {
                        new REtext {
                            propsId = () => new REtext.IdSetter{
                                id = "ProveText"
                            },
                            propsText = () => new REtext.TextSetter {
                                text = "Hello World!",
                            },
                            useLateUpdate = new REtext.UseLateUpdate.Hook
                            {
                                onLateUpdate = (s) =>
                                {
                                    // Increment the value each frame
                                    if(counter < 10)
                                        counter++;
                                },
                            }
                        },
                    },

            };

        }



        var routerProve = MainReactorComponent();
        yield return new WaitForSecondsRealtime(1);



        // Draw the component
        Debug.Log("Drawing");
        routerProve.Draw();


        // Hide the element
        routerProve.Hide();
        Assert.IsFalse(((REcanvas.Selector)REbase.Find("#ProveText")[0].rootCanvasSelector).canvas.enabled);


        // First check and hook must be runned
        yield return new WaitForSecondsRealtime(1.0f);
        Assert.IsTrue(counter == 10);




        yield return new WaitForSecondsRealtime(2);
        routerProve.Erase();


    }

    [UnityTest]
    public IEnumerator Hooks_UseLateUpdate_31InDisabledElements_DontRun()
    {


        var counter = 0;

        // A Component
        REcanvas MainReactorComponent()
        {

            return new REcanvas
            {

                childs = () => new REbase[] {
                        new REtext {
                            propsId = () => new REtext.IdSetter{
                                id = "ProveText"
                            },
                            propsText = () => new REtext.TextSetter {
                                text = "Hello World!",
                            },
                            useLateUpdate = new REtext.UseLateUpdate.Hook
                            {
                                onLateUpdate = (s) =>
                                {
                                    // Increment the value each frame
                                    if(counter < 10)
                                        counter++;
                                },
                            }
                        },
                    },

            };

        }



        var routerProve = MainReactorComponent();
        yield return new WaitForSecondsRealtime(1);



        // Draw the component
        Debug.Log("Drawing");
        routerProve.Draw();


        // Hide the element
        routerProve.Disable();
        Assert.IsFalse(((REcanvas.Selector)REbase.Find("#ProveText")[0].rootCanvasSelector).canvas.enabled);


        // First check and hook must be runned
        yield return new WaitForSecondsRealtime(1.0f);
        Assert.IsFalse(counter == 10);




        yield return new WaitForSecondsRealtime(2);
        routerProve.Erase();

    }

    [UnityTest]
    public IEnumerator Hooks_UseLateUpdate_32InErasedElements_WillBeErasedToo()
    {


        var counter = 0;

        // A Component
        REcanvas MainReactorComponent()
        {

            return new REcanvas
            {

                childs = () => new REbase[] {
                        new REtext {
                            propsId = () => new REtext.IdSetter{
                                id = "ProveText"
                            },
                            propsText = () => new REtext.TextSetter {
                                text = "Hello World!",
                            },
                            useLateUpdate = new REtext.UseLateUpdate.Hook
                            {
                                onLateUpdate = (s) =>
                                {
                                    // Increment the value each frame
                                    if(counter < 10)
                                        counter++;
                                },
                            }
                        },
                    },

            };

        }



        var routerProve = MainReactorComponent();
        yield return new WaitForSecondsRealtime(1);



        // Draw the component
        Debug.Log("Drawing");
        routerProve.Draw();


        // Hide the element
        routerProve.Erase();


        // First check and hook must be runned
        yield return new WaitForSecondsRealtime(1.0f);
        Assert.IsFalse(counter == 10);




        yield return new WaitForSecondsRealtime(2);
        routerProve.Erase();

    }




}
