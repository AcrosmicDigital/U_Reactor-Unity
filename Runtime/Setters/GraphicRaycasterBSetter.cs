using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    /// <summary>
    /// Create a GraphicRaycaster in a gameobject with default values in Unity v2020.3.1f1
    /// </summary>
    public class GraphicRaycasterBSetter
    {
        // Listeners
        // ...
        // Properties
        public virtual bool ignoreReversedGraphics { get; set; } = true;
        public virtual GraphicRaycaster.BlockingObjects blockingObjects { get; set; } = GraphicRaycaster.BlockingObjects.None;
        public virtual LayerMask blockingMask { get; set; } = ~0;  // This meaning everything

        internal GraphicRaycaster Set(GraphicRaycaster c)
        {
            c.ignoreReversedGraphics = ignoreReversedGraphics;
            c.blockingObjects = blockingObjects;
            c.blockingMask = blockingMask;


            return c;
        }

        internal GraphicRaycaster Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<GraphicRaycaster>());
        }

    }
}
