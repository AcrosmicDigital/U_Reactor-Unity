using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using U.Reactor;
using UnityEngine.EventSystems;
using System;
using System.Linq;

public class ReactorElements_REbutton
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
    public IEnumerator Draw_REbutton()
    {

        // A Component
        REcanvas MainReactorComponent()
        {

            return new REcanvas
            {

                propsGameObject = () => new GameObjectSetter
                {
                    name = "Canvas-Principal",
                },

                childs = () => new REbase[]
                {
                    new REbutton
                    {
                        propsRectTransform = () => new REbutton.RectTransformSetter
                        {
                            localPosition = new Vector3(100,200),
                        }
                    }
                }

            };

        }

        var canvas = MainReactorComponent();

        canvas.Draw();

        Debug.Log("Hola");

        yield return new WaitForSecondsRealtime(50);


    }

}