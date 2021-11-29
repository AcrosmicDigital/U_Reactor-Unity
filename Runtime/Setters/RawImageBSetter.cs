using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    /// <summary>
    /// Create a RawImage in a gameobject with default values in Unity v2020.3.1f1
    /// </summary>
    public class RawImageBSetter<TSelector> where TSelector : REbaseSelector
    {
        // Listeners
        // ...
        // Properties
        public virtual Texture texture { get; set; } = null;
        public virtual Color color { get; set; } = Color.white;
        public virtual Material material { get; set; } = null;
        public virtual bool raycastTarget { get; set; } = true;
        public virtual Vector4 raycastPadding { get; set; } = Vector4.zero;
        public virtual bool maskable { get; set; } = true;
        public virtual Rect uvRect { get; set; } = new Rect(0,0,1,1);




        internal RawImage Set(RawImage c)
        {
            c.texture = texture;
            c.color = color;
            c.material = material;
            c.raycastTarget = raycastTarget;
            c.raycastPadding = raycastPadding;
            c.maskable = maskable;
            c.uvRect = uvRect;


            return c;
        }

        internal RawImage Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<RawImage>());
        }

    }

}
