using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    /// <summary>
    /// Create a HorizontalLayoutGroup in a gameobject with default values in Unity v2020.3.1f1
    /// </summary>
    public class HorizontalLayoutGroupBSetter
    {
        // Listeners
        // ...
        // Properties
        public virtual RectOffset padding { get; set; } = new RectOffset(0, 0, 0, 0);
        public virtual float spacing { get; set; } = 0;
        public virtual TextAnchor childAlignment { get; set; } = TextAnchor.UpperLeft;
        public virtual bool reverseArrangement { get; set; } = false;
        public virtual bool childControlHeight { get; set; } = false;
        public virtual bool childControlWidth { get; set; } = false;
        public virtual bool childScaleHeight { get; set; } = false;
        public virtual bool childScaleWidth { get; set; } = false;
        public virtual bool childForceExpandHeight { get; set; } = true;
        public virtual bool childForceExpandWidth { get; set; } = true;

        internal HorizontalLayoutGroup Set(HorizontalLayoutGroup c)
        {
            c.padding = padding;
            c.spacing = spacing;
            c.childAlignment = childAlignment;
            c.reverseArrangement = reverseArrangement;
            c.childControlHeight = childControlHeight;
            c.childControlWidth = childControlWidth;
            c.childScaleHeight = childScaleHeight;
            c.childScaleWidth = childScaleWidth;
            c.childForceExpandHeight = childForceExpandHeight;
            c.childForceExpandWidth = childForceExpandWidth;

            return c;
        }


        internal HorizontalLayoutGroup Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<HorizontalLayoutGroup>());
        }
    }
}
