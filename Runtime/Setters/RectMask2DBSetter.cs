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
    /// Create a RectMask2D in a gameobject with default values in Unity v2020.3.1f1
    /// </summary>
    public class RectMask2DBSetter
    {
        // Listeners
        // ...
        // Properties
        public virtual Vector2Int softness { get; set; } = Vector2Int.zero;
        public virtual Vector4 padding { get; set; } = Vector4.zero;

        internal RectMask2D Set(RectMask2D c)
        {
            c.padding = padding;
            c.softness = softness;

            return c;
        }


        internal RectMask2D Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<RectMask2D>());
        }
    }
}
