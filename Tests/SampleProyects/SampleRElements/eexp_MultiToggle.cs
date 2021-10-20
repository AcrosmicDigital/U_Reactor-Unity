using System.Collections;
using System.Collections.Generic;
using U.Reactor;
using UnityEngine;

public class eexp_MultiToggle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
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
                    //new REtext
                    //{
                    //    propsRectTransform = () => new RectTransformSetter
                    //    {
                    //        localPosition = new Vector3(0, 200, 0),
                    //    },
                    //    propsText = () => new TextSetter
                    //    {
                    //        text = "Menu Principal",
                    //        alignment = TextAnchor.UpperCenter,
                    //    },
                    //},
                    //new REimage
                    //{
                    //    propsRectTransform = () => new RectTransformSetterImage
                    //    {
                    //        localPosition = new Vector3(200, 0,0),
                    //    },
                    //},
                    //new REbutton
                    //{
                    //    propsRectTransform = () => new RectTransformSetter
                    //    {
                    //        width = 300,
                    //        height = 120,
                    //        localPosition = new Vector3(-200, 0,0),
                    //    },
                    //},
                    new REmultiToggle
                    {
                        childs = () => new REbase[]
                        {
                            new REtoggle
                            {
                                name = "Reed",
                                number = 1,
                                value = 1.1f,
                            },
                            new REtoggle
                            {
                                name = "Blaack",
                                number = 2,
                                value = 2.2f,
                            },
                            new REtoggle
                            {
                                name = "Green",
                                number = 3,
                                value = 3.3f,
                            },
                            new REtoggle
                            {
                                name = "Bluue",
                                number = 4,
                                value = 4.4f,
                            },
                        },
                    },

                }

            };

        }

        var canvas = MainReactorComponent();

        canvas.Draw();

        Debug.Log("Hola");

    }

}
