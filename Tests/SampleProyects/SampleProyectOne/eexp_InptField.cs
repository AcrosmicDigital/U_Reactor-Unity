using System.Collections;
using System.Collections.Generic;
using U.Reactor;
using UnityEngine;

public class InptField : MonoBehaviour
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

                childs = () => new ReactorElement[]
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
                    new REinputField
                    {
                    },

                }

            };

        }

        var canvas = MainReactorComponent();

        canvas.Draw();

        Debug.Log("Hola");

    }

}
