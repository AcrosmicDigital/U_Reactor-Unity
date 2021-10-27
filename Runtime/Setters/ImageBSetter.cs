using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace U.Reactor
{
    /// <summary>
    /// Create a Image in a gameobject with default values in Unity v2020.3.1f1
    /// </summary>
    public class ImageBSetter<TSelector> where TSelector : REbaseSelector
    {
        // Listeners
        // ...
        // Properties
        public virtual Sprite sprite { get; set; } = null;
        public virtual Color color { get; set; } = Color.white;
        public virtual Material material { get; set; } = null;
        public virtual bool raycastTarget { get; set; } = true;
        public virtual Vector4 raycastPadding { get; set; } = Vector4.zero;
        public virtual bool maskable { get; set; } = true;
        public virtual Image.Type type { get; set; } = Image.Type.Simple;
        public virtual bool useSpriteMesh { get; set; } = false;
        public virtual bool preserveAspect { get; set; } = false;
        public virtual float pixelsPerUnitMultiplier { get; set; } = 1;
        public virtual Image.FillMethod fillMethod { get; set; } = Image.FillMethod.Radial360;
        public virtual int fillOrigin { get; set; } = 0;
        public virtual float fillAmount { get; set; } = 1;
        public virtual bool fillClockwise { get; set; } = true;




        internal Image Set(Image c)
        {
            SetAllExceptType(c);

            c.type = type;

            return c;
        }

        internal Image SetAllExceptType(Image c)
        {
            c.sprite = sprite;
            c.color = color;
            c.material = material;
            c.raycastTarget = raycastTarget;
            c.raycastPadding = raycastPadding;
            c.maskable = maskable;
            c.useSpriteMesh = useSpriteMesh;
            c.preserveAspect = preserveAspect;
            c.pixelsPerUnitMultiplier = pixelsPerUnitMultiplier;
            c.fillMethod = fillMethod;
            c.fillOrigin = fillOrigin;
            c.fillAmount = fillAmount;
            c.fillClockwise = fillClockwise;

            return c;
        }


        internal Image Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<Image>());
        }


    }

}
