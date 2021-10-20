using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using U.Reactor;
using UnityEngine.EventSystems;
using System;
using System.Linq;

public class Reactor_40Router
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



    #region Components for the router



    REcanvas Menu()
    {
        return new REcanvas
        {
            propsReactorId = () => new ReactorIdBSetter { id = "Menu" },
            propsGameObject = () => new REcanvas.GameObjectSetter { name = "Canvas-Menu", },
            childs = () => new REbase[] {
                    new REtext {
                        propsText = () => new REtext.TextSetter {
                            text = "Menu",
                        },
                    },
                },
        };
    }

    REcanvas Home()
    {
        return new REcanvas
        {
            propsReactorId = () => new ReactorIdBSetter { id = "Home" },
            propsGameObject = () => new REcanvas.GameObjectSetter { name = "Canvas-Home", },
            childs = () => new REbase[] {
                    new REtext {
                        propsText = () => new REtext.TextSetter {
                            text = "Home",
                        },
                    },
                },


        };
    }

    REcanvas Settings()
    {
        return new REcanvas
        {
            propsReactorId = () => new ReactorIdBSetter { id = "Settings" },
            propsGameObject = () => new REcanvas.GameObjectSetter { name = "Canvas-Settings", },
            childs = () => new REbase[] {
                    new REtext {
                        propsText = () => new REtext.TextSetter {
                            text = "Settings",
                        },
                    },
                },


        };
    }

    REcanvas Default()
    {
        return new REcanvas
        {
            propsReactorId = () => new ReactorIdBSetter { id = "Default" },
            propsGameObject = () => new REcanvas.GameObjectSetter { name = "Canvas-Default", },
            childs = () => new REbase[] {
                    new REtext {
                        propsText = () => new REtext.TextSetter {
                            text = "Default",
                        },
                    },
                },


        };
    }


    #endregion




    // Chekar e erase,hide,disable y eso


    [UnityTest]
    public IEnumerator ReactorRouter_10Basics_EraseMode()
    {

        
        // Creating the router
        yield return new WaitForSecondsRealtime(1);
        ReactorRouter RouterProveReactorComponent()
        {

            return new ReactorRouter
            {
                routes = new Dictionary<string, REcanvas>()
                    {
                        {"Menu", Menu()},
                        {"Home", Home()},
                        {"Settings", Settings()},
                        {"Default", Default()}
                    },
                defaultRoute = "Default",
            };

        }
        var routerProve = RouterProveReactorComponent();



        // Go to Home
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Home");
        routerProve.Route("Home");
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0);

        // Go to Menu
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Menu");
        routerProve.Route("Menu");
        Assert.IsTrue(REcanvas.Find("#Home").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0);

        // Go to Settings
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Settings");
        routerProve.Route("Settings");
        Assert.IsTrue(REcanvas.Find("#Home").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1);
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0);

        // Return to Menu
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Return to Menu");
        routerProve.Return();
        Assert.IsTrue(REcanvas.Find("#Home").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0);

        // Go to Settings
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Settings");
        routerProve.Route("Settings");
        Assert.IsTrue(REcanvas.Find("#Home").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1);
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0);

        // Return to Home
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Return to Home");
        routerProve.Return(2);
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0);

        // Return more, but first is Home
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Return more, but first is Home");
        routerProve.Return(10);
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0);

        // Go to unexistent Route
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to unexistent Route");
        routerProve.Route("Unexistent");
        Assert.IsTrue(REcanvas.Find("#Home").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1);

        // Go to Settings
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Settings");
        routerProve.Route("Settings");
        Assert.IsTrue(REcanvas.Find("#Home").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1);
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0);

        // Return to unexistent Route
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to unexistent Route");
        routerProve.Return();
        Assert.IsTrue(REcanvas.Find("#Home").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1);


        // Delete the components
        yield return new WaitForSecondsRealtime(1);
        routerProve.Erase();

    }



    [UnityTest]
    public IEnumerator ReactorRouter_10Basics_InEraseMode_PredrawHasNoEffect()
    {


        // Creating the router
        yield return new WaitForSecondsRealtime(1);
        ReactorRouter RouterProveReactorComponent()
        {

            return new ReactorRouter
            {
                routes = new Dictionary<string, REcanvas>()
                    {
                        {"Menu", Menu()},
                        {"Home", Home()},
                        {"Settings", Settings()},
                        {"Default", Default()}
                    },
                defaultRoute = "Default",
            };

        }
        var routerProve = RouterProveReactorComponent();

        routerProve.PreDraw();

        // Go to Home
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Home");
        routerProve.Route("Home");
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0);


        routerProve.PreDraw();


        // Go to Menu
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Menu");
        routerProve.Route("Menu");
        Assert.IsTrue(REcanvas.Find("#Home").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0);

        routerProve.PreDraw();


        // Go to Settings
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Settings");
        routerProve.Route("Settings");
        Assert.IsTrue(REcanvas.Find("#Home").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1);
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0);

        // Return to Menu
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Return to Menu");
        routerProve.Return();
        Assert.IsTrue(REcanvas.Find("#Home").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0);

        routerProve.PreDraw();


        // Go to Settings
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Settings");
        routerProve.Route("Settings");
        Assert.IsTrue(REcanvas.Find("#Home").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1);
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0);

        // Return to Home
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Return to Home");
        routerProve.Return(2);
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0);

        routerProve.PreDraw();


        // Return more, but first is Home
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Return more, but first is Home");
        routerProve.Return(10);
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0);

        routerProve.PreDraw();


        // Delete the components
        yield return new WaitForSecondsRealtime(1);
        routerProve.Erase();

    }



    [UnityTest]
    public IEnumerator ReactorRouter_10Basics_HideMode_NoPreDraw()
    {


        // Creating the router
        yield return new WaitForSecondsRealtime(1);
        ReactorRouter RouterProveReactorComponent()
        {

            return new ReactorRouter
            {
                routerMode = ReactorRouter.RouterMode.Hide,
                routes = new Dictionary<string, REcanvas>()
                    {
                        {"Menu", Menu()},
                        {"Home", Home()},
                        {"Settings", Settings()},
                        {"Default", Default()}
                    },
                defaultRoute = "Default",
            };

        }
        var routerProve = RouterProveReactorComponent();



        // Go to Home
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Home");
        routerProve.Route("Home");
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1);
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Home").root).canvas.enabled); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0);  // Only is one in scene

        // Go to Menu
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Menu");
        routerProve.Route("Menu");
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Home").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Only is one in scene
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Menu").root).canvas.enabled); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0); // Dont exist, because is not predrawed and are not routed yet

        // Go to Settings
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Settings");
        routerProve.Route("Settings");
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Home").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Menu").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // The canvas is enabled
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Settings").root).canvas.enabled); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0); // Dont exist, because is not predrawed and are not routed yet

        // Return to Menu
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Return to Menu");
        routerProve.Return();
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Home").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Still existing, but disabled
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Menu").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // The canvas is enabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Settings").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0); // Dont exist, because is not predrawed and are not routed yet

        // Go to Settings
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Settings");
        routerProve.Route("Settings");
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Home").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Menu").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // The canvas is enabled
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Settings").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0); // Dont exist, because is not predrawed and are not routed yet

        // Return to Home
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Return to Home");
        routerProve.Return(2);
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1); // Still existing, but disabled
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Home").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Menu").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // The canvas is enabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Settings").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0); // Dont exist, because is not predrawed and are not routed yet

        // Return more, but first is Home
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Return more, but first is Home");
        routerProve.Return(10);
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1); // Still existing, but disabled
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Home").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Menu").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // The canvas is enabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Settings").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0); // Dont exist, because is not predrawed and are not routed yet


        // Go to unexistent Route
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to unexistent Route");
        routerProve.Route("Unexistent");
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Home").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Menu").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // The canvas is enabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Settings").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Default").root).canvas.enabled); // The canvas is disabled
        
        // Go to Settings
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Settings");
        routerProve.Route("Settings");
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Home").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Menu").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // The canvas is enabled
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Settings").root).canvas.enabled); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Default").root).canvas.enabled); // The canvas is disabled

        // Return to unexistent Route
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Return to unexistent Route");
        routerProve.Return();
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Home").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Menu").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // The canvas is enabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Settings").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Default").root).canvas.enabled); // The canvas is disabled





        yield return new WaitForSecondsRealtime(1);
        routerProve.Erase();

    }



    [UnityTest]
    public IEnumerator ReactorRouter_10Basics_DisableMode_NoPreDraw()
    {


        // Creating the router
        yield return new WaitForSecondsRealtime(1);
        ReactorRouter RouterProveReactorComponent()
        {

            return new ReactorRouter
            {
                routerMode = ReactorRouter.RouterMode.Disable,
                routes = new Dictionary<string, REcanvas>()
                    {
                        {"Menu", Menu()},
                        {"Home", Home()},
                        {"Settings", Settings()},
                        {"Default", Default()}
                    },
                defaultRoute = "Default",
            };

        }
        var routerProve = RouterProveReactorComponent();



        // Go to Home
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Home");
        routerProve.Route("Home");
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1);
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Home").root).gameObject.activeSelf); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0);  // Only is one in scene

        // Go to Menu
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Menu");
        routerProve.Route("Menu");
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Home").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Only is one in scene
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Menu").root).gameObject.activeSelf); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0); // Dont exist, because is not predrawed and are not routed yet

        // Go to Settings
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Settings");
        routerProve.Route("Settings");
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Home").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Menu").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // The canvas is enabled
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Settings").root).gameObject.activeSelf); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0); // Dont exist, because is not predrawed and are not routed yet

        // Return to Menu
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Return to Menu");
        routerProve.Return();
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Home").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Still existing, but disabled
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Menu").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // The canvas is enabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Settings").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0); // Dont exist, because is not predrawed and are not routed yet

        // Go to Settings
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Settings");
        routerProve.Route("Settings");
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Home").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Menu").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // The canvas is enabled
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Settings").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0); // Dont exist, because is not predrawed and are not routed yet

        // Return to Home
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Return to Home");
        routerProve.Return(2);
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1); // Still existing, but disabled
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Home").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Menu").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // The canvas is enabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Settings").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0); // Dont exist, because is not predrawed and are not routed yet

        // Return more, but first is Home
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Return more, but first is Home");
        routerProve.Return(10);
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1); // Still existing, but disabled
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Home").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Menu").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // The canvas is enabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Settings").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0); // Dont exist, because is not predrawed and are not routed yet

        // Go to unexistent Route
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to unexistent Route");
        routerProve.Route("Unexistent");
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Home").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Menu").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // The canvas is enabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Settings").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Default").root).gameObject.activeSelf); // The canvas is disabled


        // Go to Settings
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Settings");
        routerProve.Route("Settings");
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Home").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Menu").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // The canvas is enabled
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Settings").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Default").root).gameObject.activeSelf); // The canvas is disabled

        // Return to unexistent Route
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Return to unexistent Route");
        routerProve.Route("Unexistent");
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Home").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Menu").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // The canvas is enabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Settings").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Default").root).gameObject.activeSelf); // The canvas is disabled





        yield return new WaitForSecondsRealtime(1);
        routerProve.Erase();

    }




    [UnityTest]
    public IEnumerator ReactorRouter_10Basics_HideMode_WithPreDraw()
    {


        // Creating the router
        yield return new WaitForSecondsRealtime(1);
        ReactorRouter RouterProveReactorComponent()
        {

            return new ReactorRouter
            {
                routerMode = ReactorRouter.RouterMode.Hide,
                routes = new Dictionary<string, REcanvas>()
                    {
                        {"Menu", Menu()},
                        {"Home", Home()},
                        {"Settings", Settings()},
                        {"Default", Default()}
                    },
                defaultRoute = "Default",
            };

        }
        var routerProve = RouterProveReactorComponent();


        // Call predraw before any route
        Debug.Log("Predraw");
        routerProve.PreDraw();
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1);
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Home").root).canvas.enabled); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Menu").root).canvas.enabled); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Settings").root).canvas.enabled); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Default").root).canvas.enabled); // The canvas is enabled

        // Go to Home
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Home");
        routerProve.Route("Home");
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1);
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Home").root).canvas.enabled); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Menu").root).canvas.enabled); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Settings").root).canvas.enabled); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Default").root).canvas.enabled); // The canvas is enabled

        // Go to Menu
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Menu");
        routerProve.Route("Menu");
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1);
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Home").root).canvas.enabled); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Menu").root).canvas.enabled); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Settings").root).canvas.enabled); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Default").root).canvas.enabled); // The canvas is enabled

        // Go to Settings
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Settings");
        routerProve.Route("Settings");
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Home").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Menu").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // The canvas is enabled
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Settings").root).canvas.enabled); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Default").root).canvas.enabled); // The canvas is enabled

        // Return to Menu
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Return to Menu");
        routerProve.Return();
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Home").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Still existing, but disabled
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Menu").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // The canvas is enabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Settings").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Default").root).canvas.enabled); // The canvas is enabled

        // Go to Settings
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Settings");
        routerProve.Route("Settings");
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Home").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Menu").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // The canvas is enabled
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Settings").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Default").root).canvas.enabled); // The canvas is enabled

        // Return to Home
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Return to Home");
        routerProve.Return(2);
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1); // Still existing, but disabled
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Home").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Menu").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // The canvas is enabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Settings").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Default").root).canvas.enabled); // The canvas is enabled

        // Return more, but first is Home
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Return more, but first is Home");
        routerProve.Return(10);
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1); // Still existing, but disabled
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Home").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Menu").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // The canvas is enabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Settings").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Default").root).canvas.enabled); // The canvas is enabled






        yield return new WaitForSecondsRealtime(1);
        routerProve.Erase();

    }



    [UnityTest]
    public IEnumerator ReactorRouter_10Basics_DisableMode_WithPreDraw()
    {


        // Creating the router
        yield return new WaitForSecondsRealtime(1);
        ReactorRouter RouterProveReactorComponent()
        {

            return new ReactorRouter
            {
                routerMode = ReactorRouter.RouterMode.Disable,
                routes = new Dictionary<string, REcanvas>()
                    {
                        {"Menu", Menu()},
                        {"Home", Home()},
                        {"Settings", Settings()},
                        {"Default", Default()}
                    },
                defaultRoute = "Default",
            };

        }
        var routerProve = RouterProveReactorComponent();


        // Call predraw before any route
        Debug.Log("Predraw");
        routerProve.PreDraw();
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1);
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Home").root).gameObject.activeSelf); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Menu").root).gameObject.activeSelf); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Settings").root).gameObject.activeSelf); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Default").root).gameObject.activeSelf); // The canvas is enabled


        // Go to Home
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Home");
        routerProve.Route("Home");
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1);
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Home").root).gameObject.activeSelf); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Menu").root).gameObject.activeSelf); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Settings").root).gameObject.activeSelf); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Default").root).gameObject.activeSelf); // The canvas is enabled


        // Go to Menu
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Menu");
        routerProve.Route("Menu");
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1);
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Home").root).gameObject.activeSelf); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Menu").root).gameObject.activeSelf); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Settings").root).gameObject.activeSelf); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Default").root).gameObject.activeSelf); // The canvas is enabled


        // Go to Settings
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Settings");
        routerProve.Route("Settings");
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Home").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Menu").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // The canvas is enabled
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Settings").root).gameObject.activeSelf); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Default").root).gameObject.activeSelf); // The canvas is enabled

        // Return to Menu
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Return to Menu");
        routerProve.Return();
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Home").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Still existing, but disabled
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Menu").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // The canvas is enabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Settings").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Default").root).gameObject.activeSelf); // The canvas is enabled

        // Go to Settings
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Settings");
        routerProve.Route("Settings");
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Home").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Menu").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // The canvas is enabled
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Settings").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Default").root).gameObject.activeSelf); // The canvas is enabled

        // Return to Home
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Return to Home");
        routerProve.Return(2);
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1); // Still existing, but disabled
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Home").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Menu").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // The canvas is enabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Settings").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Default").root).gameObject.activeSelf); // The canvas is enabled

        // Return more, but first is Home
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Return more, but first is Home");
        routerProve.Return(10);
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1); // Still existing, but disabled
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Home").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Menu").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // The canvas is enabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Settings").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Default").root).gameObject.activeSelf); // The canvas is enabled





        yield return new WaitForSecondsRealtime(1);
        routerProve.Erase();

    }



    [UnityTest]
    public IEnumerator ReactorRouter_10Basics_InHideMode_CallingPreAfterRoutesDrawHasNoefect()
    {


        // Creating the router
        yield return new WaitForSecondsRealtime(1);
        ReactorRouter RouterProveReactorComponent()
        {

            return new ReactorRouter
            {
                routerMode = ReactorRouter.RouterMode.Hide,
                routes = new Dictionary<string, REcanvas>()
                    {
                        {"Menu", Menu()},
                        {"Home", Home()},
                        {"Settings", Settings()},
                        {"Default", Default()}
                    },
                defaultRoute = "Default",
            };

        }
        var routerProve = RouterProveReactorComponent();


        // Call predraw before any route
        Debug.Log("Predraw");
        routerProve.PreDraw();
        routerProve.PreDraw();
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1);
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Home").root).canvas.enabled); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Menu").root).canvas.enabled); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Settings").root).canvas.enabled); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Default").root).canvas.enabled); // The canvas is enabled

        // Go to Home
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Home");
        routerProve.Route("Home");
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1);
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Home").root).canvas.enabled); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Menu").root).canvas.enabled); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Settings").root).canvas.enabled); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Default").root).canvas.enabled); // The canvas is enabled


        routerProve.PreDraw();



        // Go to Menu
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Menu");
        routerProve.Route("Menu");
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1);
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Home").root).canvas.enabled); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Menu").root).canvas.enabled); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Settings").root).canvas.enabled); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Default").root).canvas.enabled); // The canvas is enabled



        routerProve.PreDraw();


        // Go to Settings
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Settings");
        routerProve.Route("Settings");
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Home").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Menu").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // The canvas is enabled
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Settings").root).canvas.enabled); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Default").root).canvas.enabled); // The canvas is enabled

        // Return to Menu
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Return to Menu");
        routerProve.Return();
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Home").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Still existing, but disabled
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Menu").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // The canvas is enabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Settings").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Default").root).canvas.enabled); // The canvas is enabled


        routerProve.PreDraw();


        // Go to Settings
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Settings");
        routerProve.Route("Settings");
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Home").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Menu").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // The canvas is enabled
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Settings").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Default").root).canvas.enabled); // The canvas is enabled

        // Return to Home
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Return to Home");
        routerProve.Return(2);
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1); // Still existing, but disabled
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Home").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Menu").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // The canvas is enabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Settings").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Default").root).canvas.enabled); // The canvas is enabled


        routerProve.PreDraw();


        // Return more, but first is Home
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Return more, but first is Home");
        routerProve.Return(10);
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1); // Still existing, but disabled
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Home").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Menu").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // The canvas is enabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Settings").root).canvas.enabled); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Default").root).canvas.enabled); // The canvas is enabled






        yield return new WaitForSecondsRealtime(1);
        routerProve.Erase();

    }


    [UnityTest]
    public IEnumerator ReactorRouter_10Basics_InDisableMode_CallingPreAfterRoutesDrawHasNoefect()
    {


        // Creating the router
        yield return new WaitForSecondsRealtime(1);
        ReactorRouter RouterProveReactorComponent()
        {

            return new ReactorRouter
            {
                routerMode = ReactorRouter.RouterMode.Disable,
                routes = new Dictionary<string, REcanvas>()
                    {
                        {"Menu", Menu()},
                        {"Home", Home()},
                        {"Settings", Settings()},
                        {"Default", Default()}
                    },
                defaultRoute = "Default",
            };

        }
        var routerProve = RouterProveReactorComponent();


        // Call predraw before any route
        Debug.Log("Predraw");
        routerProve.PreDraw();
        routerProve.PreDraw();
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1);
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Home").root).gameObject.activeSelf); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Menu").root).gameObject.activeSelf); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Settings").root).gameObject.activeSelf); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Default").root).gameObject.activeSelf); // The canvas is enabled


        // Go to Home
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Home");
        routerProve.Route("Home");
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1);
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Home").root).gameObject.activeSelf); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Menu").root).gameObject.activeSelf); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Settings").root).gameObject.activeSelf); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Default").root).gameObject.activeSelf); // The canvas is enabled


        routerProve.PreDraw();


        // Go to Menu
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Menu");
        routerProve.Route("Menu");
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1);
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Home").root).gameObject.activeSelf); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Menu").root).gameObject.activeSelf); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Settings").root).gameObject.activeSelf); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Default").root).gameObject.activeSelf); // The canvas is enabled


        routerProve.PreDraw();


        // Go to Settings
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Settings");
        routerProve.Route("Settings");
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Home").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Menu").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // The canvas is enabled
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Settings").root).gameObject.activeSelf); // The canvas is enabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Default").root).gameObject.activeSelf); // The canvas is enabled

        // Return to Menu
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Return to Menu");
        routerProve.Return();
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Home").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Still existing, but disabled
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Menu").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // The canvas is enabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Settings").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Default").root).gameObject.activeSelf); // The canvas is enabled


        routerProve.PreDraw();


        // Go to Settings
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Settings");
        routerProve.Route("Settings");
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Home").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Menu").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // The canvas is enabled
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Settings").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Default").root).gameObject.activeSelf); // The canvas is enabled

        // Return to Home
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Return to Home");
        routerProve.Return(2);
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1); // Still existing, but disabled
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Home").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Menu").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // The canvas is enabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Settings").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Default").root).gameObject.activeSelf); // The canvas is enabled


        routerProve.PreDraw();


        // Return more, but first is Home
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Return more, but first is Home");
        routerProve.Return(10);
        yield return new WaitForSecondsRealtime(.1f); // Need  to wait a bit, because canvas is enabled at next frame, no immediatly
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1); // Still existing, but disabled
        Assert.IsTrue(((REcanvas.Selector)REbase.FindOne("#Home").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1); // Still existing, but disabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Menu").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1); // The canvas is enabled
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Settings").root).gameObject.activeSelf); // The canvas is disabled
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1); // Dont exist, because is not predrawed and are not routed yet
        Assert.IsFalse(((REcanvas.Selector)REbase.FindOne("#Default").root).gameObject.activeSelf); // The canvas is enabled


        routerProve.PreDraw();


        yield return new WaitForSecondsRealtime(1);
        routerProve.Erase();

    }








    [UnityTest]
    public IEnumerator ReactorRouter_10Basics_Reload()
    {


        // Creating the router
        yield return new WaitForSecondsRealtime(1);
        ReactorRouter RouterProveReactorComponent()
        {

            return new ReactorRouter
            {
                routes = new Dictionary<string, REcanvas>()
                    {
                        {"Menu", Menu()},
                        {"Home", Home()},
                        {"Settings", Settings()},
                        {"Default", Default()}
                    },
                defaultRoute = "Default",
            };

        }
        var routerProve = RouterProveReactorComponent();


        // If reload without any prev route , will load default, but not added to history
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Reload without any prev route");
        routerProve.Route();
        Assert.IsTrue(REcanvas.Find("#Home").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1);




        // Go to Home
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Home");
        routerProve.Route("Home");
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0);


        // Will reload Home
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Reloaded");
        routerProve.Route();
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0);



        // Go to Menu
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Menu");
        routerProve.Route("Menu");
        Assert.IsTrue(REcanvas.Find("#Home").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0);

        // Go to Settings
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Settings");
        routerProve.Route("Settings");
        Assert.IsTrue(REcanvas.Find("#Home").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1);
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0);



        // Will reload Settings
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Reload Settings");
        routerProve.Route();
        Assert.IsTrue(REcanvas.Find("#Home").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1);
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0);



        // Return to Menu
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Return to Menu");
        routerProve.Return();
        Assert.IsTrue(REcanvas.Find("#Home").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0);

        // Go to Settings
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Settings");
        routerProve.Route("Settings");
        Assert.IsTrue(REcanvas.Find("#Home").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1);
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0);

        // Return to Home
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Return to Home");
        routerProve.Return(2);
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0);

        // Return more, but first is Home
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Return more, but first is Home");
        routerProve.Return(10);
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0);

        // Go to unexistent Route
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to unexistent Route");
        routerProve.Route("Unexistent");
        Assert.IsTrue(REcanvas.Find("#Home").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1);



        // Will reload unexistent
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Reload unexistent");
        routerProve.Route();
        Assert.IsTrue(REcanvas.Find("#Home").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1);




        // Go to Settings
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Settings");
        routerProve.Route("Settings");
        Assert.IsTrue(REcanvas.Find("#Home").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1);
        Assert.IsTrue(REcanvas.Find("#Default").Length == 0);

        // Return to unexistent Route
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to unexistent Route");
        routerProve.Return();
        Assert.IsTrue(REcanvas.Find("#Home").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Default").Length == 1);


        // Delete the components
        yield return new WaitForSecondsRealtime(1);
        routerProve.Erase();

    }


    [UnityTest]
    public IEnumerator ReactorRouter_10Basics_NoDefaultRoute()
    {


        // Creating the router
        yield return new WaitForSecondsRealtime(1);
        ReactorRouter RouterProveReactorComponent()
        {

            return new ReactorRouter
            {
                routes = new Dictionary<string, REcanvas>()
                    {
                        {"Menu", Menu()},
                        {"Home", Home()},
                        {"Settings", Settings()},
                        {"Default", Default()}
                    },
            };

        }
        var routerProve = RouterProveReactorComponent();


        // If reload without any prev route , will load default, but not added to history
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Reload without any prev route");
        routerProve.Route();
        Assert.IsTrue(REcanvas.Find("#Home").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0);




        // Go to Home
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Home");
        routerProve.Route("Home");
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0);


        // Will reload Home
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Reloaded");
        routerProve.Route();
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0);



        // Go to Menu
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Menu");
        routerProve.Route("Menu");
        Assert.IsTrue(REcanvas.Find("#Home").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0);

        // Go to Settings
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Settings");
        routerProve.Route("Settings");
        Assert.IsTrue(REcanvas.Find("#Home").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1);



        // Will reload Settings
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Reload Settings");
        routerProve.Route();
        Assert.IsTrue(REcanvas.Find("#Home").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1);



        // Return to Menu
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Return to Menu");
        routerProve.Return();
        Assert.IsTrue(REcanvas.Find("#Home").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0);

        // Go to Settings
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Settings");
        routerProve.Route("Settings");
        Assert.IsTrue(REcanvas.Find("#Home").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1);

        // Return to Home
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Return to Home");
        routerProve.Return(2);
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0);

        // Return more, but first is Home
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Return more, but first is Home");
        routerProve.Return(10);
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0);

        // Go to unexistent Route
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to unexistent Route");
        routerProve.Route("Unexistent");
        Assert.IsTrue(REcanvas.Find("#Home").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0);



        // Will reload unexistent
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Reload unexistent");
        routerProve.Route();
        Assert.IsTrue(REcanvas.Find("#Home").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0);




        // Go to Settings
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Settings");
        routerProve.Route("Settings");
        Assert.IsTrue(REcanvas.Find("#Home").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1);

        // Return to unexistent Route
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to unexistent Route");
        routerProve.Return();
        Assert.IsTrue(REcanvas.Find("#Home").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0);


        // Delete the components
        yield return new WaitForSecondsRealtime(1);
        routerProve.Erase();

    }





    [UnityTest]
    public IEnumerator ReactorRouter_10Basics_DefaultAsNull_NullRoutes()
    {


        // Creating the router
        yield return new WaitForSecondsRealtime(1);
        ReactorRouter RouterProveReactorComponent()
        {

            return new ReactorRouter
            {
                routes = new Dictionary<string, REcanvas>()
                    {
                        {"Menu", Menu()},
                        {"Home", Home()},
                        {"Settings", Settings()},
                        {"Default", null},
                    },
                defaultRoute = "Default",
            };

        }
        var routerProve = RouterProveReactorComponent();



        // Go to Home
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Home");
        routerProve.Route("Home");
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0);

        // Go to Menu
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Menu");
        routerProve.Route("Menu");
        Assert.IsTrue(REcanvas.Find("#Home").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0);

        // Go to Settings
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Settings");
        routerProve.Route("Settings");
        Assert.IsTrue(REcanvas.Find("#Home").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1);

        // Return to Menu
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Return to Menu");
        routerProve.Return();
        Assert.IsTrue(REcanvas.Find("#Home").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 1);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0);

        // Go to Settings
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Settings");
        routerProve.Route("Settings");
        Assert.IsTrue(REcanvas.Find("#Home").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1);

        // Return to Home
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Return to Home");
        routerProve.Return(2);
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0);

        // Return more, but first is Home
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Return more, but first is Home");
        routerProve.Return(10);
        Assert.IsTrue(REcanvas.Find("#Home").Length == 1);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0);

        // Go to unexistent Route
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to unexistent Route");
        routerProve.Route("Unexistent");
        Assert.IsTrue(REcanvas.Find("#Home").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0);

        // Go to Settings
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to Settings");
        routerProve.Route("Settings");
        Assert.IsTrue(REcanvas.Find("#Home").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 1);

        // Return to unexistent Route
        yield return new WaitForSecondsRealtime(.5f);
        Debug.Log("Go to unexistent Route");
        routerProve.Return();
        Assert.IsTrue(REcanvas.Find("#Home").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Menu").Length == 0);
        Assert.IsTrue(REcanvas.Find("#Settings").Length == 0);


        // Delete the components
        yield return new WaitForSecondsRealtime(1);
        routerProve.Erase();

    }



}
