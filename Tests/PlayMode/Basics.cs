using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using U.Reactor;
using UnityEngine.EventSystems;
using System;
using System.Linq;

public class Basics
{

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var mainCamera = new GameObject("Main Camera");
        var camera = mainCamera.AddComponent<Camera>();
        camera.backgroundColor = Color.grey;
        camera.orthographic = true;
        camera.depth = -1;
        mainCamera.AddComponent<AudioListener>();
        var eventSystem = new GameObject("EventSystem");
        eventSystem.AddComponent<EventSystem>();
        eventSystem.AddComponent<StandaloneInputModule>();
    }




    [UnityTest]
    public IEnumerator DrawCanvas()
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
                    new REtext
                    {
                        propsRectTransform = () => new RectTransformBaseSetter
                        {
                            localPosition = new Vector3(0, 200, 0),
                        },
                        propsText = () => new TextSetter
                        {
                            text = "Menu Principal",
                            alignment = TextAnchor.UpperCenter,
                        },
                    },
                    new REimage
                    {
                        propsRectTransform = () => new RectTransformSetterImage
                        {
                            localPosition = new Vector3(200, 0,0),
                        },
                    },
                    new REbutton
                    {
                        propsRectTransform = () => new REbutton.RectTransformSetter
                        {
                            width = 300,
                            height = 120,
                            localPosition = new Vector3(-200, 0,0),
                        },
                    },
                }

            };

        }

        var canvas = MainReactorComponent();

        canvas.Draw();

        Debug.Log("Hola");

        yield return new WaitForSecondsRealtime(5);

        canvas.Erase();
    }



}