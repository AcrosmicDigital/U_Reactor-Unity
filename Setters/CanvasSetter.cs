using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    public class CanvasSetter
    {
        public virtual bool enabled { get; set; } = true;
        public virtual RenderMode renderMode { get; set; } = RenderMode.ScreenSpaceOverlay;
        public virtual bool pixelPerfect { get; set; } = false;
        public virtual int sortOrder { get; set; } = 0;
        public virtual int targetDisplay { get; set; } = 0;
        public virtual Camera worldCamera { get; set; } = null;
        public virtual int sortingLayerID { get; set; }
        public virtual string sortingLayerName { get; set; }
        public virtual AdditionalCanvasShaderChannels additionalShaderChannels { get; set; } = AdditionalCanvasShaderChannels.None;


        public Canvas Set(Canvas c)
        {
            c.renderMode = renderMode;

            if (renderMode == RenderMode.ScreenSpaceOverlay)
            {
                c.pixelPerfect = pixelPerfect;
                c.sortingOrder = sortOrder;
                c.targetDisplay = targetDisplay;
            }

            if (renderMode == RenderMode.ScreenSpaceCamera)
            {
                c.pixelPerfect = pixelPerfect;
                c.worldCamera = worldCamera;
            }

            if (renderMode == RenderMode.WorldSpace)
            {
                c.sortingOrder = sortOrder;
                c.sortingLayerID = sortingLayerID;
                c.sortingLayerName = sortingLayerName;
            }

            c.additionalShaderChannels = additionalShaderChannels;

            return c;
        }

        public Canvas Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<Canvas>());
        }

    }
}
