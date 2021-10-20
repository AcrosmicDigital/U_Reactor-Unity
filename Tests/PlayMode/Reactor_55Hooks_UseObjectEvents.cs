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

public class Reactor_55Hooks_UseObjectEvents
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



    [UnityTest]
    public IEnumerator Hooks_UseObjectEvents_10Basics()
    {

        // A Component
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
                                text = "Hello World!",
                            },
                            useObjectEvents = new REtext.UseObjectEvents.Hook
                            {
                               onStart = (s) => Debug.LogAssertion("onStart"),
                               onAwake = (s) => Debug.LogAssertion("onAwake"),
                               onDestroy = (s) => Debug.LogAssertion("onDestroy"),
                               onDisable = (s) => Debug.LogAssertion("onDisable"),
                               onEnable = (s) => Debug.LogAssertion("onEnable"),
                            }
                        },
                    },

            };

        }

        var routerProve = MainReactorComponent();
        yield return new WaitForSecondsRealtime(1);


        // Messages
        LogAssert.Expect(LogType.Assert, new Regex("onAwake"));
        LogAssert.Expect(LogType.Assert, new Regex("onEnable"));
        LogAssert.Expect(LogType.Assert, new Regex("onStart"));


        // Draw the component
        Debug.Log("Drawing");
        routerProve.Draw();




        yield return new WaitForSecondsRealtime(2);
        routerProve.Erase();

        // Messages
        LogAssert.Expect(LogType.Assert, new Regex("onDisable"));
        LogAssert.Expect(LogType.Assert, new Regex("onDestroy"));

    }


    [UnityTest]
    public IEnumerator Hooks_UseObjectEvents_60ThrowErrorInFunctions()
    {

        // A Component
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
                                text = "Hello World!",
                            },
                            useObjectEvents = new REtext.UseObjectEvents.Hook
                            {
                               onStart = (s) => throw new Exception("onStart"),
                               onAwake = (s) => throw new Exception("onAwake"),
                               onDestroy = (s) => throw new Exception("onDestroy"),
                               onDisable = (s) => throw new Exception("onDisable"),
                               onEnable = (s) => throw new Exception("onEnable"),
                            }
                        },
                    },

            };

        }

        var routerProve = MainReactorComponent();
        yield return new WaitForSecondsRealtime(1);


        // Messages
        LogAssert.Expect(LogType.Exception, new Regex("onAwake"));
        LogAssert.Expect(LogType.Exception, new Regex("onEnable"));
        LogAssert.Expect(LogType.Exception, new Regex("onStart"));


        // Draw the component
        Debug.Log("Drawing");
        routerProve.Draw();




        yield return new WaitForSecondsRealtime(2);
        routerProve.Erase();

        // Messages
        LogAssert.Expect(LogType.Exception, new Regex("onDisable"));
        LogAssert.Expect(LogType.Exception, new Regex("onDestroy"));

    }


    [UnityTest]
    public IEnumerator Hooks_UseObjectEvents_30WhenHideElements()
    {

        // A Component
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
                                text = "Hello World!",
                            },
                            useObjectEvents = new REtext.UseObjectEvents.Hook
                            {
                               onStart = (s) => Debug.LogAssertion("onStart"),
                               onAwake = (s) => Debug.LogAssertion("onAwake"),
                               onDestroy = (s) => Debug.LogAssertion("onDestroy"),
                               onDisable = (s) => Debug.LogAssertion("onDisable"),
                               onEnable = (s) => Debug.LogAssertion("onEnable"),
                            }
                        },
                    },

            };

        }

        var routerProve = MainReactorComponent();
        yield return new WaitForSecondsRealtime(1);


        // Messages
        LogAssert.Expect(LogType.Assert, new Regex("onAwake"));
        LogAssert.Expect(LogType.Assert, new Regex("onEnable"));
        LogAssert.Expect(LogType.Assert, new Regex("onStart"));


        // Draw the component
        Debug.Log("Drawing");
        routerProve.Draw();


        // Messages
        routerProve.Hide();
        yield return new WaitForSecondsRealtime(1);
        routerProve.Show();





        yield return new WaitForSecondsRealtime(2);
        routerProve.Erase();

        // Messages
        LogAssert.Expect(LogType.Assert, new Regex("onDisable"));
        LogAssert.Expect(LogType.Assert, new Regex("onDestroy"));

    }


    [UnityTest]
    public IEnumerator Hooks_UseObjectEvents_31WhenDisableElements()
    {

        // A Component
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
                                text = "Hello World!",
                            },
                            useObjectEvents = new REtext.UseObjectEvents.Hook
                            {
                               onStart = (s) => Debug.LogAssertion("onStart"),
                               onAwake = (s) => Debug.LogAssertion("onAwake"),
                               onDestroy = (s) => Debug.LogAssertion("onDestroy"),
                               onDisable = (s) => Debug.LogAssertion("onDisable"),
                               onEnable = (s) => Debug.LogAssertion("onEnable"),
                            }
                        },
                    },

            };

        }

        var routerProve = MainReactorComponent();
        yield return new WaitForSecondsRealtime(1);


        // Messages
        LogAssert.Expect(LogType.Assert, new Regex("onAwake"));
        LogAssert.Expect(LogType.Assert, new Regex("onEnable"));
        LogAssert.Expect(LogType.Assert, new Regex("onStart"));


        // Draw the component
        Debug.Log("Drawing");
        routerProve.Draw();

        yield return new WaitForSecondsRealtime(1);


        // Messages
        LogAssert.Expect(LogType.Assert, new Regex("onDisable"));
        routerProve.Disable();
        yield return new WaitForSecondsRealtime(1);
        LogAssert.Expect(LogType.Assert, new Regex("onEnable"));
        routerProve.Enable();





        yield return new WaitForSecondsRealtime(2);
        routerProve.Erase();

        // Messages
        LogAssert.Expect(LogType.Assert, new Regex("onDisable"));
        LogAssert.Expect(LogType.Assert, new Regex("onDestroy"));

    }



}
