using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace U.Reactor
{
    /// <summary>
    /// Create a RectTransform in a gameobject with default values in Unity v2020.3.1f1
    /// </summary>
    public class RectTransformBSetter
    {
        // Listeners
        // ...
        // Properties
        public virtual Vector2 pivot { get; set; } = new Vector2(.5f, .5f);
        public virtual Vector2 localScale { get; set; } = new Vector2(1, 1);
        public virtual Quaternion rotation { get; set; } = new Quaternion(0, 0, 0, 0);

        public virtual Vector2 anchorMin { get; set; } = new Vector2(.5f, .5f);
        public virtual Vector2 anchorMax { get; set; } = new Vector2(.5f, .5f);
        public virtual Vector3 localPosition { get; set; } = new Vector3(0, 0, 0);
        internal virtual Vector2 sizeDelta { get; set; } = Vector2.zero;
        internal virtual Vector2 offsetMin { get; set; } = Vector2.zero;
        internal virtual Vector2 offsetMax { get; set; } = Vector2.zero;
        public virtual float width { get; set; } = 100f;
        public virtual float height { get; set; } = 100f;

        // internal virtual Vector3 worldPosition { get; set; } = Vector3.zero;

        internal RectTransform SetByWidthAndHeight(RectTransform c)
        {
            c.pivot = pivot;
            c.localPosition = localPosition;
            c.anchorMin = anchorMin;
            c.anchorMax = anchorMax;
            c.sizeDelta = new Vector2(width, height);

            c.localScale = new Vector3(localScale.x, localScale.y, 1f);
            c.rotation = rotation;

            return c;
        }

        internal RectTransform SetByAnchors(RectTransform c)
        {
            c.pivot = pivot;
            c.anchorMin = anchorMin;
            c.anchorMax = anchorMax;
            //c.sizeDelta = sizeDelta; // Quitar este y ver si no se rompe nada
            c.offsetMin = offsetMin;
            c.offsetMax = offsetMax;

            c.localScale = new Vector3(localScale.x, localScale.y, 1f);
            c.rotation = rotation;

            return c;
        }

        internal RectTransform SetBySizeDelta(RectTransform c)
        {
            c.pivot = pivot;
            c.localPosition = localPosition;
            c.sizeDelta = sizeDelta;
            c.anchorMin = anchorMin;
            c.anchorMax = anchorMax;

            c.localScale = new Vector3(localScale.x, localScale.y, 1f);
            c.rotation = rotation;

            return c;
        }

        //internal RectTransform SetWorldPosition(RectTransform c)
        //{
        //    c.position = worldPosition;

        //    return c;
        //}



        // Try to get the component or add it 

        internal RectTransform SetOrSearchByWidthAndHeight(GameObject gameObject)
        {
            var rectT = gameObject.GetComponent<RectTransform>();

            if (rectT == null)
                return SetByWidthAndHeight(gameObject);

            return SetByWidthAndHeight(rectT);
        }

        internal RectTransform SetOrSearchByAnchors(GameObject gameObject)
        {
            var rectT = gameObject.GetComponent<RectTransform>();

            if (rectT == null)
                return SetByAnchors(gameObject);

            return SetByAnchors(rectT);
        }

        internal RectTransform SetOrSearchBySizeDelta(GameObject gameObject)
        {
            var rectT = gameObject.GetComponent<RectTransform>();

            if (rectT == null)
                return SetBySizeDelta(gameObject);

            return SetBySizeDelta(rectT);
        }

        //internal RectTransform SetOrSearchWorldPosition(GameObject gameObject)
        //{
        //    var rectT = gameObject.GetComponent<RectTransform>();

        //    if (rectT == null)
        //        return SetWorldPosition(gameObject);

        //    return SetWorldPosition(rectT);
        //}


        // Add a new component allways

        internal RectTransform SetByWidthAndHeight(GameObject gameObject)
        {
            return SetByWidthAndHeight(gameObject.AddComponent<RectTransform>());
        }

        internal RectTransform SetByAnchors(GameObject gameObject)
        {
            return SetByAnchors(gameObject.AddComponent<RectTransform>());
        }

        internal RectTransform SetBySizeDelta(GameObject gameObject)
        {
            return SetBySizeDelta(gameObject.AddComponent<RectTransform>());
        }

        //internal RectTransform SetWorldPosition(GameObject gameObject)
        //{
        //    return SetWorldPosition(gameObject.AddComponent<RectTransform>());
        //}

    }

}
