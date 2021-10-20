using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    public class ScrollRectBSetter
    {
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

        public ScrollRect Set(ScrollRect c)
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


        public ScrollRect Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<ScrollRect>());
        }
    }
}
