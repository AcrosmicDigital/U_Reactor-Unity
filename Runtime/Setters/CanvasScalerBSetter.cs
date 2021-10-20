using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    public class CanvasScalerBSetter
    {

        public virtual CanvasScaler.ScaleMode uiScaleMode { get; set; } = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        public virtual Vector3 referenceResolution { get; set; } = new Vector3(1920, 1080);
        public virtual CanvasScaler.ScreenMatchMode screenMatchMode { get; set; } = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        public virtual float matchWidthOrHeight { get; set; } = .5f;
        public virtual float referencePixelsPerUnit { get; set; } = 100;
        public virtual float scaleFactor { get; set; } = 1;
        public virtual CanvasScaler.Unit physicalUnit { get; set; } = CanvasScaler.Unit.Points;
        public virtual float fallbackScreenDPI { get; set; } = 96;
        public virtual float defaultSpriteDPI { get; set; } = 96;


        public CanvasScaler Set(CanvasScaler c)
        {
            c.uiScaleMode = uiScaleMode;

            if (uiScaleMode == CanvasScaler.ScaleMode.ConstantPixelSize)
            {
                c.scaleFactor = scaleFactor;
            }

            if (uiScaleMode == CanvasScaler.ScaleMode.ScaleWithScreenSize)
            {
                c.referenceResolution = referenceResolution;
                c.screenMatchMode = screenMatchMode;
                c.matchWidthOrHeight = matchWidthOrHeight;
            }

            if (uiScaleMode == CanvasScaler.ScaleMode.ConstantPhysicalSize)
            {
                c.physicalUnit = physicalUnit;
                c.fallbackScreenDPI = fallbackScreenDPI;
                c.defaultSpriteDPI = defaultSpriteDPI;
            }

            c.referencePixelsPerUnit = referencePixelsPerUnit;

            return c;
        }

        public CanvasScaler Set(GameObject gameObject)
        {
            return Set (gameObject.AddComponent<CanvasScaler>());
        }

    }
}
