using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    // Here are the find elements functions
    public abstract partial class ReactorElement
    {



        // Create a child GO with recttransform and canvas renderer
        protected static GameObject InstanciateUIObject(string name, GameObject parent)
        {
            GameObject go = new GameObject(name);
            go.transform.SetParent(parent.transform);
            go.AddComponent<RectTransform>();
            go.AddComponent<CanvasRenderer>();
            go.layer = parent.layer;
            return go;
        }

        // Create a child GO with recttransform
        protected static GameObject InstanciateObject(string name, GameObject parent)
        {
            GameObject go = new GameObject(name);
            go.transform.SetParent(parent.transform);
            go.AddComponent<RectTransform>();
            go.layer = parent.layer;
            return go;
        }

        // Create a scrollbar
        protected static GameObject InstanciateScrollbar(

            string name,
            GameObject parent,

            out Scrollbar scrollbarCmp,
            out Image backImageCmp,
            out Image handleImageCmp

        )
        {

            var scrollbarGO = InstanciateUIObject(name, parent);
            var slidingAreaGO = InstanciateObject("Sliding Area", scrollbarGO);
            var handleGO = InstanciateUIObject("Handle", slidingAreaGO);

            backImageCmp = scrollbarGO.AddComponent<Image>();
            backImageCmp.type = Image.Type.Sliced;

            handleImageCmp = handleGO.AddComponent<Image>();
            handleImageCmp.type = Image.Type.Sliced;

            var recttSliderArea = slidingAreaGO.GetComponent<RectTransform>();
            recttSliderArea.sizeDelta = new Vector2(-20, -20);
            recttSliderArea.anchorMin = Vector2.zero;
            recttSliderArea.anchorMax = Vector2.one;

            var recttHandle = handleGO.GetComponent<RectTransform>();
            recttHandle.sizeDelta = new Vector2(20, 20);

            scrollbarCmp = scrollbarGO.AddComponent<Scrollbar>();
            scrollbarCmp.handleRect = recttHandle;
            scrollbarCmp.targetGraphic = handleImageCmp;

            return scrollbarGO;

        }


        protected class NoBehaviour : MonoBehaviour
        {

        }



    }

}
