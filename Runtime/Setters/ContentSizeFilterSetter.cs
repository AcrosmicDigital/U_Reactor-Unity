using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    public class ContentSizeFilterSetter
    {
        public ContentSizeFitter.FitMode horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
        public ContentSizeFitter.FitMode verticalFit = ContentSizeFitter.FitMode.Unconstrained;

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
