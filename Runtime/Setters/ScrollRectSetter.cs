using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    public class ScrollRectSetter
    {
        public bool horizontal = true;
        public bool vertical = true;
        public ScrollRect.MovementType movementType = ScrollRect.MovementType.Elastic;
        public float elasticity = 0.1f;
        public bool inertia = true;
        public float decelerationRate = 0.135f;
        public float scrollSensitivity = 1;
        public ScrollRect.ScrollbarVisibility horizontalScrollbarVisibility = ScrollRect.ScrollbarVisibility.Permanent;
        public float horizontalScrollbarSpacing = 0;
        public ScrollRect.ScrollbarVisibility verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.Permanent;
        public float verticalScrollbarSpacing = 0;

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
