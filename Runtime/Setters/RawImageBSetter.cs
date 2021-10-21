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
    public class RawImageBSetter<TSelector> where TSelector : REbaseSelector
    {
        // Listeners
        public virtual UnityAction<bool, TSelector> OnCullStateChangedListener { get; set; } = (b, s) => { };
        // Properties
        public virtual Sprite sprite { get; set; } = null;
        public virtual Color color { get; set; } = Color.white;
        public virtual Material material { get; set; } = null;
        public virtual bool raycastTarget { get; set; } = true;
        public virtual Vector4 raycastPadding { get; set; } = Vector4.zero;
        public virtual bool maskable { get; set; } = true;
        public virtual bool useSpriteMesh { get; set; } = false;
        public virtual bool preserveAspect { get; set; } = false;


        public RawImage Set(RawImage c)
        {
            c.color = color;
            c.material = material;
            c.raycastTarget = raycastTarget;
            c.raycastPadding = raycastPadding;
            c.maskable = maskable;
            
            return c;
        }

        public RawImage Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<RawImage>());
        }

        public void SetListeners(RawImage c, TSelector selector)
        {
            c.onCullStateChanged.AddListener((b) =>
            {
                try
                {
                    OnCullStateChangedListener?.Invoke(b, selector);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error Executing OnCullStateChangedListener: " + e);
                }
            });

            
        }

    }

}
