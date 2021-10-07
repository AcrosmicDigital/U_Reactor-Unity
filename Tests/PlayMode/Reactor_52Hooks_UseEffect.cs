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

public class Reactor_52Hooks_UseEffect
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
    public IEnumerator Hooks_UseEffect_10Basics()
    {

        // A Component
        REcanvas MainReactorComponent()
        {
            var count = 0;

            return new REcanvas
            {

                childs = () => new ReactorElement[] {
                        new REtext {
                            propsElementId = () => new ElementIdSetter{
                                id = "ProveText"
                            },
                            propsText = () => new TextSetter {
                                text = "Hello World!",
                            },
                            useEffect = new REtext.UseEffect.Hook[]{

                                new REtext.UseEffect.Hook {
                                    
                                    // Is executd each 1second per default in unscaled time mode
                                    deltaFunction = (s) => {
                                        count++;
                                        s.textCmp.text = "C: " + count;
                                    },

                                }

                            }
                        },
                    },

            };

        }

        var routerProve = MainReactorComponent();
        yield return new WaitForSecondsRealtime(1);

        // Draw the component
        routerProve.Draw();
        Debug.Log("Drawing");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "Hello World!");

        // First check and hook must be runner 1
        yield return new WaitForSecondsRealtime(1.2f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 1");

        // Second check and hook must be runner 2
        yield return new WaitForSecondsRealtime(1f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 2");

        // Thirh check and hook must be runner 3
        yield return new WaitForSecondsRealtime(1f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 3");

        // Time dont affect deltaUnscaledEffect
        Time.timeScale = .01f;


        // Four check and hook must be runner 4
        yield return new WaitForSecondsRealtime(1f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 4");

        // Five check and hook must be runner 5
        yield return new WaitForSecondsRealtime(1f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 5");


        // Time dont affect deltaUnscaledEffect
        Time.timeScale = 1f;


        // Four check and hook must be runner 6
        yield return new WaitForSecondsRealtime(1f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 6");

       // Five check and hook must be runner 7
        yield return new WaitForSecondsRealtime(1f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 7");




        yield return new WaitForSecondsRealtime(2);
        routerProve.Erase();


    }

    [UnityTest]
    public IEnumerator Hooks_UseEffect_11TimeMode()
    {

        // A Component
        REcanvas MainReactorComponent()
        {
            var count = 0;

            return new REcanvas
            {

                childs = () => new ReactorElement[] {
                        new REtext {
                            propsElementId = () => new ElementIdSetter{
                                id = "ProveText"
                            },
                            propsText = () => new TextSetter {
                                text = "Hello World!",
                            },
                            useEffect = new REtext.UseEffect.Hook[]{

                                new REtext.UseEffect.Hook {
                                    
                                    // time mode is unscaled time by default
                                    timeMode = ReactorHooks.TimeMode.DeltaTime,

                                    // Is executd each 1second per default affected by time
                                    deltaFunction = (s) => {
                                        count++;
                                        s.textCmp.text = "C: " + count;
                                    },

                                }

                            }
                        },
                    },

            };

        }

        var routerProve = MainReactorComponent();
        yield return new WaitForSecondsRealtime(1);

        // Draw the component
        routerProve.Draw();
        Debug.Log("Drawing");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "Hello World!");

        // First check and hook must be runner 1
        yield return new WaitForSecondsRealtime(1.2f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 1");

        // Second check and hook must be runner 2
        yield return new WaitForSecondsRealtime(1f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 2");

        // Thirh check and hook must be runner 3
        yield return new WaitForSecondsRealtime(1f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 3");

        // Time affect deltaUnscaledEffect
        Time.timeScale = .01f;

        // Four check and hook must not be runner because of time
        yield return new WaitForSecondsRealtime(1f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 3");
        // Five check and hook must not be runner because of time
        yield return new WaitForSecondsRealtime(1f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 3");

        // Time dont affect deltaUnscaledEffect
        Time.timeScale = 1f;

        // Four check and hook must be runner 6
        yield return new WaitForSecondsRealtime(1f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 4");

        // Five check and hook must be runner 7
        yield return new WaitForSecondsRealtime(1f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 5");



        yield return new WaitForSecondsRealtime(2);
        routerProve.Erase();


    }


    [UnityTest]
    public IEnumerator Hooks_UseEffect_12Duration()
    {

        // A Component
        REcanvas MainReactorComponent()
        {
            var count = 0;

            return new REcanvas
            {

                childs = () => new ReactorElement[] {
                        new REtext {
                            propsElementId = () => new ElementIdSetter{
                                id = "ProveText"
                            },
                            propsText = () => new TextSetter {
                                text = "Hello World!",
                            },
                            useEffect = new REtext.UseEffect.Hook[]{

                                new REtext.UseEffect.Hook {

                                    duration = 5,
                                    // Is executd each 1second per default in unscaled time mode
                                    deltaFunction = (s) => {
                                        count++;
                                        s.textCmp.text = "C: " + count;
                                    },

                                }

                            }
                        },
                    },

            };

        }

        var routerProve = MainReactorComponent();
        yield return new WaitForSecondsRealtime(1);

        // Draw the component
        routerProve.Draw();
        Debug.Log("Drawing");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "Hello World!");

        // First check and hook must be runner 1
        yield return new WaitForSecondsRealtime(5.2f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 1");

        // Second check and hook must be runner 2
        yield return new WaitForSecondsRealtime(5f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 2");

        // Thirh check and hook must be runner 3
        yield return new WaitForSecondsRealtime(5f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 3");





        yield return new WaitForSecondsRealtime(2);
        routerProve.Erase();


    }


    [UnityTest]
    public IEnumerator Hooks_UseEffect_13DeltaUnscaledEffect_IterationsCount()
    {

        // A Component
        REcanvas MainReactorComponent()
        {
            var count = 0;

            return new REcanvas
            {

                childs = () => new ReactorElement[] {
                        new REtext {
                            propsElementId = () => new ElementIdSetter{
                                id = "ProveText"
                            },
                            propsText = () => new TextSetter {
                                text = "Hello World!",
                            },
                            useEffect = new REtext.UseEffect.Hook[]{

                                new REtext.UseEffect.Hook {
                                    
                                    // After second time, hook will be destroyed
                                    iterationsCount = 2,

                                    // Is executd each 1second per default
                                    deltaFunction = (s) => {
                                        count++;
                                        s.textCmp.text = "C: " + count;
                                    },

                                }

                            }
                        },
                    },

            };

        }

        var routerProve = MainReactorComponent();
        yield return new WaitForSecondsRealtime(1);

        // Draw the component
        routerProve.Draw();
        Debug.Log("Drawing");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "Hello World!");

        // First check and hook must be runner 1
        yield return new WaitForSecondsRealtime(1.2f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 1");

        // Second check and hook must be runner 2
        yield return new WaitForSecondsRealtime(1f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 2");

        // Thirh check and hook not be runned again
        yield return new WaitForSecondsRealtime(1f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 2");

        // Time dont affect deltaUnscaledEffect
        Time.timeScale = .01f;


        // Four check and hook must be runner 4
        yield return new WaitForSecondsRealtime(1f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 2");

        // Five check and hook must be runner 5
        yield return new WaitForSecondsRealtime(1f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 2");


        // Time dont affect deltaUnscaledEffect
        Time.timeScale = 1f;


        // Four check and hook must be runner 6
        yield return new WaitForSecondsRealtime(1f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 2");

        // Five check and hook must be runner 7
        yield return new WaitForSecondsRealtime(1f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 2");




        yield return new WaitForSecondsRealtime(2);
        routerProve.Erase();


    }



    [UnityTest]
    public IEnumerator Hooks_UseEffect_15DeltaUnscaledEffectAndDeltaEffect_Basics()
    {

        // A Component
        REcanvas MainReactorComponent()
        {
            var count = 0;
            var otherCount = 0;

            return new REcanvas
            {

                childs = () => new ReactorElement[] {
                        new REtext {
                            propsElementId = () => new ElementIdSetter{
                                id = "ProveText"
                            },
                            propsText = () => new TextSetter {
                                text = "Hello World!",
                            },
                            useEffect = new REtext.UseEffect.Hook[]{

                                new REtext.UseEffect.Hook {
                                    
                                    // Is executd each 1second per default time speed affect it
                                    timeMode = ReactorHooks.TimeMode.DeltaTime,
                                    deltaFunction = (s) => {
                                        otherCount++;
                                        s.textCmp.fontSize = otherCount;
                                    },

                                },

                                new REtext.UseEffect.Hook {
                                    
                                    // Is executd each 1second per default
                                    deltaFunction = (s) => {
                                        count++;
                                        s.textCmp.text = "C: " + count;
                                    },

                                }

                            }
                        },
                    },

            };

        }

        var routerProve = MainReactorComponent();
        yield return new WaitForSecondsRealtime(1);

        // Draw the component
        routerProve.Draw();
        Debug.Log("Drawing");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "Hello World!");

        // First check and hook must be runner 1
        yield return new WaitForSecondsRealtime(1.2f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 1");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.fontSize == 1);

        // Second check and hook must be runner 2
        yield return new WaitForSecondsRealtime(1f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 2");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.fontSize == 2);

        // Thirh check and hook must be runner 3
        yield return new WaitForSecondsRealtime(1f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 3");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.fontSize == 3);

        // Time dont affect deltaUnscaledEffect
        Time.timeScale = .01f;


        // Four check and hook must be runner 4
        yield return new WaitForSecondsRealtime(1f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 4");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.fontSize == 3);

        // Five check and hook must be runner 5
        yield return new WaitForSecondsRealtime(1f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 5");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.fontSize == 3);


        // Time dont affect deltaUnscaledEffect
        Time.timeScale = 1f;


        // Four check and hook must be runner 6
        yield return new WaitForSecondsRealtime(1f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 6");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.fontSize == 4);

        // Five check and hook must be runner 7
        yield return new WaitForSecondsRealtime(1f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 7");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.fontSize == 5);




        yield return new WaitForSecondsRealtime(2);
        routerProve.Erase();


    }


    [UnityTest]
    public IEnumerator Hooks_UseEffect_16DeltaUnscaledEffect_ManyHooks()
    {

        // A Component
        REcanvas MainReactorComponent()
        {
            var count = 0;
            var count2 = 0;

            return new REcanvas
            {

                childs = () => new ReactorElement[] {
                        new REtext {
                            propsElementId = () => new ElementIdSetter{
                                id = "ProveText"
                            },
                            propsText = () => new TextSetter {
                                text = "Hello World!",
                            },
                            useEffect = new REtext.UseEffect.Hook[]{

                                new REtext.UseEffect.Hook {
                                    
                                    // Is executd each 1second per default
                                    deltaFunction = (s) => {
                                        count++;
                                        s.textCmp.text = "C: " + count;
                                    },

                                },

                                new REtext.UseEffect.Hook {
                                    
                                    // Is executd each 1second per default
                                    deltaFunction = (s) => {
                                        count2++;
                                        s.textCmp.fontSize = count2;
                                    },

                                }

                            }
                        },
                    },

            };

        }

        var routerProve = MainReactorComponent();
        yield return new WaitForSecondsRealtime(1);

        // Draw the component
        routerProve.Draw();
        Debug.Log("Drawing");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "Hello World!");

        // First check and hook must be runner 1
        yield return new WaitForSecondsRealtime(1.2f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 1");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.fontSize == 1);

        // Second check and hook must be runner 2
        yield return new WaitForSecondsRealtime(1f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 2");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.fontSize == 2);

        // Thirh check and hook must be runner 3
        yield return new WaitForSecondsRealtime(1f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 3");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.fontSize == 3);


        yield return new WaitForSecondsRealtime(2);
        routerProve.Erase();


    }




    [UnityTest]
    public IEnumerator Hooks_UseEffect_60ThrowErrorInDeltaFunction()
    {

        // A Component
        REcanvas MainReactorComponent()
        {
            var count = 0;

            return new REcanvas
            {

                childs = () => new ReactorElement[] {
                        new REtext {
                            propsElementId = () => new ElementIdSetter{
                                id = "ProveText"
                            },
                            propsText = () => new TextSetter {
                                text = "Hello World!",
                            },
                            useEffect = new REtext.UseEffect.Hook[]{

                                new REtext.UseEffect.Hook {
                                    
                                    // Is executd each 1second per default in unscaled time mode
                                    deltaFunction = (s) => {
                                        count++;
                                        s.textCmp.text = "C: " + count;
                                        throw new Exception("Expected error");
                                    },

                                }

                            }
                        },
                    },

            };

        }

        var routerProve = MainReactorComponent();
        yield return new WaitForSecondsRealtime(1);

        // Draw the component
        routerProve.Draw();
        Debug.Log("Drawing");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "Hello World!");

        // One message will be printed
        LogAssert.Expect(LogType.Error, new Regex("Expected error"));  // Regex - Contains that string


        // First check and hook must be runner 1
        yield return new WaitForSecondsRealtime(1.2f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 1");




        yield return new WaitForSecondsRealtime(2);
        routerProve.Erase();


    }



    [UnityTest]
    public IEnumerator Hooks_UseEffect_61ThrowErrorInvalidTime()
    {

        // A Component
        REcanvas MainReactorComponent()
        {
            var count = 0;

            return new REcanvas
            {

                childs = () => new ReactorElement[] {
                        new REtext {
                            propsElementId = () => new ElementIdSetter{
                                id = "ProveText"
                            },
                            propsText = () => new TextSetter {
                                text = "Hello World!",
                            },
                            useEffect = new REtext.UseEffect.Hook[]{

                                new REtext.UseEffect.Hook {
                                    
                                    // Is executd each 1second per default in unscaled time mode
                                    duration = 0,
                                    deltaFunction = (s) => {
                                        count++;
                                        s.textCmp.text = "C: " + count;
                                    },

                                },

                                new REtext.UseEffect.Hook {
                                    
                                    // Is executd each 1second per default in unscaled time mode
                                    duration = -12.2f,
                                    deltaFunction = (s) => {
                                        count++;
                                        s.textCmp.text = "C: " + count;
                                    },

                                }

                            }
                        },
                    },

            };

        }

        var routerProve = MainReactorComponent();
        yield return new WaitForSecondsRealtime(1);

        // Draw the component
        routerProve.Draw();
        Debug.Log("Drawing");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "Hello World!");

        // One message will be printed peer hook
        LogAssert.Expect(LogType.Error, new Regex("Duration cant be 0 or less"));  // Regex - Contains that string
        LogAssert.Expect(LogType.Error, new Regex("Duration cant be 0 or less"));  // Regex - Contains that string

        // First check and hook must be runner 1
        yield return new WaitForSecondsRealtime(1.2f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "Hello World!");



        yield return new WaitForSecondsRealtime(2);
        routerProve.Erase();


    }






    [UnityTest]
    public IEnumerator Hooks_UseEffect_30InHidedElements_RunNormally()
    {

        // A Component
        REcanvas MainReactorComponent()
        {
            var count = 0;

            return new REcanvas
            {

                childs = () => new ReactorElement[] {
                        new REtext {
                            propsElementId = () => new ElementIdSetter{
                                id = "ProveText"
                            },
                            propsText = () => new TextSetter {
                                text = "Hello World!",
                            },
                            useEffect = new REtext.UseEffect.Hook[]{

                                new REtext.UseEffect.Hook {
                                    
                                    // Is executd each 1second per default in unscaled time mode
                                    deltaFunction = (s) => {
                                        count++;
                                        s.textCmp.text = "C: " + count;
                                    },

                                }

                            }
                        },
                    },

            };

        }

        var routerProve = MainReactorComponent();
        yield return new WaitForSecondsRealtime(1);

        // Draw the component
        routerProve.Draw();
        Debug.Log("Drawing");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "Hello World!");

        // First check and hook must be runner 1
        yield return new WaitForSecondsRealtime(1.2f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 1");



        // Hide the element
        routerProve.Hide();
        Assert.IsFalse(((REcanvas.Selector)ReactorElement.Find("#ProveText")[0].root).canvas.enabled);

        // Second check and hook must be runner 2
        yield return new WaitForSecondsRealtime(1f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 2");




        // Enable and check
        routerProve.Show();
        Assert.IsTrue(((REcanvas.Selector)ReactorElement.Find("#ProveText")[0].root).canvas.enabled);

        // Thirh check and hook must be runner 3
        yield return new WaitForSecondsRealtime(1f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 3");





        yield return new WaitForSecondsRealtime(2);
        routerProve.Erase();


    }

    [UnityTest]
    public IEnumerator Hooks_UseEffect_31InDisabledElements_DontRun()
    {

        // A Component
        REcanvas MainReactorComponent()
        {
            var count = 0;

            return new REcanvas
            {

                childs = () => new ReactorElement[] {
                        new REtext {
                            propsElementId = () => new ElementIdSetter{
                                id = "ProveText"
                            },
                            propsText = () => new TextSetter {
                                text = "Hello World!",
                            },
                            useEffect = new REtext.UseEffect.Hook[]{

                                new REtext.UseEffect.Hook {
                                    
                                    // Is executd each 1second per default in unscaled time mode
                                    deltaFunction = (s) => {
                                        count++;
                                        s.textCmp.text = "C: " + count;
                                    },

                                }

                            }
                        },
                    },

            };

        }

        var routerProve = MainReactorComponent();
        yield return new WaitForSecondsRealtime(1);

        // Draw the component
        routerProve.Draw();
        Debug.Log("Drawing");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "Hello World!");

        // First check and hook must be runner 1
        yield return new WaitForSecondsRealtime(1.2f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 1");



        // Hide the element
        routerProve.Disable();
        Assert.IsFalse(ReactorElement.Find("#ProveText")[0].gameObject.activeInHierarchy);


        // Second check and hook must be runner 2
        yield return new WaitForSecondsRealtime(1f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 1");




        // Enable and check
        routerProve.Enable();
        Assert.IsTrue(ReactorElement.Find("#ProveText")[0].gameObject.activeInHierarchy);

        // Thirh check and hook must be runner 3
        yield return new WaitForSecondsRealtime(1f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 2");





        yield return new WaitForSecondsRealtime(2);
        routerProve.Erase();


    }

    [UnityTest]
    public IEnumerator Hooks_UseEffect_32InErasedElements_WillBeErasedToo()
    {

        // A Component
        REcanvas MainReactorComponent()
        {
            var count = 0;

            return new REcanvas
            {

                childs = () => new ReactorElement[] {
                        new REtext {
                            propsElementId = () => new ElementIdSetter{
                                id = "ProveText"
                            },
                            propsText = () => new TextSetter {
                                text = "Hello World!",
                            },
                            useEffect = new REtext.UseEffect.Hook[]{

                                new REtext.UseEffect.Hook {
                                    
                                    // Is executd each 1second per default in unscaled time mode
                                    deltaFunction = (s) => {
                                        count++;
                                        s.textCmp.text = "C: " + count;
                                    },

                                }

                            }
                        },
                    },

            };

        }

        var routerProve = MainReactorComponent();
        yield return new WaitForSecondsRealtime(1);

        // Draw the component
        routerProve.Draw();
        Debug.Log("Drawing");
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "Hello World!");

        // First check and hook must be runner 1
        yield return new WaitForSecondsRealtime(1.2f);
        Assert.IsTrue(REtext.FindOne("#ProveText").textCmp.text == "C: 1");



        // Hide the element
        routerProve.Erase();

        // Second check and hook must be runner 2
        yield return new WaitForSecondsRealtime(3f);
        Assert.IsTrue(REtext.FindOne("#ProveText") == null);




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

                childs = () => new ReactorElement[] {
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




}
