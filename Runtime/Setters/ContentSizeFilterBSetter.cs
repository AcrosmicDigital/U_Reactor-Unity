using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    /// <summary>
    /// Create a ContentSizeFitter in a gameobject with default values in Unity v2020.3.1f1
    /// </summary>
    public class ContentSizeFilterBSetter
    {
        // Listeners
        // ...
        // Properties
        public virtual ContentSizeFitter.FitMode horizontalFit { get; set; } = ContentSizeFitter.FitMode.Unconstrained;
        public virtual ContentSizeFitter.FitMode verticalFit { get; set; } = ContentSizeFitter.FitMode.Unconstrained;

        internal ContentSizeFitter Set(ContentSizeFitter c)
        {
            c.horizontalFit = horizontalFit;
            c.verticalFit = verticalFit;

            return c;
        }


        internal ContentSizeFitter Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<ContentSizeFitter>());
        }

    }
}
