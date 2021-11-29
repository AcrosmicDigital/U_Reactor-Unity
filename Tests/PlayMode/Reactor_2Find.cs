using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using U.Reactor;
using UnityEngine.EventSystems;
using System;
using System.Linq;

public class Reactor_2Find
{


    GameObject mainCamera = null;
    GameObject eventSystem = null;
    REcanvas mainCanvas = null;

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

        // Draw the canvas
        mainCanvas = MainReactorComponent().Draw();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        UnityEngine.Object.Destroy(mainCamera);
        UnityEngine.Object.Destroy(eventSystem);

        mainCanvas.Erase();
    }


    // Canvas with components to find
    // ids:
    // - CanvasSuperior 1
    // - Title-One 2
    // - ImagenSuperior 1
    // - PanelBack 1
    // - 
    // - 
    //
    // classNames:
    // - Back 2
    // - H1 2
    // - Text 4
    // - H1&&Text 2
    // - Title 2
    // - H1&&Text&&Title 1
    // - Button 2
    // - Button&&Title 1
    // - Button||Text 6
    // - Back||H1||Text 6
    // - 
    // - 
    // ReactorElementsType: Total: 12
    // - REcanvas 1
    // - REtext 4
    // - REimage 4
    // - REbutton 2
    // - REpanel 1
    // - 

    REcanvas MainReactorComponent()
    {

        return new REcanvas
        {
            propsId = () => new REcanvas.IdSetter
            {
                id = "CanvasSuperior",
                className = new string[]
                {
                    "Back",
                },
            },
            childs = () => new REbase[]
            {
                new REtext
                {
                    propsId = () => new REtext.IdSetter
                    {
                        id = "Title-One",
                        className = new string[]
                        {
                            "H1",
                            "Text",
                            "Title",
                        },
                    },
                },
                new REimage
                {
                    propsId = () => new REimage.IdSetter
                    {
                        id = "ImagenSuperior",
                    },
                },
                new REbutton
                {
                    propsId = () => new REbutton.IdSetter
                    {
                        id = "ButtonSuperior",
                        className = new string[]
                        {
                            "Button",
                            "Title",
                        },
                    },
                },
                new REpanel
                {
                    propsId = () => new REpanel.IdSetter
                    {
                        id = "PanelBack",
                        className = new string[]
                        {
                            "Back",
                        },
                    },
                    childs = () => new REbase[]
                    {
                        new REtext
                        {
                            propsId = () => new REtext.IdSetter
                            {
                                id = "Title-One",
                                className = new string[]
                                {
                                    "H1",
                                    "Text",
                                },
                            },
                        },
                        new REtext
                        {
                            propsId = () => new REtext.IdSetter
                            {
                                className = new string[]
                                {
                                    "Text",
                                },
                            },
                        },
                        new REtext
                        {
                            propsId = () => new REtext.IdSetter
                            {
                                className = new string[]
                                {
                                    "Text",
                                },
                            },
                        },
                        new REbutton
                        {
                            propsId = () => new REbutton.IdSetter
                            {
                                id = "ButtonSuperior",
                                className = new string[]
                                {
                                    "Button",
                                },
                            },
                        },
                        new REimage(),
                        new REimage(),
                        new REimage(),
                    }
                },
            }

        };

    }




    [UnityTest]
    public IEnumerator FindAll()
    {

        yield return new WaitForSecondsRealtime(2);
        


        // Select All
        var finded = REbase.Find();
        Assert.IsTrue(finded.Length == 12);




        // Select All
        finded = REbase.Find("");
        Assert.IsTrue(finded.Length == 12);



        yield return new WaitForSecondsRealtime(2);


    }


    [UnityTest]
    public IEnumerator FindById()
    {

        yield return new WaitForSecondsRealtime(2);




        // By id
        var finded = REbase.Find("#CanvasSuperior");
        Assert.IsTrue(finded.Length == 1);



        // By id
        finded = REbase.Find("#Title-One");
        Assert.IsTrue(finded.Length == 2);



        // By id
        finded = REbase.Find("#ImagenSuperior");
        Assert.IsTrue(finded.Length == 1);




        // By id
        finded = REbase.Find("#PanelBack");
        Assert.IsTrue(finded.Length == 1);



        // By id
        finded = REbase.Find("#MainButton");
        Assert.IsTrue(finded.Length == 0);



        yield return new WaitForSecondsRealtime(2);


    }



    [UnityTest]
    public IEnumerator FindOne()
    {

        yield return new WaitForSecondsRealtime(2);




        // By id
        Assert.IsTrue(REbase.Find("#CanvasSuperior").Length == 1);
        Assert.IsTrue(REbase.FindOne("#CanvasSuperior") != null);



        // By id
        Assert.IsTrue(REbase.Find("#ImagenSuperior").Length == 1);
        Assert.IsTrue(REbase.FindOne("#ImagenSuperior") != null);




        // By id
        Assert.IsTrue(REbase.Find("#PanelBack").Length == 1);
        Assert.IsTrue(REbase.FindOne("#PanelBack") != null);



        yield return new WaitForSecondsRealtime(2);


    }



    [UnityTest]
    public IEnumerator FindByClassName()
    {
        yield return new WaitForSecondsRealtime(2);


        // One classname
        var finded = REbase.Find(".Back");
        Debug.Log(finded.Length);
        Assert.IsTrue(finded.Length == 2);

        finded = REbase.Find(".H1");
        Debug.Log(finded.Length);
        Assert.IsTrue(finded.Length == 2);

        finded = REbase.Find(".Text");
        Debug.Log(finded.Length);
        Assert.IsTrue(finded.Length == 4);

        finded = REbase.Find(".Title");
        Debug.Log(finded.Length);
        Assert.IsTrue(finded.Length == 2);

        finded = REbase.Find(".Button");
        Debug.Log(finded.Length);
        Assert.IsTrue(finded.Length == 2);

        finded = REbase.Find(".White");
        Debug.Log(finded.Length);
        Assert.IsTrue(finded.Length == 0);



        // With AND
        finded = REbase.Find(".H1&&.Text&&.Title");
        Debug.Log(finded.Length);
        Assert.IsTrue(finded.Length == 1);

        finded = REbase.Find(".Button&&Title");
        Debug.Log(finded.Length);
        Assert.IsTrue(finded.Length == 1);

        finded = REbase.Find(".H1&&.Text");
        Debug.Log(finded.Length);
        Assert.IsTrue(finded.Length == 2);

        finded = REbase.Find(".H1&&Pink");
        Debug.Log(finded.Length);
        Assert.IsTrue(finded.Length == 0);



        // With or
        finded = REbase.Find(".Button||Text");
        Debug.Log(finded.Length);
        Assert.IsTrue(finded.Length == 6);

        finded = REbase.Find(".Back||H1||Text");
        Debug.Log(finded.Length);
        Assert.IsTrue(finded.Length == 6);

        finded = REbase.Find(".Back||H1||Grey");
        Debug.Log(finded.Length);
        Assert.IsTrue(finded.Length == 4);



        yield return new WaitForSecondsRealtime(2);

    }




    [UnityTest]
    public IEnumerator FindAllOfType()
    {

        yield return new WaitForSecondsRealtime(2);


        // - REcanvas 1
        // - REtext 4
        // - REimage 4
        // - REbutton 2
        // - REpanel 1


        // Select All
        REcanvas.Selector[] finded = REcanvas.Find();
        Debug.Log(finded.Length);
        Assert.IsTrue(finded.Length == 1);

        REtext.Selector[] finded2 = REtext.Find();
        Debug.Log(finded2.Length);
        Assert.IsTrue(finded2.Length == 4);

        REimage.Selector[] finded3 = REimage.Find();
        Debug.Log(finded3.Length);
        Assert.IsTrue(finded3.Length == 4);

        REbutton.Selector[] finded4 = REbutton.Find();
        Debug.Log(finded4.Length);
        Assert.IsTrue(finded4.Length == 2);

        REpanel.Selector[] finded5 = REpanel.Find();
        Debug.Log(finded5.Length);
        Assert.IsTrue(finded5.Length == 1);



        // Select All
        finded = REcanvas.Find("");
        Debug.Log(finded.Length);
        Assert.IsTrue(finded.Length == 1);

        finded2 = REtext.Find("");
        Debug.Log(finded2.Length);
        Assert.IsTrue(finded2.Length == 4);

        finded3 = REimage.Find("");
        Debug.Log(finded3.Length);
        Assert.IsTrue(finded3.Length == 4);

        finded4 = REbutton.Find("");
        Debug.Log(finded4.Length);
        Assert.IsTrue(finded4.Length == 2);

        finded5 = REpanel.Find("");
        Debug.Log(finded5.Length);
        Assert.IsTrue(finded5.Length == 1);



        yield return new WaitForSecondsRealtime(2);


    }

    [UnityTest]
    public IEnumerator FindByIdOfType()
    {

        yield return new WaitForSecondsRealtime(2);




        // By id
        REcanvas.Selector[] finded = REcanvas.Find("#CanvasSuperior");
        Assert.IsTrue(finded.Length == 1);



        // By id
        REtext.Selector[] finded2 = REtext.Find("#Title-One");
        Assert.IsTrue(finded2.Length == 2);



        // By id
        REcanvas.Selector[] finded3 = REcanvas.Find("#ImagenSuperior");
        Assert.IsTrue(finded3.Length == 0);




        // By id
        REpanel.Selector[] finded4 = REpanel.Find("#PanelBack");
        Assert.IsTrue(finded4.Length == 1);



        // By id
        REbutton.Selector[] finded5 = REbutton.Find("#MainButton");
        Assert.IsTrue(finded5.Length == 0);



        yield return new WaitForSecondsRealtime(2);


    }


    [UnityTest]
    public IEnumerator FindOneByType()
    {

        yield return new WaitForSecondsRealtime(2);




        // By id
        Assert.IsTrue(REcanvas.Find("#CanvasSuperior").Length == 1); Debug.Log("One");
        Assert.IsTrue(REcanvas.FindOne("#CanvasSuperior") != null); Debug.Log("One");



        // By id
        Assert.IsTrue(REcanvas.Find("#ImagenSuperior").Length == 0); Debug.Log("One");
        Assert.IsTrue(REcanvas.FindOne("#ImagenSuperior") == null); Debug.Log("One");




        yield return new WaitForSecondsRealtime(2);


    }


    [UnityTest]
    public IEnumerator FindByClassNameOfType()
    {
        yield return new WaitForSecondsRealtime(2);


        // One classname
        var finded = REcanvas.Find(".Back");
        Debug.Log(finded.Length);
        Assert.IsTrue(finded.Length == 1);

        var finded2 = REtext.Find(".H1");
        Debug.Log(finded2.Length);
        Assert.IsTrue(finded2.Length == 2);

        var finded3 = REtext.Find(".Text");
        Debug.Log(finded3.Length);
        Assert.IsTrue(finded3.Length == 4);

        var finded4 = REimage.Find(".Title");
        Debug.Log(finded4.Length);
        Assert.IsTrue(finded4.Length == 0);

        var finded5 = REbutton.Find(".Button");
        Debug.Log(finded5.Length);
        Assert.IsTrue(finded5.Length == 2);

        var finded6 = REcanvas.Find(".White");
        Debug.Log(finded6.Length);
        Assert.IsTrue(finded6.Length == 0);



        // With AND
        var finded7 = REtext.Find(".H1&&.Text&&.Title");
        Debug.Log(finded7.Length);
        Assert.IsTrue(finded7.Length == 1);

        var finded8 = REbutton.Find(".Button&&Title");
        Debug.Log(finded8.Length);
        Assert.IsTrue(finded8.Length == 1);

        var finded9 = REimage.Find(".H1&&.Text");
        Debug.Log(finded9.Length);
        Assert.IsTrue(finded9.Length == 0);

        var finded10 = REcanvas.Find(".H1&&Pink");
        Debug.Log(finded10.Length);
        Assert.IsTrue(finded10.Length == 0);



        // With or
        var finded11 = REtext.Find(".Button||Text");
        Debug.Log(finded11.Length);
        Assert.IsTrue(finded11.Length == 4);

        var finded12 = REpanel.Find(".Back||H1||Text");
        Debug.Log(finded12.Length);
        Assert.IsTrue(finded12.Length == 1);

        var finded13 = REtext.Find(".Back||H1||Grey");
        Debug.Log(finded13.Length);
        Assert.IsTrue(finded13.Length == 2);



        yield return new WaitForSecondsRealtime(2);

    }


}
