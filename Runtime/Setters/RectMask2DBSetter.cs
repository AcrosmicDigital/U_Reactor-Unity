using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    public class RectMask2DBSetter
    {
        public virtual Vector2Int softness { get; set; } = Vector2Int.zero;
        public virtual Vector4 padding { get; set; } = Vector4.zero;

        public RectMask2D Set(RectMask2D c)
        {
            c.padding = padding;
            c.softness = softness;

            return c;
        }


        public RectMask2D Set(GameObject gameObject)
        {
            return Set(gameObject.AddComponent<RectMask2D>());
        }
    }
}
