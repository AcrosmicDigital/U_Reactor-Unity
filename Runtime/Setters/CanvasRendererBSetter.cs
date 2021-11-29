using UnityEngine;

namespace U.Reactor
{
    /// <summary>
    /// Create a CanvasRenderer in a gameobject with default values in Unity v2020.3.1f1
    /// </summary>
    public class CanvasRendererBSetter
    {
        // Listeners
        // ...
        // Properties
        public virtual bool cullTransparentMesh { get; set; } = true;

        internal CanvasRenderer Set(CanvasRenderer c)
        {
            c.cullTransparentMesh = cullTransparentMesh;

            return c;
        }

        internal CanvasRenderer Set(GameObject gameObject)
        {
            return Set (gameObject.AddComponent<CanvasRenderer>());
        }
    }

}
