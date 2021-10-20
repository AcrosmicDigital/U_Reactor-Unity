using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    public class ContentSizeFilterBSetter
    {
        public virtual ContentSizeFitter.FitMode horizontalFit { get; set; } = ContentSizeFitter.FitMode.Unconstrained;
        public virtual ContentSizeFitter.FitMode verticalFit { get; set; } = ContentSizeFitter.FitMode.Unconstrained;

        public ContentSizeFitter Set(ContentSizeFitter c)
        {
            c.horizontalFit = horizontalFit;
            c.verticalFit = verticalFit;

            return c;
        }


        public ContentSizeFitter Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<ContentSizeFitter>());
        }

    }
}
