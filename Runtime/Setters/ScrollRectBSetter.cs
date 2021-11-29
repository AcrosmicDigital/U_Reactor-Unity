using System;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    /// <summary>
    /// Create a ScrollRect in a gameobject with default values in Unity v2020.3.1f1
    /// </summary>
    public class ScrollRectBSetter<TSelector> where TSelector : REbaseSelector
    {
        // Listeners
        public virtual Action<Vector2, TSelector> OnValueChangedListener { get; set; } = (v, s) => { };
        // Properties
        public virtual bool horizontal { get; set; } = true;
        public virtual bool vertical { get; set; } = true;
        public virtual ScrollRect.MovementType movementType { get; set; } = ScrollRect.MovementType.Elastic;
        public virtual float elasticity { get; set; } = 0.1f;
        public virtual bool inertia { get; set; } = true;
        public virtual float decelerationRate { get; set; } = 0.135f;
        public virtual float scrollSensitivity { get; set; } = 1;
        public virtual ScrollRect.ScrollbarVisibility horizontalScrollbarVisibility { get; set; } = ScrollRect.ScrollbarVisibility.Permanent;
        public virtual float horizontalScrollbarSpacing { get; set; } = 0;
        public virtual ScrollRect.ScrollbarVisibility verticalScrollbarVisibility { get; set; } = ScrollRect.ScrollbarVisibility.Permanent;
        public virtual float verticalScrollbarSpacing { get; set; } = 0;


        internal ScrollRect Set(ScrollRect c)
        {
            c.horizontal = horizontal;
            c.vertical = vertical;
            c.movementType = movementType;
            c.inertia = inertia;
            c.elasticity = elasticity;
            c.decelerationRate = decelerationRate;
            c.scrollSensitivity = scrollSensitivity;
            c.horizontalScrollbarVisibility = horizontalScrollbarVisibility;
            c.horizontalScrollbarSpacing = horizontalScrollbarSpacing;
            c.verticalScrollbarVisibility = verticalScrollbarVisibility;
            c.verticalScrollbarSpacing = verticalScrollbarSpacing;
            
            return c;
        }


        internal ScrollRect Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<ScrollRect>());
        }

        internal void SetListeners(ScrollRect c, TSelector selector)
        {
            c.onValueChanged.AddListener((v) =>
            {
                try
                {
                    OnValueChangedListener?.Invoke(v, selector);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error Executing OnValueChangedListener: " + e);
                }
            });


        }
    }
}
