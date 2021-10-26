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
    /// Create a Mask in a gameobject with default values in Unity v2020.3.1f1
    /// </summary>
    public class MaskBSetter
    {
        // Listeners
        // ...
        // Properties
        public virtual bool  showMaskGraphic { get; set; } = true;

        internal Mask Set(Mask c)
        {
            c.showMaskGraphic = showMaskGraphic;

            return c;
        }


        internal Mask Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<Mask>());
        }
    }
}
