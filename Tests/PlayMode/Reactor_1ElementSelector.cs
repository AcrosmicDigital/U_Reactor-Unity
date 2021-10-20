using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using U.Reactor;
using UnityEngine.EventSystems;
using System;
using System.Linq;

public class Reactor_1ElementSelector
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
    public IEnumerator AccessTo_ParentChildsBrothers()
    {

        // A Component
        // CanvasMain
        //   PanelMain1
        //      PanelMain1-PanelChild1*
        //        PanelMain1-PanelChild1-Text
        //        PanelMain1-PanelChild1-Image
        //        PanelMain1-PanelChild1-Panel
        //          PanelMain1-PanelChild1-Panel-Text
        //          PanelMain1-PanelChild1-Panel-Image
        //      PanelMain1-PanelChild2
        //        PanelMain1-PanelChild2-Text
        //        PanelMain1-PanelChild2-Image
        //      PanelMain1-PanelChild3
        //      PanelMain1-PanelChild4
        //   PanelMain2
        //      PanelMain2-PanelChild1
        //      PanelMain2-PanelChild2
        //      PanelMain2-PanelChild3
        //      PanelMain2-PanelChild4
        //        PanelMain2-PanelChild4-Text
        //        PanelMain2-PanelChild4-Image
        //   PanelMain3
        //   PanelMain4
        REcanvas MainReactorComponent()
        {

            return new REcanvas
            {
                propsGameObject = () => new GameObjectBSetter { name = "CanvasMain" },
                childs = () => new REbase[] {
                        new REpanel {
                            propsGameObject = () => new GameObjectBSetter { name = "PanelMain1" },
                            childs = () => new REbase[]
                            {
                                new REpanel {
                                    propsGameObject = () => new GameObjectBSetter{ name = "PanelMain1-PanelChild1"},
                                    childs = () => new REbase[]
                                    {
                                        new REtext{ propsGameObject = () => new GameObjectBSetter{ name = "PanelMain1-PanelChild1-Text"} },
                                        new REimage{ propsGameObject = () => new GameObjectBSetter{ name = "PanelMain1-PanelChild1-Image"} },
                                        new REpanel{
                                            propsGameObject = () => new GameObjectBSetter{ name = "PanelMain1-PanelChild2-Panel"},
                                            childs = () => new REbase[]
                                            {
                                                new REtext{ propsGameObject = () => new GameObjectBSetter{ name = "PanelMain1-PanelChild2-Panel-Text"} },
                                                new REimage{ propsGameObject = () => new GameObjectBSetter{ name = "PanelMain1-PanelChild2-Panel-Image"} },
                                            },
                                        },
                                    },
                                    // *
                                    useEffect = new REpanel.UseEffect.Hook[]{
                                        new REpanel.UseEffect.Hook {
                                            duration = 2,
                                            deltaFunction = (s) =>
                                            {
                                                // PanelMain1-PanelChild1*-Panel-Image
                                                Debug.Log("PanelMain1-PanelChild1-Panel-Image: *" + s.childs[2].childs[1].gameObject.name + "*");
                                                Assert.IsTrue(s.childs[2].childs[1].gameObject.name == "PanelMain1-PanelChild2-Panel-Image");


                                                // PanelMain1-PanelChild1*-Panel-Image
                                                Debug.Log("PanelMain1-PanelChild1-Image: " + s.childs[2].childs[1].gameObject.name);
                                                Assert.IsTrue(s.childs[1].gameObject.name == "PanelMain1-PanelChild1-Image");

                                                // PanelMain1
                                                Debug.Log("PanelMain1: " + s.parent.gameObject.name);
                                                Assert.IsTrue(s.parent.gameObject.name == "PanelMain1");

                                                // CanvasMain
                                                Debug.Log("CanvasMain: " + s.parent.parent.gameObject.name);
                                                Assert.IsTrue(s.parent.parent.gameObject.name == "CanvasMain");

                                                // PanelMain1-PanelChild2-Text
                                                Debug.Log("PanelMain1-PanelChild2-Text: " + s.brothers[1].childs[0].gameObject.name);
                                                Assert.IsTrue(s.brothers[1].childs[0].gameObject.name == "PanelMain1-PanelChild2-Text");

                                                // PanelMain4
                                                Debug.Log("PanelMain4: " + s.parent.brothers[3].gameObject.name);
                                                Assert.IsTrue(s.parent.brothers[3].gameObject.name == "PanelMain4");

                                                // PanelMain2-PanelChild4-Image
                                                Debug.Log("PanelMain2-PanelChild4-Image: " + s.parent.brothers[3].gameObject.name);
                                                Assert.IsTrue(s.parent.brothers[1].childs[3].childs[1].gameObject.name == "PanelMain2-PanelChild4-Image");


                                                // Null reference
                                                Assert.Throws<NullReferenceException>(() => Debug.Log(s.parent.parent.parent.parent));

                                                // Inexistent child or brother
                                                Assert.Throws<IndexOutOfRangeException>(() => Debug.Log(s.childs[10]));

                                                // Inexistent reference
                                                Assert.Throws<IndexOutOfRangeException>(() => Debug.Log(s.brothers[10]));

                                                // No childs
                                                Assert.IsTrue(s.brothers[2].childs.Length == 0);

                                            },
                                        }
                                    },
                                },
                                new REpanel{
                                    propsGameObject = () => new GameObjectBSetter{ name = "PanelMain1-PanelChild2"},
                                    childs = () => new REbase[]
                                    {
                                        new REtext{ propsGameObject = () => new GameObjectBSetter{ name = "PanelMain1-PanelChild2-Text"} },
                                        new REimage{ propsGameObject = () => new GameObjectBSetter{ name = "PanelMain1-PanelChild2-Image"} },
                                    },
                                },
                                new REpanel{ propsGameObject = () => new GameObjectBSetter{ name = "PanelMain1-PanelChild3"} },
                                new REpanel{ propsGameObject = () => new GameObjectBSetter{ name = "PanelMain1-PanelChild4"} },
                            },
                        },
                        new REpanel {
                            propsGameObject = () => new GameObjectBSetter { name = "PanelMain2" },
                            childs = () => new REbase[]
                            {
                                new REpanel{ propsGameObject = () => new GameObjectBSetter{ name = "PanelMain2-PanelChild1"} },
                                new REpanel{ propsGameObject = () => new GameObjectBSetter{ name = "PanelMain2-PanelChild2"} },
                                new REpanel{ propsGameObject = () => new GameObjectBSetter{ name = "PanelMain2-PanelChild3"} },
                                new REpanel {
                                    propsGameObject = () => new GameObjectBSetter{ name = "PanelMain2-PanelChild4"},
                                    childs = () => new REbase[]
                                    {
                                        new REtext{ propsGameObject = () => new GameObjectBSetter{ name = "PanelMain2-PanelChild4-Text"} },
                                        new REimage{ propsGameObject = () => new GameObjectBSetter{ name = "PanelMain2-PanelChild4-Image"} },
                                    },
                                },
                            },
                        },
                        new REpanel{ propsGameObject = () => new GameObjectBSetter{ name = "PanelMain3"} },
                        new REpanel{ propsGameObject = () => new GameObjectBSetter{ name = "PanelMain4"} },
                    },


            };

        }

        var routerProve = MainReactorComponent().Draw();

        yield return new WaitForSecondsRealtime(3);
        routerProve.Erase();

    }


}