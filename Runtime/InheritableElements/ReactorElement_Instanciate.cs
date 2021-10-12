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
        protected static void InstanciateScrollbar(string name, GameObject parent,

            out GameObject gObScrollbar,
            out RectTransform recttScrollbar,
            out Image imageBg,
            out Scrollbar scrollbar,
            out Image imageHandle

        )
        {

            gObScrollbar = InstanciateUIObject("ScrollBar-" + name, parent);
            var gObSliding = InstanciateObject("SlidingArea-" + name, gObScrollbar);
            var gObHandle = InstanciateUIObject("Handle-" + name, gObSliding);

            imageBg = gObScrollbar.AddComponent<Image>();
            imageBg.type = Image.Type.Sliced;
            imageBg.color = Color.red;

            imageHandle = gObHandle.AddComponent<Image>();
            imageHandle.type = Image.Type.Sliced;
            imageHandle.color = Color.green;

            var recttSliderArea = gObSliding.GetComponent<RectTransform>();
            recttSliderArea.sizeDelta = new Vector2(-20, -20);
            recttSliderArea.anchorMin = Vector2.zero;
            recttSliderArea.anchorMax = Vector2.one;

            var recttHandle = gObHandle.GetComponent<RectTransform>();
            recttHandle.sizeDelta = new Vector2(20, 20);

            scrollbar = gObScrollbar.AddComponent<Scrollbar>();
            scrollbar.handleRect = recttHandle;
            scrollbar.targetGraphic = imageHandle;

            recttScrollbar = gObScrollbar.GetComponent<RectTransform>();

        }


        protected class NoBehaviour : MonoBehaviour
        {

        }



    }

}
