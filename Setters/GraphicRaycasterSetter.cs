using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    public class GraphicRaycasterSetter
    {
        public virtual bool ignoreReversedGraphics { get; set; } = true;
        public virtual GraphicRaycaster.BlockingObjects blockingObjects { get; set; } = GraphicRaycaster.BlockingObjects.None;
        public virtual LayerMask blockingMask { get; set; } = ~0;

        public GraphicRaycaster Set(GraphicRaycaster c)
        {
            c.ignoreReversedGraphics = ignoreReversedGraphics;
            c.blockingObjects = blockingObjects;
            c.blockingMask = blockingMask;


            return c;
        }

        public GraphicRaycaster Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<GraphicRaycaster>());
        }

    }
}
