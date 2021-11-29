using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    /// <summary>
    /// Create a CanvasScaler in a gameobject with default values in Unity v2020.3.1f1
    /// </summary>
    public class CanvasScalerBSetter
    {
        // Listeners
        // ...
        // Properties
        public virtual float dynamicPixelsPerUnit { get; set; } = 1;
        public virtual float referencePixelsPerUnit { get; set; } = 100;
        public virtual CanvasScaler.ScaleMode uiScaleMode { get; set; } = CanvasScaler.ScaleMode.ConstantPixelSize;
        public virtual float scaleFactor { get; set; } = 1;
        public virtual CanvasScaler.Unit physicalUnit { get; set; } = CanvasScaler.Unit.Points;
        public virtual float fallbackScreenDPI { get; set; } = 96;
        public virtual float defaultSpriteDPI { get; set; } = 96;
        public virtual Vector3 referenceResolution { get; set; } = new Vector3(800, 600);
        public virtual CanvasScaler.ScreenMatchMode screenMatchMode { get; set; } = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        public virtual float matchWidthOrHeight { get; set; } = 0f;



        internal CanvasScaler Set(CanvasScaler c)
        {

            c.dynamicPixelsPerUnit = dynamicPixelsPerUnit;
            c.referencePixelsPerUnit = referencePixelsPerUnit;
            c.uiScaleMode = uiScaleMode;
            c.scaleFactor = scaleFactor;
            c.physicalUnit = physicalUnit;
            c.fallbackScreenDPI = fallbackScreenDPI;
            c.defaultSpriteDPI = defaultSpriteDPI;
            c.referenceResolution = referenceResolution;
            c.screenMatchMode = screenMatchMode;
            c.matchWidthOrHeight = matchWidthOrHeight;

            return c;
        }

        internal CanvasScaler Set(GameObject gameObject)
        {
            return Set (gameObject.AddComponent<CanvasScaler>());
        }

    }
}
