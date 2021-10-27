using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    /// <summary>
    /// Create a Canvas in a gameobject with default values in Unity v2020.3.1f1
    /// </summary>
    public class CanvasBSetter
    {
        

        // Listeners
        // ...
        // Properties
        public virtual bool pixelPerfect { get; set; } = false;
        public virtual bool overrideSorting { get; set; } = false;
        public virtual int sortingOrder { get; set; } = 0;  // In Editor can be Order in layer  
        public virtual AdditionalCanvasShaderChannels additionalShaderChannels { get; set; } = AdditionalCanvasShaderChannels.None;
        public virtual RenderMode renderMode { get; set; } = RenderMode.WorldSpace;
        public virtual int targetDisplay { get; set; } = 0;
        public virtual Camera worldCamera { get; set; } = null;

        internal Canvas Set(Canvas c)
        {

            c.pixelPerfect = pixelPerfect;
            c.overrideSorting = overrideSorting;
            c.sortingOrder = sortingOrder;
            c.worldCamera = worldCamera;
            c.additionalShaderChannels = additionalShaderChannels;
            c.renderMode = renderMode;
            c.targetDisplay = targetDisplay;


            return c;
        }

        internal Canvas Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<Canvas>());
        }

    }
}
