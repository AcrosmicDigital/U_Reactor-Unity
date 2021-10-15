using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace U.Reactor
{
    public class RectMask2DSetter
    {
        private Vector2Int softness = Vector2Int.zero;
        private Vector4 padding = Vector4.zero;

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
